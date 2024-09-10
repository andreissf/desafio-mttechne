using Core.Data;

namespace Saldo.Infrastructure.Entities;

public class Saldo : Entity
{
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
}