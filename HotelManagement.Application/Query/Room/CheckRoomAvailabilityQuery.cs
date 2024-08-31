using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using MediatR;

namespace HotelManagement.Application.Query.Room
{
    public class CheckRoomAvailabilityQuery : IRequest<bool>
    {
        internal readonly int roomId;
        internal readonly DateTime checkInDate;
        internal readonly DateTime checkOutDate;

        public CheckRoomAvailabilityQuery(int roomId, DateTime checkInDate, DateTime checkOutDate)
        {
            this.roomId = roomId;
            this.checkInDate = checkInDate;
            this.checkOutDate = checkOutDate;
        }
    }

    public class CheckRoomAvailabilityQueryHandler : IRequestHandler<CheckRoomAvailabilityQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckRoomAvailabilityQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> Handle(CheckRoomAvailabilityQuery request, CancellationToken cancellationToken)
        {
            var conflictingBookings = await _unitOfWork.BookingRepository.GetAllByColumn(b => 
            b.RoomId == request.roomId &&
            b.CheckInDate < request.checkOutDate && 
            b.CheckOutDate > request.checkInDate);

            return !conflictingBookings.Any(); // returns true if no no conflicting booking
        }
    }
}