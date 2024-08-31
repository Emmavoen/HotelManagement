using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Persistence.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            // Configure the primary key
            //builder.HasKey(e => e.Id);
            // Configure properties
            builder.Property(e => e.Address).IsRequired();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.FirstName).IsRequired();
            builder.Property(e => e.LastName).IsRequired();
            builder.Property(e => e.PhoneNumber).IsRequired();
            builder.Property(e => e.UserName).IsRequired();



            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
        }

    }
}