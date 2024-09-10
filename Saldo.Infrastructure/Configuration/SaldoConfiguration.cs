using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Saldo.Infrastructure.Configuration;

public class SaldoConfiguration : IEntityTypeConfiguration<Entities.Saldo>
{
    public void Configure(EntityTypeBuilder<Entities.Saldo> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Valor)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.Data)
            .IsRequired();
    }
}