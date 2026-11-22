using System.Text.Json.Serialization;

namespace BackendPractice.Models;

public class Zone
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Meter> Meters { get; set; } = new List<Meter>();
}
