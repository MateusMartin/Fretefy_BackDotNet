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

        public bool alterarRegiaocidade(List<Guid> cidades, Guid regiao)
        {
            return _regiaoCidadeRepository.alterarRegiaocidade(cidades, regiao);
        }

        public bool insertRegiaoCidade(List<Guid> cidades, string regiao)
        {
            return _regiaoCidadeRepository.insertRegiaoCidade(cidades, regiao);
        }

        public List<RegiaoCidade> listByRegiaoId(Guid regiaoId)
        {
            return _regiaoCidadeRepository.List()
                    .Where(x => x.RegiaoID.Equals(regiaoId))
                    .ToList();
        }
    }
}
