namespace HotelManagement.Application.Dtos.Response
{
    public class AmenityResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public int RoomAmenityId { get; set; }
    }
}