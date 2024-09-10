using Core.Data;

namespace Relatorio.Infrastructure.Repository;

public interface ISaldoRepository : IRepository<Saldo.Domain.Saldo>
{
    Task<Saldo.Domain.Saldo> GetByDate(DateTime date);
    decimal GetUltimoSaldo();
}