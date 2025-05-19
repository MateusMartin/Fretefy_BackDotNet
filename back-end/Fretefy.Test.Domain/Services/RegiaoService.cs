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
    public class RegiaoService : IRegiaoService
    {

        private readonly IRegiaoRepository _regiaoRepository;
        public RegiaoService(IRegiaoRepository regiaoRepository)
        {
            _regiaoRepository = regiaoRepository;

        }

        public bool exissteNome(string nome)
        {
            return _regiaoRepository.exissteNome(nome);
        }

        public (bool, string) insertRegiao(string regiao)
        {
            return _regiaoRepository.insertRegiao(regiao);
        }

        public IEnumerable<Regiao> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Regiao> ListByUf(string uf)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Regiao> Query(string terms)
        {
            return _regiaoRepository.Query(terms);
        }
    }
}
