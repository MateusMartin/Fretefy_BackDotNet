using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces.Repositories
{
    public interface IRegiaoCidadeRepository
    {
        IQueryable<RegiaoCidade> List();

        bool insertRegiaoCidade(List<Guid> cidades, string regiao);

        bool alterarRegiaocidade(List<Guid> cidades, Guid regiao);
    }
}
