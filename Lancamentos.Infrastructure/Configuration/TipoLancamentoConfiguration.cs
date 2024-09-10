using Lancamentos.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lancamentos.Infrastructure.Configuration;

public class TipoLancamentoConfiguration : IEntityTypeConfiguration<TipoLancamento>
{
    public void Configure(EntityTypeBuilder<TipoLancamento> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Tipo)
            .IsRequired()
            .HasColumnType("varchar(10)")
            .HasMaxLength(10);
        
        builder.HasMany(x => x.Lancamentos)
            .WithOne(x => x.TipoLancamento)
            .HasForeignKey(x => x.TipoLancamentoId);
    }
}