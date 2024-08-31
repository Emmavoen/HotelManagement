using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Domain.Entities;
using HotelManagement.Persistence.DataBaseContext;
using HotelManagement.Persistence.RepositoryImplementation.GenericRepository;

namespace HotelManagement.Persistence.RepositoryImplementation.Repository
{
    public class RoomAmenityRepository : GenericRepository<RoomAmenity>, IRoomAmenityRepository
    {
        public RoomAmenityRepository(AppDbContext _context) : base(_context)
        {
            
        }
    }
}