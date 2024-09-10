namespace Core.Messages.Integration;

public class LancamentoRealizadoIntegrationEvent : IntegrationEvent
{
    public Guid LancamentoId { get; private set; }
    public decimal Valor { get; private set; }
    public DateTime Data { get; private set; }

    public LancamentoRealizadoIntegrationEvent(Guid lancamentoId, decimal valor, DateTime data)
    {
        LancamentoId = lancamentoId;
        Valor = valor;
        Data = new DateTime(data.Year, data.Month, data.Day, 0, 0, 0);
    }
}