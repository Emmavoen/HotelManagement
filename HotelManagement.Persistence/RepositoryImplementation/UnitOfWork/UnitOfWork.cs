using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Persistence.DataBaseContext;

namespace HotelManagement.Persistence.RepositoryImplementation.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IUserRepository _userRepository;

        public UnitOfWork(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
        }
    }
}