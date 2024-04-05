using ErrorOr;
using GymManagement.Domain.Rooms;

namespace GymManagement.Domain.Gyms;

public class Gym
{
    private readonly int _maxRooms;
    public Guid Id { get; }
    private readonly List<Guid> _roomIds = new();
    private readonly List<Guid> _trainerIds = new();
    public string Name { get; set; }
    public Guid SubscriptionId { get; init; }
    
    public Gym(
        string name,
        int maxRooms,
        Guid subscriptionId,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        _maxRooms = maxRooms;
        this.SubscriptionId = subscriptionId;
    }

    public ErrorOr<Success> AddRoom(Room room)
    {
        if (_roomIds.Contains(room.Id)) 
            return new Error();

        if (_roomIds.Count >= _maxRooms)
            return new Error();
            
        _roomIds.Add(room.Id);

        return Result.Success;
    }
    
    public bool HasRoom(Guid roomId)
    {
        return _roomIds.Contains(roomId);
    }

    public ErrorOr<Success> AddTrainer(Guid trainerId)
    {
        if (_trainerIds.Contains(trainerId))
            return new Error();
        
        _trainerIds.Add(trainerId);
        
        return Result.Success;
    }
    
    public bool HasTrainer(Guid trainerId)
    {
        return _trainerIds.Contains(trainerId);
    }

    public void RemoveRoom(Guid roomId)
    {
        _roomIds.Remove(roomId);
    }

    private Gym() { }
}