using System.Reflection;
using Core.Data;
using Microsoft.EntityFrameworkCore;
using Saldo.Infrastructure.Entities;

namespace Saldo.Infrastructure;

public class SaldoDbContext : DbContext, IUnitOfWork
{
    public SaldoDbContext(DbContextOptions<SaldoDbContext> options) : base(options)
    {
        ChangeTracker.AutoDetectChangesEnabled = false;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    public DbSet<Entities.Saldo> Saldos { get; set; }

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