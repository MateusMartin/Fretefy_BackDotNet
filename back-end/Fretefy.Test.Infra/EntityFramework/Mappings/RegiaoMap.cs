using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
    public class RegiaoMap : IEntityTypeConfiguration<Regiao>
    {
        public void Configure(EntityTypeBuilder<Regiao> builder)
        {
            builder.ToTable("Regiao");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
              .IsRequired()
              .HasMaxLength(1024);


            builder.Property(r => r.Ativa)
                .IsRequired()
                .HasDefaultValue(true);

        }
    }
}
