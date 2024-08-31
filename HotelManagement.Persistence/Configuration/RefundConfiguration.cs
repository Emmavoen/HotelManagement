using HotelManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelManagement.Persistence.Configuration
{
    public class RefundConfiguration : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Amount).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PaymentReference).IsRequired().HasMaxLength(200);
            builder.Property(x => x.AccountNumber).IsRequired();
            builder.Property(x => x.AccountName).IsRequired();
            builder.Property(x => x.DateIssued).IsRequired();
        
        }
    }
}