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
        IEnumerable<Regiao> Query(string terms);
        bool exissteNome(string nome);
        (bool, string) insertRegiao(string nome);

    }
}
