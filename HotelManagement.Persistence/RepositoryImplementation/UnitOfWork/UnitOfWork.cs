using System.Threading.Tasks;
using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Persistence.DataBaseContext;

namespace HotelManagement.Persistence.RepositoryImplementation.UnitOfWork
{
    public class UnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository UserRepository {get;}

        public UnitOfWork(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            UserRepository = userRepository;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}