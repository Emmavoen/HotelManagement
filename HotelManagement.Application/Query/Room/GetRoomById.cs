using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Query.Room
{
    public class GetRoomByIdCommand : IRequest<Result<GetRoomByIdResponseDto>>
    {
        public int Id { get; }

        public GetRoomByIdCommand(int id)
        {
            Id = id;
        }
    }
    public class GetRoomByIdHandler : IRequestHandler<GetRoomByIdCommand, Result<GetRoomByIdResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetRoomByIdHandler> _logger;

        public GetRoomByIdHandler(IUnitOfWork unitOfWork, ILogger<GetRoomByIdHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<GetRoomByIdResponseDto>> Handle(GetRoomByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.Id <= 0)
                {
                    _logger.LogWarning("Invalid room ID: {Id}", request.Id);
                    return Result<GetRoomByIdResponseDto>.BadRequest();
                }

                var room = await _unitOfWork.RoomRepository.GetByColumnAsync(x => x.Id == request.Id);

                if (room == null)
                {
                    _logger.LogWarning("Room not found: {Id}", request.Id);
                    return Result<GetRoomByIdResponseDto>.NotFound("Room not found");
                }

                var roomDto = new GetRoomByIdResponseDto
                {
                    Id = room.Id,
                    RoomNumber = room.RoomNumber,
                    Price = room.Price,
                    Status = room.Status,
                    DateCreated = room.DateCreated,
                    RoomTypeId = room.RoomTypeId,
                    RoomAmenitiesId = room.RoomAmenityId,
                    //Urls = room.Url
                };

                _logger.LogInformation("Room retrieved successfully: {Id}", request.Id);

                return Result<GetRoomByIdResponseDto>.SuccessResult(roomDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving room: {Id}", request.Id);
                return Result<GetRoomByIdResponseDto>.InternalServerError();
            }
        }
    }
    public class GetRoomByIdResponseDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomAmenitiesId { get; set; }
        public List<string> Urls { get; set; }
    }
}