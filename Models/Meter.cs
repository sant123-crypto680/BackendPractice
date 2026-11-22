using System.Text.Json.Serialization;

namespace BackendPractice.Models;

public class Meter
{
    public int Id { get; set; }
    public required string SerialNumber { get; set; }
    public string MeterType { get; set; } = "Smart"; // e.g., Smart, Prepaid, Postpaid
    public string Status { get; set; } = "Active";    // e.g., Active, Inactive, Disconnected
    public DateTime InstallationDate { get; set; } = DateTime.UtcNow;

    // Relationship with Zone (Meter belongs to a Zone)
    public int ZoneId { get; set; }
    public Zone? Zone { get; set; }

    // Relationship with User (Meter can be assigned to a User)
    public int? UserId { get; set; }
    public User? User { get; set; }
}
