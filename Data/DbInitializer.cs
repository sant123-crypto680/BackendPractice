using BackendPractice.Models;

namespace BackendPractice.Data;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext context)
    {
        // Check if database already has zones
        if (context.Zones.Any())
        {
            return; // DB has already been seeded
        }

        // 1. Create Zones
        var northZone = new Zone
        {
            Name = "North Feeder Zone",
            Code = "ZN-NORTH",
            Description = "Northern industrial and commercial grid",
            CreatedAt = DateTime.UtcNow
        };

        var southZone = new Zone
        {
            Name = "South Residential Zone",
            Code = "ZN-SOUTH",
            Description = "Southern urban residential sector",
            CreatedAt = DateTime.UtcNow
        };

        var eastZone = new Zone
        {
            Name = "East Suburban Zone",
            Code = "ZN-EAST",
            Description = "Eastern residential and township sector",
            CreatedAt = DateTime.UtcNow
        };

        context.Zones.AddRange(northZone, southZone, eastZone);
        await context.SaveChangesAsync();

        // 2. Create Users linked to Zones
        var user1 = new User
        {
            Name = "Rahul Sharma",
            Email = "rahul.sharma@example.com",
            PhoneNumber = "+91-9810012345",
            Address = "A-102 Green Park, Delhi",
            ZoneId = northZone.Id,
            CreatedAt = DateTime.UtcNow
        };

        var user2 = new User
        {
            Name = "Priya Verma",
            Email = "priya.verma@example.com",
            PhoneNumber = "+91-9820023456",
            Address = "B-405 Lake View, Mumbai",
            ZoneId = southZone.Id,
            CreatedAt = DateTime.UtcNow
        };

        var user3 = new User
        {
            Name = "Amit Patel",
            Email = "amit.patel@example.com",
            PhoneNumber = "+91-9830034567",
            Address = "C-12 Tech City, Bengaluru",
            ZoneId = southZone.Id,
            CreatedAt = DateTime.UtcNow
        };

        var user4 = new User
        {
            Name = "Sneha Reddy",
            Email = "sneha.reddy@example.com",
            PhoneNumber = "+91-9840045678",
            Address = "D-304 Hill Ridge, Hyderabad",
            ZoneId = eastZone.Id,
            CreatedAt = DateTime.UtcNow
        };

        var user5 = new User
        {
            Name = "Vikram Singh",
            Email = "vikram.singh@example.com",
            PhoneNumber = "+91-9850056789",
            Address = "E-501 Metro Enclave, Pune",
            ZoneId = eastZone.Id,
            CreatedAt = DateTime.UtcNow
        };

        context.Users.AddRange(user1, user2, user3, user4, user5);
        await context.SaveChangesAsync();

        // 3. Create Meters linked to Zones and Users
        var meters = new List<Meter>
        {
            new Meter
            {
                SerialNumber = "MTR-N-00101",
                MeterType = "Smart",
                Status = "Active",
                ZoneId = northZone.Id,
                UserId = user1.Id,
                InstallationDate = DateTime.UtcNow.AddMonths(-6)
            },
            new Meter
            {
                SerialNumber = "MTR-N-00102",
                MeterType = "Smart",
                Status = "Active",
                ZoneId = northZone.Id,
                UserId = user1.Id,
                InstallationDate = DateTime.UtcNow.AddMonths(-3)
            },
            new Meter
            {
                SerialNumber = "MTR-S-00201",
                MeterType = "Smart",
                Status = "Active",
                ZoneId = southZone.Id,
                UserId = user2.Id,
                InstallationDate = DateTime.UtcNow.AddMonths(-12)
            },
            new Meter
            {
                SerialNumber = "MTR-S-00202",
                MeterType = "Prepaid",
                Status = "Active",
                ZoneId = southZone.Id,
                UserId = user3.Id,
                InstallationDate = DateTime.UtcNow.AddMonths(-4)
            },
            new Meter
            {
                SerialNumber = "MTR-E-00301",
                MeterType = "Smart",
                Status = "Active",
                ZoneId = eastZone.Id,
                UserId = user4.Id,
                InstallationDate = DateTime.UtcNow.AddMonths(-8)
            },
            new Meter
            {
                SerialNumber = "MTR-E-00302",
                MeterType = "Postpaid",
                Status = "Active",
                ZoneId = eastZone.Id,
                UserId = user5.Id,
                InstallationDate = DateTime.UtcNow.AddMonths(-2)
            },
            new Meter
            {
                SerialNumber = "MTR-N-00199",
                MeterType = "Smart",
                Status = "Inactive",
                ZoneId = northZone.Id,
                UserId = null, // Spare unassigned meter
                InstallationDate = DateTime.UtcNow.AddDays(-15)
            },
            new Meter
            {
                SerialNumber = "MTR-S-00299",
                MeterType = "Prepaid",
                Status = "Maintenance",
                ZoneId = southZone.Id,
                UserId = null, // In maintenance
                InstallationDate = DateTime.UtcNow.AddMonths(-18)
            }
        };

        context.Meters.AddRange(meters);
        await context.SaveChangesAsync();
    }
}
