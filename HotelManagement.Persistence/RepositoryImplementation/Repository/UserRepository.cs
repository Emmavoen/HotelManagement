using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Domain.Entities;
using HotelManagement.Persistence.DataBaseContext;
using HotelManagement.Persistence.RepositoryImplementation.GenericRepository;

namespace HotelManagement.Persistence.RepositoryImplementation.Repository
{
    public class UserRepository: GenericRepository<User> , IUserRepository
    {
        public UserRepository(AppDbContext _context) : base(_context)
        {
            
        }
    }
}