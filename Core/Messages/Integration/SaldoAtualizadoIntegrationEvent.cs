namespace Core.Messages.Integration;

public class SaldoAtualizadoIntegrationEvent : IntegrationEvent
{
    public decimal Saldo { get; private set; }

    public SaldoAtualizadoIntegrationEvent(decimal saldo)
    {
        Saldo = saldo;
    }
}