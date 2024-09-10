using Core.Data;

namespace Lancamentos.Infrastructure.Entities;

public class TipoLancamento : Entity
{
    public string Tipo { get; set; }
    public IEnumerable<Lancamento>? Lancamentos { get; set; }
}