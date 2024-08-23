using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class State
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsActive { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Guest> Guests { get; set; }
    }
}