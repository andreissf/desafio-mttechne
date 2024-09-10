namespace Lancamentos.Api.UseCases.Update;

public class UpdateLancamentoRequest
{
    public decimal Valor { get; set; }
    public string Descricao { get; set; } = default!;
}