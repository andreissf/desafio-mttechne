using Core.Data;

namespace Saldo.Domain;

public class Saldo : Entity
{
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    

    public void AdicionarValor(decimal messageValor)
    {
        Valor += messageValor;
    }
}