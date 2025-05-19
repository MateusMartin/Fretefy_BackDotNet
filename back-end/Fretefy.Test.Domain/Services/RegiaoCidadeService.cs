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
    public class RegiaoCidadeService : IRegiaoCidadeService
    {


        private readonly IRegiaoCidadeRepository _regiaoCidadeRepository;
        public RegiaoCidadeService(IRegiaoCidadeRepository regiaoCidadeRepository)
        {
            _regiaoCidadeRepository = regiaoCidadeRepository;

        }

        public RegiaoCidade Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool insertRegiaoCidade(List<Guid> cidades, string regiao)
        {
            return _regiaoCidadeRepository.insertRegiaoCidade(cidades, regiao);
        }

        public IEnumerable<RegiaoCidade> List()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RegiaoCidade> Query(string terms)
        {
            throw new NotImplementedException();
        }


    }
}
