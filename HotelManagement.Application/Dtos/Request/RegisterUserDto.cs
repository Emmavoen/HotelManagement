using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Application.Dtos.Request
{
    public class RegisterUserDto
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgeGroup { get; set; }
        public string Address { get; set; }
        public string StateId { get; set; }

    }
}
