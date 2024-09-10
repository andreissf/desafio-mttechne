using Lancamentos.Domain;
using MediatR;

namespace Lancamentos.Application.UseCases.Get;

public class GetLancamentoCommand : IRequest<Lancamento>
{
    public Guid LancamentoId { get; set; }
}