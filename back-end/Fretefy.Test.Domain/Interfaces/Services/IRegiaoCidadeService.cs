using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoCidadeService
    {

        bool insertRegiaoCidade(List<Guid> cidades, string regiao);

        bool alterarRegiaocidade(List<Guid> cidades, Guid regiao);

        List<RegiaoCidade> listByRegiaoId(Guid regiaoId);

    }
}
