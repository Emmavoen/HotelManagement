using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Persistence.Configuration
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(n => n.Name).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Description).IsRequired().HasMaxLength(200);
            builder.Property(n => n.IsActive).IsRequired().HasMaxLength(50);
            builder.HasOne(a => a.RoomAmenity).WithMany(a => a.Amenities);
        }
    }
}