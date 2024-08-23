namespace HotelManagement.Application.Dtos.Request
{
    public class UpdateUserDto
    {
        public string  Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string UserName { get; set; }
    }
}