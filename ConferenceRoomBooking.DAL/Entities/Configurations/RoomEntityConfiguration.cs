using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConferenceRoomBooking.DAL.Entities.Configurations;

public class RoomEntityConfiguration : IEntityTypeConfiguration<RoomEntity>
{
    public void Configure(EntityTypeBuilder<RoomEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.Property(r => r.BasePricePerHour)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.HasMany(b => b.AvaliableServices)
            .WithMany(s => s.Rooms);
    }
}