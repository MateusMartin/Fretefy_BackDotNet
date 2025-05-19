using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoService
    {
        IEnumerable<Regiao> List();
        IEnumerable<Regiao> Query(string terms);
        (bool, string) insertRegiao(string regiao);

        bool exissteNome(string nome);
    }
}
