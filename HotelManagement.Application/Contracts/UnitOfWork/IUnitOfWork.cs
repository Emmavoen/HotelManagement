using System;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.Repository;

namespace HotelManagement.Application.Contracts.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //IPostRepository PostRepository{ get; }
        //IBlogRepository BlogRepository{ get; }
        IUserRepository UserRepository{ get; }
        IAmenityRepository AmenityRepository{ get; }
        IBookingRepository BookingRepository { get; }
        IFeedbackRepository FeedbackRepository{ get; }
        IPaymentRepository PaymentRepository { get; }
        IRefundRepository RefundRepository{ get; }
        IRoomRepository RoomRepository  { get; }
        IRoomTypeRepository RoomTypeRepository{ get; }

        IRoomAmenityRepository RoomAmenityRepository { get; }
        //IStateRepository StateRepository { get; }



        Task<int> Save();
    }
}