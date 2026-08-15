using Microsoft.Extensions.Logging;
using Ordering.Domain.Events.Domain;

namespace Ordering.Application.Orders.EventHandlers;

public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreatedEvent>
    {
    public Task Handle(OrderCreatedEvent notification, CancellationToken cancellationToken)
        {
        logger.LogInformation("Domain event handled :{DomainEvent}", notification.GetType().Name);
        return Task.CompletedTask;
        }
    }

