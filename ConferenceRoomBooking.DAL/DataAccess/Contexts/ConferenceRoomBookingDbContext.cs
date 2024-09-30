using ConferenceRoomBooking.DAL.Entities;
using ConferenceRoomBooking.DAL.Entities.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomBooking.DAL.DataAccess.Contexts;

public class ConferenceRoomBookingDbContext : DbContext
{
    public DbSet<RoomEntity> Rooms { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<BookingEntity> Bookings { get; set; }
    public DbSet<UserEntity> Users { get; set; }

    public ConferenceRoomBookingDbContext(DbContextOptions<ConferenceRoomBookingDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookingEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoomEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        
        // Initial data for Rooms
        modelBuilder.Entity<RoomEntity>().HasData(
            new RoomEntity { Id = Guid.NewGuid(), Name = "Room A", Capacity = 50, BasePricePerHour = 2000.00M },
            new RoomEntity { Id = Guid.NewGuid(), Name = "Room B", Capacity = 100, BasePricePerHour = 3500.00M },
            new RoomEntity { Id = Guid.NewGuid(), Name = "Room C", Capacity = 30, BasePricePerHour = 1500.00M }
        );

        // Initial data for Services
        modelBuilder.Entity<ServiceEntity>().HasData(
            new ServiceEntity { Id = Guid.NewGuid(), Name = "Projector", Price = 500.00M },
            new ServiceEntity { Id = Guid.NewGuid(), Name = "Wi-Fi", Price = 300.00M },
            new ServiceEntity { Id = Guid.NewGuid(), Name = "Sound System", Price = 700.00M }
        );
    }
}
