using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string AccessibilityFeatures { get; set; }
        public ICollection<Room> Rooms { get; set; }

    }
}