using Lancamentos.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lancamentos.Infrastructure.Configuration;

public class LancamentoConfiguration : IEntityTypeConfiguration<Lancamento>
{
    public void Configure(EntityTypeBuilder<Lancamento> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasColumnType("varchar(200)")
            .HasMaxLength(200);
        
        builder.Property(x => x.Data)
            .HasColumnType("datetime2")
            .IsRequired();

        builder.Property(x => x.Valor)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
        
        builder.HasOne<TipoLancamento>(x => x.TipoLancamento)
            .WithMany(x => x.Lancamentos)
            .HasForeignKey(x => x.TipoLancamentoId);
    }
}

