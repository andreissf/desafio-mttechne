using Lancamentos.Domain;
using MediatR;

namespace Lancamentos.Application.UseCases.Get;

public class GetLancamentosCommand : IRequest<IEnumerable<Lancamento>>
{
    
}