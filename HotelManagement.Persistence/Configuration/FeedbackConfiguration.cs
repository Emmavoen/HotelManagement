using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Persistence.Configuration
{
    public class FeedbackConfiguration : IEntityTypeConfiguration<Feedback>
    {
        public void Configure(EntityTypeBuilder<Feedback> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Comments).IsRequired().HasMaxLength(300);
            builder.Property(x => x.DateSubmitted).IsRequired();
            builder.Property(x => x.Rating).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Feedbacks).HasForeignKey(x => x.UserId);
        }
    }
}