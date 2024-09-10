using Core.Data;

namespace Lancamentos.Domain;

public class Lancamento : Entity
{
    public Guid TipoLancamentoId { get; set; }
    public decimal Valor { get; set; }
    public DateTime Data { get; set; }
    public string Descricao { get; set; } = default!;
}