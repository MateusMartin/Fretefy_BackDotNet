using Fretefy.Test.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fretefy.Test.Domain.Interfaces
{
    public interface IRegiaoCidadeService
    {
        RegiaoCidade Get(Guid id);
        IEnumerable<RegiaoCidade> List();
        IEnumerable<RegiaoCidade> Query(string terms);

        bool insertRegiaoCidade(List<Guid> cidades, string regiao);
    }
}
