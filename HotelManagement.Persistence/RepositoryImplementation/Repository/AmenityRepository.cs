using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Domain.Entities;
using HotelManagement.Persistence.DataBaseContext;
using HotelManagement.Persistence.RepositoryImplementation.GenericRepository;

namespace HotelManagement.Persistence.RepositoryImplementation.Repository
{
    public class AmenityRepository : GenericRepository<Amenity> , IAmenityRepository
    {
        public AmenityRepository(AppDbContext _context) : base(_context)
        {
            
        }
    }
}