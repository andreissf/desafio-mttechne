using Lancamentos.Domain;
using MediatR;

namespace Lancamentos.Application.UseCases.Update;

public class UpdateLancamentoCommand : IRequest<Lancamento>
{
    public Guid Id { get; set; }
    public decimal Valor { get; set; }
    public string Descricao { get; set; } = default!;
}