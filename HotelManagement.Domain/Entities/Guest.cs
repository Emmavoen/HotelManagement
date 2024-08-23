namespace HotelManagement.Domain.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {get; set; }
        public string PhoneNumber { get; set; }
        public string AgeGroup { get; set; }
        public string Address { get; set; }
        public string StateId { get; set; }
        public State State  { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public string CountryId { get; set; }
        public Country Country { get; set; }
    }
}