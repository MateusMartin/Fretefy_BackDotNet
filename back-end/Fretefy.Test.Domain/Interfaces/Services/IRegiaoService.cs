using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoService
    {
        IEnumerable<Regiao> List();

        Regiao buscaByID(Guid id);

        (bool, string) insertRegiao(string regiao);

        bool exissteNome(string nome);

        bool alteraRegiao(Guid id, string nome);

        bool deletarRegiao(Guid id);

        bool RemoverRegiao(Guid id);

        bool ativarRegiao(Guid id);
    }
}
