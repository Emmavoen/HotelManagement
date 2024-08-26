using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Persistence.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.CheckInDate)
                .IsRequired();

            builder.Property(b => b.CheckOutDate)
                .IsRequired();

                builder.Property(b => b.BookingDate)
                .IsRequired();

                builder.Property(b => b.NumberOfOcupant)
                .IsRequired();

            builder.HasOne(b => b.User)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.UserId);

            builder.HasOne(b => b.Room)
                .WithOne(r => r.Booking)
                .HasForeignKey<Booking>(b => b.RoomId);

            builder.HasOne(b => b.Payment)
                .WithOne(p => p.Booking)
                .HasForeignKey<Booking>(p => p.PaymentId);

        }

        
    }

}