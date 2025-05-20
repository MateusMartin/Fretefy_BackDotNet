using System;

namespace Fretefy.Test.Domain.Entities
{
    public class Regiao : IEntity
    {
        public Regiao()
        {

        }

        public Regiao(string nome)
        {
            Id = Guid.NewGuid();
            Nome = nome;
        }

        public Guid Id { get; set; }

        public string Nome { get; set; }

        public bool? Ativa { get; set; }

    }
}
