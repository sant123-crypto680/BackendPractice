using System.Text.Json.Serialization;

namespace BackendPractice.Models;

public class User
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Relationship with Zone (User belongs to a Zone)
    public int? ZoneId { get; set; }
    public Zone? Zone { get; set; }

    // Relationship with Meter (User can have multiple Meters)
    public ICollection<Meter> Meters { get; set; } = new List<Meter>();
}
