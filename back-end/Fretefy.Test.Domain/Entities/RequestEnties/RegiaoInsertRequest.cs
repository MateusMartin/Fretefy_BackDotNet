using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities.RequestEntites
{
    public class RegiaoInsertRequest
    {
        public string Nome { get; set; }

        public List<Guid> Cidades { get; set; }

    }

}
