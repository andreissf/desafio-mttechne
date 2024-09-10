
using Core.Messages.Integration;
using MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Relatorio.Infrastructure.Repository;

namespace Saldo.Application;

public class SaldoAtualizadoIntegrationHandler : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageBus _bus;
    
    public SaldoAtualizadoIntegrationHandler(
        IServiceProvider serviceProvider,
        IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _bus.SubscribeAsync<LancamentoRealizadoIntegrationEvent>("AtualizacaoSaldo", async request =>
            await AtualizacaoDeSaldo(request));
        
        return Task.CompletedTask;
    }
    
    private async Task AtualizacaoDeSaldo(LancamentoRealizadoIntegrationEvent message)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var saldoRepository = scope.ServiceProvider.GetRequiredService<ISaldoRepository>();
            
            var saldo = await saldoRepository.GetByDate(message.Data);
        
            if (saldo is null)
            {
                var ultimoSaldo = saldoRepository.GetUltimoSaldo();
                saldo = new Domain.Saldo() { Data = message.Data, Valor = ultimoSaldo };
                saldo.AdicionarValor(message.Valor);
                saldoRepository.Add(saldo);
            }
            else
            {
                saldo.AdicionarValor(message.Valor);
                saldoRepository.Update(saldo);
            }
        
            await saldoRepository.UnitOfWork.Commit();
            //Pode enviar que o saldo foi gerado a fins de report, lógica etc...
        }
    }
}