using System;
using System.Threading;
using HotelManagement.Application.Contracts.UnitOfWork;
using System.Threading.Tasks;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Command.Room
{
    public class UpdateRoomCommand : IRequest<Result<UpdateRoomResponseDto>>
    {
    
        public UpdateRoomRequestDto RequestDto { get; }

        public UpdateRoomCommand( UpdateRoomRequestDto requestDto)
        {
            
            RequestDto = requestDto;
        }
    }
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, Result<UpdateRoomResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateRoomHandler> _logger;

        public UpdateRoomHandler(IUnitOfWork unitOfWork, ILogger<UpdateRoomHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<UpdateRoomResponseDto>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var roomEntity = await _unitOfWork.RoomRepository.GetByColumnAsync(a => a.RoomNumber == request.RequestDto.RoomNumber);

                if (roomEntity == null)
                {
                    _logger.LogWarning("Room not found: {Id}", request.RequestDto.RoomNumber);
                    return Result<UpdateRoomResponseDto>.NotFound("Room not found");
                }

                roomEntity.RoomNumber = request.RequestDto.RoomNumber;
                roomEntity.Price = request.RequestDto.Price;
                roomEntity.Status = request.RequestDto.Status;
                roomEntity.RoomTypeId = request.RequestDto.RoomTypeId;
                roomEntity.RoomAmenityId = request.RequestDto.RoomAmenitiesId;

                _unitOfWork.RoomRepository.UpdateASync(roomEntity);
                await _unitOfWork.Save();

                _logger.LogInformation("Room updated: {Id}", request.RequestDto.RoomNumber);

                var responseDto = new UpdateRoomResponseDto
                {
                    Id = roomEntity.Id,
                    RoomNumber = roomEntity.RoomNumber,
                    Price = roomEntity.Price,
                    Status = roomEntity.Status,
                    DateUpdated = DateTime.UtcNow,
                    RoomTypeId = roomEntity.RoomTypeId,
                    RoomAmenitiesId = roomEntity.RoomAmenityId
                };

                return Result<UpdateRoomResponseDto>.SuccessResult(responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating room: {Id}", request.RequestDto.RoomNumber);
                return Result<UpdateRoomResponseDto>.InternalServerError();
            }
        }


    }
    public class UpdateRoomResponseDto
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public DateTime DateUpdated { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomAmenitiesId { get; set; }
    }
    public class UpdateRoomRequestDto
    {
        public string RoomNumber { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public int RoomTypeId { get; set; }
        public int RoomAmenitiesId { get; set; }
    }

}