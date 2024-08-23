using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class RoomType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string Description { get; set; }
        public string AccessibilityFeatures { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public string ModifiedDate { get; set; }
        public ICollection<Room> Rooms { get; set; }

    }
}