using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Domain.Entities;
using HotelManagement.Persistence.DataBaseContext;
using HotelManagement.Persistence.RepositoryImplementation.GenericRepository;

namespace HotelManagement.Persistence.RepositoryImplementation.Repository
{
    public class FeedbackRepository : GenericRepository<Feedback> , IFeedbackRepository
    {
        public FeedbackRepository(AppDbContext _context) : base(_context)
        {
            
        }
    }
}