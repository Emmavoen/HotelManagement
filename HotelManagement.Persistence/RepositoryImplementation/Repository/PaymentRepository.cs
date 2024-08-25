using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Domain.Entities;
using HotelManagement.Persistence.DataBaseContext;
using HotelManagement.Persistence.RepositoryImplementation.GenericRepository;

namespace HotelManagement.Persistence.RepositoryImplementation.Repository
{
    public class PaymentRepository : GenericRepository<User> , IUserRepository
    {
        public PaymentRepository(AppDbContext _context) : base(_context)
        {
            
        }
    }
}