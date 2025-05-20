using ClosedXML.Excel;
using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.RequestEntites;
using Fretefy.Test.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class Regiaocontroller : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;
        private readonly IRegiaoCidadeService _regiaoCidadeService;
        private readonly ICidadeService _cidadeService;

        public Regiaocontroller(IRegiaoService regiaoService, IRegiaoCidadeService regiaoCidadeService, ICidadeService cidadeService)
        {
            _regiaoService = regiaoService;
            _regiaoCidadeService = regiaoCidadeService;
            _cidadeService = cidadeService;
        }


        [Route("get")]
        [HttpGet]
        public IActionResult getRegiaoCidade([FromHeader] Guid? idRegiao, [FromHeader] bool? incluirInativas)
        {
            bool incluirInativasFlag = incluirInativas ?? false; // padrão false

            if (idRegiao == null || idRegiao == Guid.Empty)
            {
                // Buscar todas as regiões, considerando o filtro de 'Ativa'
                var regioesQuery = _regiaoService.List().AsQueryable();

                if (!incluirInativasFlag)
                {
                    regioesQuery = regioesQuery.Where(r => r.Ativa == true);
                }

                var regioes = regioesQuery.ToList();

                var resultado = regioes.Select(regiao =>
                {
                    var regiaoCidades = _regiaoCidadeService.listByRegiaoId(regiao.Id).ToList();

                    var cidadeIds = regiaoCidades.Select(rc => rc.CidadeID).ToList();

                    var cidades = _cidadeService.List()
                        .Where(c => cidadeIds.Contains(c.Id))
                        .Select(c => new
                        {
                            id = c.Id,
                            nome = c.Nome,
                            uf = c.UF
                        })
                        .ToList();

                    return new
                    {
                        id = regiao.Id,
                        nome = regiao.Nome,
                        ativa = regiao.Ativa,
                        cidades = cidades

                    };
                });

                return Ok(resultado);
            }
            else
            {
                // Buscar apenas a região informada
                var regiao = _regiaoService.buscaByID(idRegiao.Value);

                if (regiao == null)
                {
                    return NotFound(new { success = false, message = "Região não encontrada." });
                }

                // Se não incluir inativas, e a região está inativa, retorna 404
                if (!incluirInativasFlag && regiao.Ativa != true)
                {
                    return NotFound(new { success = false, message = "Região não encontrada." });
                }

                var regiaoCidades = _regiaoCidadeService.listByRegiaoId(regiao.Id).ToList();

                var cidadeIds = regiaoCidades.Select(rc => rc.CidadeID).ToList();

                var cidades = _cidadeService.List()
                    .Where(c => cidadeIds.Contains(c.Id))
                    .Select(c => new
                    {
                        id = c.Id,
                        nome = c.Nome,
                        uf = c.UF
                    })
                    .ToList();

                var resultado = new
                {
                    id = regiao.Id,
                    nome = regiao.Nome,
                    ativa = regiao.Ativa,
                    cidades = cidades
                };

                return Ok(resultado);
            }
        }




        [Route("insert")]
        [HttpPost]
        public IActionResult InsertRegiao([FromBody] RegiaoInsertRequest regiaoRequest)
        {

            bool existeNome = _regiaoService.exissteNome(regiaoRequest.Nome);
            if (!existeNome)
            {
                (bool, string) regiao = _regiaoService.insertRegiao(regiaoRequest.Nome);
                if (regiao.Item1 && regiaoRequest.Cidades.Any())
                {
                    bool retorno = _regiaoCidadeService.insertRegiaoCidade(regiaoRequest.Cidades, regiao.Item2);
                    if (retorno == true)
                    {
                        return StatusCode(201, new { status = "success", message = "Inserido com Sucesso" });
                        // 201 = Created
                    }
                    else
                    {
                        return StatusCode(500, new { status = "error", message = "Falha ao inserir Cidades" });
                        // 500 = Internal Server Error

                    }
                }
                else if (regiao.Item1)
                {
                    return StatusCode(201, new { status = "success", message = "Inserido com Sucesso" });
                }
                else
                {
                    return StatusCode(400, new { status = "error", message = regiao.Item2 });
                    // 400 = Bad Request
                }
            }
            else
            {
                return StatusCode(409, new { status = "error", message = $"Região com nome: {regiaoRequest.Nome} já existe na base." });
                // 409 = Conflict
            }
        }


        [Route("put")]
        [HttpPut]
        public IActionResult PutRegiao([FromBody] RegiaoPutRequest regiaoRequest)
        {
            var regiao = _regiaoService.buscaByID(regiaoRequest.id);

            if (regiao == null)
            {
                return StatusCode(404, new { success = false, message = "Região não encontrada." });
            }
            bool alteraRegiaoResult = _regiaoService.alteraRegiao(regiaoRequest.id, regiaoRequest.nome);
            if (alteraRegiaoResult)
            {
                bool alteraRegiaoCidades = _regiaoCidadeService.alterarRegiaocidade(regiaoRequest.Cidades, regiaoRequest.id);
                if (alteraRegiaoCidades)
                {
                    return StatusCode(201, new { status = "success", message = "Região alterada com Sucesso" });
                }
                else
                {
                    return StatusCode(400, new { status = "error", message = "Falha ao alterar cidades" });
                }
            }
            else
            {
                return StatusCode(400, new { status = "error", message = "Falha ao alterar região" });
            }
        }


        [Route("Delete")]
        [HttpDelete]
        public IActionResult DeleteRegiao([FromHeader] Guid regiaoid)
        {
            var regiao = _regiaoService.buscaByID(regiaoid);
            if (regiao == null)
            {
                return StatusCode(404, new { success = false, message = "Região não encontrada." });
            }
            else
            {
                bool deletarResult = _regiaoService.deletarRegiao(regiaoid);
                if (deletarResult)
                {

                    return StatusCode(201, new { status = "success", message = "Região deletada com Sucesso" });
                }
                else
                {

                    return StatusCode(400, new { status = "error", message = "Falha ao deletar região" });
                }
            }

        }

        [Route("get-excel")]
        [HttpGet]
        public IActionResult GetRegiaoCidadeExcel([FromHeader] Guid? idRegiao, [FromHeader] bool? incluirInativas)
        {
            bool incluirInativasFlag = incluirInativas ?? false;

            var regioesQuery = _regiaoService.List().AsQueryable();
            if (!incluirInativasFlag)
                regioesQuery = regioesQuery.Where(r => r.Ativa == true);

            List<Regiao> regioes;
            if (idRegiao == null || idRegiao == Guid.Empty)
            {
                regioes = regioesQuery.ToList();
            }
            else
            {
                regioes = regioesQuery.Where(r => r.Id == idRegiao.Value).ToList();
                if (!regioes.Any())
                    return NotFound(new { success = false, message = "Região não encontrada." });
            }

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Regiões");

            worksheet.Cell(1, 1).Value = "Região Id";
            worksheet.Cell(1, 2).Value = "Região Nome";
            worksheet.Cell(1, 3).Value = "Cidade Id";
            worksheet.Cell(1, 4).Value = "Cidade Nome";
            worksheet.Cell(1, 5).Value = "Cidade UF";

            int currentRow = 2;

            foreach (var regiao in regioes)
            {
                var regiaoCidades = _regiaoCidadeService.listByRegiaoId(regiao.Id).ToList();
                var cidadeIds = regiaoCidades.Select(rc => rc.CidadeID).ToList();
                var cidades = _cidadeService.List()
                    .Where(c => cidadeIds.Contains(c.Id))
                    .ToList();

                if (cidades.Any())
                {
                    foreach (var cidade in cidades)
                    {
                        worksheet.Cell(currentRow, 1).Value = regiao.Id.ToString();
                        worksheet.Cell(currentRow, 2).Value = regiao.Nome;
                        worksheet.Cell(currentRow, 3).Value = cidade.Id.ToString();
                        worksheet.Cell(currentRow, 4).Value = cidade.Nome;
                        worksheet.Cell(currentRow, 5).Value = cidade.UF;
                        currentRow++;
                    }
                }
                else
                {
                    worksheet.Cell(currentRow, 1).Value = regiao.Id.ToString();
                    worksheet.Cell(currentRow, 2).Value = regiao.Nome;
                    worksheet.Cell(currentRow, 3).Value = "";
                    worksheet.Cell(currentRow, 4).Value = "";
                    worksheet.Cell(currentRow, 5).Value = "";
                    currentRow++;
                }
            }

            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            string excelName = $"regioes-{DateTime.Now:yyyyMMddHHmmss}.xlsx";

            return File(stream,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        excelName);
        }




    }
}
