using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoRepository
    {
        IQueryable<Regiao> List();
        bool exissteNome(string nome);
        (bool, string) insertRegiao(string nome);

        bool alteraRegiao(Guid id, string nome);

        bool deletarRegiao(Guid id);
        bool RemoverRegiao(Guid id);

        bool ativarRegiao(Guid id);
    }
}
