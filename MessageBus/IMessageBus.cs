using Core.Messages.Integration;
using EasyNetQ;
using NSE.Core.Messages.Integration;

namespace MessageBus;

public interface IMessageBus : IDisposable
{
    bool IsConnected { get; }
    IAdvancedBus AdvancedBus { get; }

    void Publish<T>(T message) where T : IntegrationEvent;

    Task PublishAsync<T>(T message) where T : IntegrationEvent;

    void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

    void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;
}