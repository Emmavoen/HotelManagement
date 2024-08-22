﻿using System;
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

    }
}