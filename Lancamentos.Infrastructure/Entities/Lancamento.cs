using Core.Data;

namespace Lancamentos.Infrastructure.Entities;

public class Lancamento : Entity
{   
    public Guid TipoLancamentoId { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; } = default!;

    public TipoLancamento TipoLancamento { get; set; }
}