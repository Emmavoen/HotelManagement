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
        public IInvoinceRepository InvoinceRepository {get;}
        public IPaymentRepository PaymentRepository {get;}
        public IRefundMethodRepository RefundMethodRepository {get;}
        public IRefundRepository RefundRepository {get;}
        public IRoomRepository RoomRepository {get;}
        public IRoomTypeRepository RoomTypeRepository {get;}
        public IServiceRepository ServiceRepository {get;}
        

        

        public UnitOfWork(AppDbContext context, IUserRepository userRepository, IServiceRepository serviceRepository,
         IBookingRepository bookingRepository,IRoomTypeRepository roomTypeRepository, IFeedbackRepository feedbackRepository, 
         IRoomRepository roomRepository, IRefundRepository refundRepository, IRefundMethodRepository refundMethodRepository,
         IAmenityRepository amenityRepository, IPaymentRepository paymentRepository, IInvoinceRepository invoinceRepository
         )
        {
            _context = context;
            UserRepository = userRepository;
            ServiceRepository = serviceRepository;
            BookingRepository = bookingRepository;
            RoomTypeRepository = roomTypeRepository;
            FeedbackRepository = feedbackRepository;
            RoomRepository = roomRepository;
            RefundRepository = refundRepository;
            RefundMethodRepository = refundMethodRepository;
            AmenityRepository = amenityRepository;
            PaymentRepository = paymentRepository;
            InvoinceRepository = invoinceRepository;


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