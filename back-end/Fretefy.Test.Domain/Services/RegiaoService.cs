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

        public bool alteraRegiao(Guid id, string nome)
        {
            return _regiaoRepository.alteraRegiao(id, nome);
        }

        public bool ativarRegiao(Guid id)
        {
            return _regiaoRepository.ativarRegiao(id);

        }

        public Regiao buscaByID(Guid id)
        {
            return _regiaoRepository.List().FirstOrDefault(x => x.Id == id);
        }

        public bool deletarRegiao(Guid id)
        {
            return _regiaoRepository.deletarRegiao(id);
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
            return _regiaoRepository.List();
        }

        public bool RemoverRegiao(Guid id)
        {
            return _regiaoRepository.RemoverRegiao(id);
        }
    }
}
