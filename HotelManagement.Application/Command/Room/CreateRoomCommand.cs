using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Command.Room
{
    public class CreateRoomCommand : IRequest<Result<CreateRoomResponseDto>>
    {
        public CreateRoomRequestDto RequestDto { get; }

        public CreateRoomCommand(CreateRoomRequestDto _requestDto)
        {
            RequestDto = _requestDto;
        }
    }
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, Result<CreateRoomResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateRoomHandler> _logger;

        public CreateRoomHandler(IUnitOfWork unitOfWork, ILogger<CreateRoomHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<CreateRoomResponseDto>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var roomEntity = new Domain.Entities.Room
                {
                    RoomNumber = request.RequestDto.RoomNumber,
                    Price = request.RequestDto.Price,
                    Status = request.RequestDto.Status,
                    DateCreated = DateTime.Now,
                    RoomTypeId = request.RequestDto.RoomTypeId,
                    RoomAmenityId = request.RequestDto.RoomAmenitiesId
                };

                await _unitOfWork.RoomRepository.AddAsync(roomEntity);
                await _unitOfWork.Save();

                _logger.LogInformation("Room created: {RoomId}", roomEntity.Id);

                var responseDto = new CreateRoomResponseDto
                {
                    RoomId = roomEntity.Id,
                    RoomNumber = roomEntity.RoomNumber,
                    Price = roomEntity.Price,
                    Status = roomEntity.Status,
                    DateCreated = roomEntity.DateCreated,
                    RoomTypeId = roomEntity.RoomTypeId,
                    RoomAmenitiesId = roomEntity.RoomAmenityId
                };

                return Result<CreateRoomResponseDto>.SuccessResult(responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating room");
                return Result<CreateRoomResponseDto>.InternalServerError();
            }
        }
    }
    public class CreateRoomRequestDto
    {
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomAmenitiesId { get; set; }
    }
    public class CreateRoomResponseDto
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime DateCreated { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomAmenitiesId { get; set; }
    }
}