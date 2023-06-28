using SocialMedia.EventSourcing.Events;

namespace SocialMedia.EventSourcing.Domain;

public abstract class AggregateRoot
{
    private readonly List<BaseEvent> _changes = new();

    protected Guid _id;
    public Guid Id => _id;

    public int Version { get; set; } = -1;

    public IEnumerable<BaseEvent> GetUncommittedChanges()
        => _changes;

    public void MarkChangesAsCommitted()
        => _changes.Clear();

    public void ReplayEvents(IEnumerable<BaseEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChange(@event, false);
        }
    }
    
    protected void RaiseEvent(BaseEvent @event)
    {
        ApplyChange(@event, true);
    }

    private void ApplyChange(BaseEvent @event, bool isNew)
    {
        var method = GetType().GetMethod("Apply", new[] {@event.GetType()});

        if (method == null)
        {
            throw new ArgumentNullException(nameof(method),
                $"The 'Apply' method was not found in the aggregate for {@event.GetType()}");
        }

        method.Invoke(this, new object[] {@event});

        if (isNew)
        {
            _changes.Add(@event);
        }
    }
}