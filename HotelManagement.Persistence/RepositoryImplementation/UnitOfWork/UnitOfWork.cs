using System.Threading.Tasks;
using HotelManagement.Application.Contracts.Repository;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Persistence.DataBaseContext;

namespace HotelManagement.Persistence.RepositoryImplementation.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserRepository UserRepository {get;}
        public IAmenityRepository AmenityRepository {get;}
        public IBookingRepository BookingRepository {get;}
        public IFeedbackRepository FeedbackRepository {get;}
        public IPaymentRepository PaymentRepository {get;}
        public IRefundRepository RefundRepository {get;}
        public IRoomRepository RoomRepository {get;}
        public IRoomTypeRepository RoomTypeRepository {get;}
        public IRoomAmenityRepository RoomAmenityRepository {get;}
        

        

        public UnitOfWork(AppDbContext context, IUserRepository userRepository, 
         IBookingRepository bookingRepository,IRoomTypeRepository roomTypeRepository, IFeedbackRepository feedbackRepository, 
         IRoomRepository roomRepository, IRefundRepository refundRepository,
         IAmenityRepository amenityRepository, IPaymentRepository paymentRepository
         ,IRoomAmenityRepository roomAmenityRepository
         )
        {
            _context = context;
            UserRepository = userRepository;
            BookingRepository = bookingRepository;
            RoomTypeRepository = roomTypeRepository;
            FeedbackRepository = feedbackRepository;
            RoomRepository = roomRepository;
            RefundRepository = refundRepository;
            AmenityRepository = amenityRepository;
            PaymentRepository = paymentRepository;
            RoomAmenityRepository = roomAmenityRepository;


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