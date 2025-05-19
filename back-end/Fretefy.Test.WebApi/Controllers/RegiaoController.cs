using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Entities.RequestEntites;
using Fretefy.Test.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Fretefy.Test.WebApi.Controllers
{
    [Route("api/regiao")]
    [ApiController]
    public class Regiaocontroller : ControllerBase
    {
        private readonly IRegiaoService _regiaoService;
        private readonly IRegiaoCidadeService _regiaoCidadeService;

        public Regiaocontroller(IRegiaoService regiaoService, IRegiaoCidadeService regiaoCidadeService)
        {
            _regiaoService = regiaoService;
            _regiaoCidadeService = regiaoCidadeService;
        }


        [Route("insert")]
        [HttpPost]
        public IActionResult InsertRegiao([FromBody] RegiaoInsertRequest regiaoRequest)
        {
            bool existeNome = _regiaoService.exissteNome(regiaoRequest.Nome);
            if (!existeNome)
            {
                (bool, string) regiao = _regiaoService.insertRegiao(regiaoRequest.Nome);
                if (regiao.Item1)
                {
                    bool retorno = _regiaoCidadeService.insertRegiaoCidade(regiaoRequest.Cidades, regiao.Item2);
                    if (retorno == true)
                    {
                        return Ok("Inserido com Sucesso");
                    }
                    else
                    {
                        return BadRequest("Falha ao inserir");

                    }
                }
                else
                {
                    return BadRequest(regiao.Item2);
                }
            }
            else
            {
                return BadRequest("Regiao com nome: " + regiaoRequest.Nome + " já existe na base.");
            }
        }
    }
}
