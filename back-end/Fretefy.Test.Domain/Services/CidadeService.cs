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
using Microsoft.Extensions.Options;

namespace Fretefy.Test.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;
        private readonly IbgeSettings _ibgeSettings;
        public CidadeService(ICidadeRepository cidadeRepository, IOptions<IbgeSettings> ibgeSettings)
        {
            _cidadeRepository = cidadeRepository;
            _ibgeSettings = ibgeSettings.Value;

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

            var response = await httpClient.GetAsync(_ibgeSettings.UrlMunicipios);
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
