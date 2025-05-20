using Fretefy.Test.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fretefy.Test.Infra.EntityFramework.Mappings
{
  public class RegiaoCidadeMap : IEntityTypeConfiguration<RegiaoCidade>
  {
    public void Configure(EntityTypeBuilder<RegiaoCidade> builder)
    {
      builder.ToTable("RegiaoCidade");

      builder.HasKey(rc => rc.Id);

      builder.Property(rc => rc.CidadeID)
        .IsRequired();

      builder.Property(rc => rc.RegiaoID)
        .IsRequired();


      builder.HasOne<Cidade>().WithMany().HasForeignKey(rc => rc.CidadeID);
      builder.HasOne<Regiao>().WithMany().HasForeignKey(rc => rc.RegiaoID);
    }
  }
}
