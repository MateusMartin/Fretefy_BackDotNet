using System;
using System.Collections.Generic;

namespace Fretefy.Test.Domain.Entities.RequestEntites
{
    public class RegiaoPutRequest
    {
        public Guid id { get; set; }

        public string nome { get; set; }
        public List<Guid>? Cidades { get; set; }

    }

}
