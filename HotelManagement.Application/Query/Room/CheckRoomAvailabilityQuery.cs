using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Query.Room
{
    public class CheckRoomAvailabilityQuery : IRequest<Result<List<AvailableRoomsResponse>>>
    {
        

        public List<AvailableRooms> Request { get; set; }
        public CheckRoomAvailabilityQuery( List<AvailableRooms> request)
        {
            Request = request;
        }
    }

    public class AvailableRooms
    {
        public int RoomId { get; set; }
        public DateTime CheckinDate { get; set;}
        public DateTime CheckOutDate { get; set;}
    }
public class AvailableRoomsResponse
    {
        public int RoomNumber { get; set; }
        public DateTime CheckinDate { get; set;}
        public DateTime CheckOutDate { get; set;}

        public string Message { get; set; }
    }

    public class CheckRoomAvailabilityQueryHandler : IRequestHandler<CheckRoomAvailabilityQuery, Result<List<AvailableRoomsResponse>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckRoomAvailabilityQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<List<AvailableRoomsResponse>>> Handle(CheckRoomAvailabilityQuery request, 
        CancellationToken cancellationToken)
        {
            List<AvailableRoomsResponse>  resp = new List<AvailableRoomsResponse>();
            foreach(var req in request.Request)
            {
                var conflictingBookings = await _unitOfWork.BookingRepository.GetAllByColumn(b => 
                b.RoomId == req.RoomId &&
                b.CheckInDate < req.CheckOutDate && 
                b.CheckOutDate > req.CheckinDate); 
                if(conflictingBookings.Any())
                {
                    resp.Add(new AvailableRoomsResponse
                    {
                        RoomNumber = 1,
                        CheckinDate = req.CheckinDate,
                        CheckOutDate = req.CheckOutDate,
                        Message = "Room is not available."
                    });
                }
                else
                {
                    resp.Add(new AvailableRoomsResponse
                    {
                        RoomNumber = 1,
                        CheckinDate = req.CheckinDate,
                        CheckOutDate = req.CheckOutDate,
                        Message = "Room is available."
                    });
                }
            }
          return Result<List<AvailableRoomsResponse>>.SuccessResult(resp);

            //return !conflictingBookings.Any(); // returns true if no no conflicting booking
        }
    }
}