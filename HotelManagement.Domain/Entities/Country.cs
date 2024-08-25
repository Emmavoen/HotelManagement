using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

        public ICollection<State> States { get; set; }
        public ICollection<User> Users { get; set; }

    }
}