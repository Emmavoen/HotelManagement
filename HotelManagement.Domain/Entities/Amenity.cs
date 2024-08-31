using System.Collections.Generic;

namespace HotelManagement.Domain.Entities
{
    public class Amenity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public int RoomAmenityId { get; set; }
        public RoomAmenity RoomAmenity { get; set; }
        //public ICollection<Room> Rooms { get; set; }

    }

    public class ResDto
    {
        public int Id { get; set;}
        public string RoomId {get; set;}
        public List<RAmenities> RoomAnmenities {get; set;}

    }

    public class RAmenities
    {
        public int Id { get; set;}
        public string Name {get; set;}
    }


}