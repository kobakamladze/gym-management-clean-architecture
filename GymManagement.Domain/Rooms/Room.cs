namespace GymManagement.Domain.Rooms;

public class Room
{
    public Guid Id { get; }
    public string Name { get; set; }
    public Guid GymId { get; }
    public int MaxDailySessions { get; }

    public Room(
        string name,
        Guid gymId,
        int maxDailySessions,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        GymId = gymId;
        MaxDailySessions = maxDailySessions;
    }
    
    
}