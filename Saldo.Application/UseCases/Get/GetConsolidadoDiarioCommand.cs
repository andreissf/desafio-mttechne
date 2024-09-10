using MediatR;

namespace Saldo.Application.UseCases.Get;

public class GetConsolidadoDiarioCommand : IRequest<Domain.Saldo>
{
    public DateTime Date { get; private set; }
    
    public GetConsolidadoDiarioCommand()
    {
        var datetime = DateTime.Now;
        Date = new DateTime(datetime.Year, datetime.Month, datetime.Day);
    }
}