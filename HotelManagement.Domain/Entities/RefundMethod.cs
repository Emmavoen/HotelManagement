using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Domain.Entities
{
    public class RefundMethod
    {
    public int Id { get; set; }
    public string Name { get; set; } // e.g., Credit Card, Bank Transfer

    
    public ICollection<Refund> Refunds { get; set; }
    }
}