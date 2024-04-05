using ErrorOr;
using GymManagement.Domain.Gyms;

namespace GymManagement.Domain.Subscriptions;

public class Subscription
{
    private readonly List<Guid> _gymIds = new();

    private readonly int _maxGyms = 1;
    
    private readonly Guid _adminId;
    public Guid Id { get; private set; }
    public SubscriptionType SubscriptionType { get; private set; }

    public Subscription(
        SubscriptionType subscriptionType,
        Guid adminId,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        _adminId = adminId;
        SubscriptionType = subscriptionType;

        _maxGyms = GetMaxGyms();
    }

    public ErrorOr<Success> AddGym(Gym gym)
    {
        if (_gymIds.Contains(gym.Id)) return new Error();
        if (_gymIds.Count >= _maxGyms) return new Error();
        
        _gymIds.Add(gym.Id);

        return Result.Success;
    }
    public int GetMaxRooms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 3,
        nameof(SubscriptionType.Pro) => 10,
        _ => throw new InvalidOperationException()
    };
    
    public int GetMaxGyms() => SubscriptionType.Name switch
    {
        nameof(SubscriptionType.Free) => 1,
        nameof(SubscriptionType.Starter) => 2,
        nameof(SubscriptionType.Pro) => 3,
        _ => throw new InvalidOperationException()
    };

    public bool HasGym(Guid gymId)
    {
        return _gymIds.Contains(gymId);
    }

    public void RemoveGym(Guid gymId)
    {
        _gymIds.Remove(gymId);
    }
    
    private Subscription() {}
}