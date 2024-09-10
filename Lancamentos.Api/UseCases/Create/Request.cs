namespace Lancamentos.Api.UseCases.Create;

public class CreateLancamentoRequest
{
    public Guid TipoLancamentoId { get; set; }
    public string Descricao { get; set; } = default!;
    public decimal Valor { get; set; }
}