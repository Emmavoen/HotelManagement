namespace HotelManagement.Domain.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string BedType { get; set; }
        public string ViewType { get; set; }
        public string status { get; set; }
        public string IsActive  { get; set; }
        public string DateCreated { get; set; }
        public string RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
    }
}