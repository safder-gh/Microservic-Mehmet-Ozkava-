namespace BuildingBlocks.Messaging.Events;

public abstract record  IntegrationEvent
    {
    public Guid Guid => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;

    }

