using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Persistence.Configuration
{
    public class UserConfiguration :IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            // Configure the primary key
            builder.HasKey(e => e.Id);
            // Configure properties
            builder.Property(e => e.)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
        }
        
    }
}