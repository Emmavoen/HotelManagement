using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Application.Dtos.Request
{
    public class BookingRequestDto
    {
        public DateTime BookingDate { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NumberOfOcupant { get; set; }
        //public string Status { get; set; }
        public string UserId { get; set; }
        public int RoomId { get; set; }
        
        public int PaymentId { get; set; }
    }
}