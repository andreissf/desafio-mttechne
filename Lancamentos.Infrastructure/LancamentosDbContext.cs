using System.Reflection;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using Lancamento = Lancamentos.Infrastructure.Entities.Lancamento;
using TipoLancamento = Lancamentos.Infrastructure.Entities.TipoLancamento;

namespace Lancamentos.Infrastructure;

public class LancamentosDbContext : DbContext, IUnitOfWork
{
    public LancamentosDbContext(DbContextOptions<LancamentosDbContext> options) : base(options)
    {
        
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }

    public DbSet<Lancamento> Lancamentos { get; set; }
    public DbSet<TipoLancamento> TipoLancamentos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Entity>();
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public async Task<bool> Commit()
    {
        var sucesso = await base.SaveChangesAsync() > 0;
        return sucesso;
    }
}