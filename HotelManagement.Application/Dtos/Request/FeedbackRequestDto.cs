using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelManagement.Application.Dtos.Request
{
    public class FeedbackRequestDto
    {
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string UserId { get; set; }
    }
}