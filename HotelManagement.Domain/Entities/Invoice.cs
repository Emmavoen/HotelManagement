using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Domain.Entities
{
    public class Invoice
    {
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime IssueDate { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Booking> Bookings { get; set; }
    }
}