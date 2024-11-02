using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Query.Room
{
    public class GetAvailableRoomsCommand : IRequest<Result<List<GetAvailableRoomsResponseDto>>>
    {
    }

    public class GetAvailableRoomsHandler : IRequestHandler<GetAvailableRoomsCommand, Result<List<GetAvailableRoomsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetAvailableRoomsHandler> _logger;

        public GetAvailableRoomsHandler(IUnitOfWork unitOfWork, ILogger<GetAvailableRoomsHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<List<GetAvailableRoomsResponseDto>>> Handle(GetAvailableRoomsCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var rooms = await _unitOfWork.RoomRepository.GetWhereAndIncludeAsync(
                    r => r.Status == "Available",
                    include: r => r.Include(rt => rt.RoomType)
                );

                if (rooms == null || !rooms.Any())
                {
                    _logger.LogWarning("No available rooms found");
                    return Result<List<GetAvailableRoomsResponseDto>>.NotFound("No available rooms found");
                }

                var roomDtos = rooms.Select(r => new GetAvailableRoomsResponseDto
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber,
                    Price = r.Price,
                    RoomTypeName = r.RoomType?.TypeName // Null-conditional operator to avoid null reference exception
                }).ToList();

                _logger.LogInformation("Available rooms retrieved successfully");

                return Result<List<GetAvailableRoomsResponseDto>>.SuccessResult(roomDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving available rooms");
                return Result<List<GetAvailableRoomsResponseDto>>.InternalServerError();
            }
        }
    }

    public class GetAvailableRoomsResponseDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string RoomTypeName { get; set; }
    }

    public class RoomDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string RoomTypeName { get; set; }
    }
}