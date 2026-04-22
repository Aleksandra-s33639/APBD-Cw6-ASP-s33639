using APBD_Cw6_ASP_s33639.Models;

namespace APBD_Cw6_ASP_s33639.Data;

public static class InMemoryDataStore
{
    public static List<Room> Rooms { get; } =
    [
        new Room
        {
            Id = 1,
            Name = "Room A101",
            BuildingCode = "A",
            Floor = 1,
            Capacity = 20,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 2,
            Name = "Lab B202",
            BuildingCode = "B",
            Floor = 2,
            Capacity = 24,
            HasProjector = true,
            IsActive = true
        },
        new Room
        {
            Id = 3,
            Name = "Workshop C303",
            BuildingCode = "C",
            Floor = 3,
            Capacity = 16,
            HasProjector = false,
            IsActive = true
        },
        new Room
        {
            Id = 4,
            Name = "Conference A255",
            BuildingCode = "A",
            Floor = 2,
            Capacity = 40,
            HasProjector = true,
            IsActive = false
        },
        new Room
        {
            Id = 5,
            Name = "Training B155",
            BuildingCode = "B",
            Floor = 1,
            Capacity = 12,
            HasProjector = false,
            IsActive = true
        }
    ];

    public static List<Reservation> Reservations { get; } =
    [
        new Reservation
        {
            Id = 1,
            RoomId = 1,
            OrganizerName = "Anna Kwiatkowska",
            Topic = "ASP.NET Core Basics",
            Date = new DateOnly(2026, 4, 10),
            StartTime = new TimeOnly(9, 0),
            EndTime = new TimeOnly(11, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 2,
            RoomId = 2,
            OrganizerName = "Jan Kowalski",
            Topic = "REST API Workshop",
            Date = new DateOnly(2026, 4, 10),
            StartTime = new TimeOnly(10, 0),
            EndTime = new TimeOnly(12, 30),
            Status = "planned"
        },
        new Reservation
        {
            Id = 3,
            RoomId = 3,
            OrganizerName = "Maria Wesolowska",
            Topic = "Git Training",
            Date = new DateOnly(2026, 5, 11),
            StartTime = new TimeOnly(8, 30),
            EndTime = new TimeOnly(10, 0),
            Status = "confirmed"
        },
        new Reservation
        {
            Id = 4,
            RoomId = 5,
            OrganizerName = "Piotr Wiśniewski",
            Topic = "Postman Practice",
            Date = new DateOnly(2026, 5, 12),
            StartTime = new TimeOnly(13, 0),
            EndTime = new TimeOnly(15, 0),
            Status = "cancelled"
        },
        new Reservation
        {
            Id = 5,
            RoomId = 1,
            OrganizerName = "Robert Lewandowski",
            Topic = "Routing in Web API",
            Date = new DateOnly(2026, 5, 13),
            StartTime = new TimeOnly(12, 0),
            EndTime = new TimeOnly(14, 0),
            Status = "planned"
        }
    ];
}