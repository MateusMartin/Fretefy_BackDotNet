using Fretefy.Test.Domain.Entities;
using Fretefy.Test.Domain.Interfaces;
using Fretefy.Test.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        public CidadeService(ICidadeRepository cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;

        }

        public Cidade Get(Guid id)
        {
            return _cidadeRepository.List().FirstOrDefault(f => f.Id == id);
        }

        public IEnumerable<Cidade> List()
        {
            return _cidadeRepository.List();
        }

        public IEnumerable<Cidade> ListByUf(string uf)
        {
            return _cidadeRepository.ListByUf(uf);
        }

        public IEnumerable<Cidade> Query(string terms)
        {
            return _cidadeRepository.Query(terms);
        }

        public async Task PopularCidadesAsync()
        {

            using var httpClient = new HttpClient();

            var response = await httpClient.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/municipios");
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var array = JArray.Parse(json);

            var cidades = array
                .Select(item => new Cidade(
                item["nome"]?.ToString(),
                item.SelectToken("microrregiao.mesorregiao.UF.sigla")?.ToString()
            ))
            .Where(c => !string.IsNullOrWhiteSpace(c.Nome) && !string.IsNullOrWhiteSpace(c.UF))
            .ToList();

            await _cidadeRepository.CreateRangeAsync(cidades);
        }


    }
}
