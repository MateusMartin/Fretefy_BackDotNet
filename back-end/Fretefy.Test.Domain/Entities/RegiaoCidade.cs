using System;

namespace Fretefy.Test.Domain.Entities
{
    public class RegiaoCidade : IEntity
    {
        public RegiaoCidade()
        {

        }

        public RegiaoCidade(Guid idCidade, Guid idRegiao)
        {
            Id = Guid.NewGuid();
            CidadeID = idCidade;
            RegiaoID = idRegiao;
        }


        public Guid Id { get; set; }

        public Guid CidadeID { get; set; }

        public Guid RegiaoID { get; set; }
    }
}
