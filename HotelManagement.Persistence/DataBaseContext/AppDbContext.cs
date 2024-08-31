using HotelManagement.Domain.Entities;
using HotelManagement.Persistence.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Persistence.DataBaseContext
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public  DbSet<Amenity> Amenities { get;}
        public  DbSet<Booking> Bookings { get;}
        public  DbSet<Feedback> Feedbacks { get;}
        public  DbSet<Payment> Payments { get;}
        public  DbSet<Refund> Refunds { get;}
        public  DbSet<Room>  Rooms{ get;}
        public  DbSet<RoomAmenity> RoomAmenities  { get;}
        public  DbSet<RoomType> RoomTypes  { get;}
        public  DbSet<State> States  { get;}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new AmenityConfiguration());

            modelBuilder.ApplyConfiguration(new BookingConfiguration());

            modelBuilder.ApplyConfiguration(new FeedbackConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new RefundConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StateConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());

        }
    }
}