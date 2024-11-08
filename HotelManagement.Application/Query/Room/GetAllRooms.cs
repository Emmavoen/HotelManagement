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
    public class GetAllRoomsCommand : IRequest<Result<List<GetRoomsResponseDto>>>
    {
    }
    public class GetAllRoomsHandler : IRequestHandler<GetAllRoomsCommand, Result<List<GetRoomsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAllRoomsHandler> _logger;

        public GetAllRoomsHandler(IUnitOfWork unitOfWork, ILogger<GetAllRoomsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<List<GetRoomsResponseDto>>> Handle(GetAllRoomsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rooms = await _unitOfWork.RoomRepository.GetAll();

                if (rooms == null || !rooms.Any())
                {
                    _logger.LogWarning("No rooms found");
                    return Result<List<GetRoomsResponseDto>>.NotFound("No rooms found");
                }

                var roomDtos = rooms.Select(room => new GetRoomsResponseDto
                {
                    Id = room.Id,
                    RoomNumber = room.RoomNumber,
                    Price = room.Price,
                    Status = room.Status,
                    DateCreated = room.DateCreated,
                    RoomTypeId = room.RoomTypeId,
                    RoomAmenitiesId = room.RoomAmenityId,
                    //Urls = room.Url
                }).ToList();

                _logger.LogInformation("Rooms retrieved successfully");

                return Result<List<GetRoomsResponseDto>>.SuccessResult(roomDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving rooms");
                return Result<List<GetRoomsResponseDto>>.InternalServerError();
            }
        }
    }
    public class GetRoomsResponseDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomAmenitiesId { get; set; }
        public string RoomAmenitiesName { get; set; }
        public List<string> Urls { get; set; }
    }

}