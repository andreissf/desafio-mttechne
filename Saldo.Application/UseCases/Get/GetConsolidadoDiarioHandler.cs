using MediatR;
using Relatorio.Infrastructure.Repository;

namespace Saldo.Application.UseCases.Get;

public class GetConsolidadoDiarioHandler : IRequestHandler<GetConsolidadoDiarioCommand, Domain.Saldo>
{
    private readonly ISaldoRepository _saldoRepository;
    
    public GetConsolidadoDiarioHandler(ISaldoRepository saldoRepository)
    {
        _saldoRepository = saldoRepository;
    }
    
    public async Task<Domain.Saldo> Handle(GetConsolidadoDiarioCommand request, CancellationToken cancellationToken)
    {
        var saldo = await _saldoRepository.GetByDate(request.Date);
        return saldo;
    }
}