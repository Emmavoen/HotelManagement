using System;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Command.Room
{
    public class ChangeRoomStatusCommand : IRequest<Result<ChangeRoomStatusResponseDto>>
    {
        public ChangeRoomStatusRequestDto RequestDto { get; }

        public ChangeRoomStatusCommand(ChangeRoomStatusRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
    public class ChangeRoomStatusHandler : IRequestHandler<ChangeRoomStatusCommand, Result<ChangeRoomStatusResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ChangeRoomStatusHandler> _logger;

        public ChangeRoomStatusHandler(IUnitOfWork unitOfWork, ILogger<ChangeRoomStatusHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<ChangeRoomStatusResponseDto>> Handle(ChangeRoomStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.RequestDto.RoomId <= 0)
                {
                    _logger.LogWarning("Invalid room ID: {RoomId}", request.RequestDto.RoomId);
                    return Result<ChangeRoomStatusResponseDto>.BadRequest();
                }

                var roomEntity = await _unitOfWork.RoomRepository.GetByColumnAsync(x => x.Id == request.RequestDto.RoomId);

                if (roomEntity == null)
                {
                    _logger.LogWarning("Room not found: {RoomId}", request.RequestDto.RoomId);
                    return Result<ChangeRoomStatusResponseDto>.NotFound("Room not found");
                }
                
                var oldStatus = roomEntity.Status;

                roomEntity.Status = request.RequestDto.NewStatus;

                _unitOfWork.RoomRepository.UpdateASync(roomEntity);
                await _unitOfWork.Save();

                var responseDto = new ChangeRoomStatusResponseDto
                {
                    RoomId = roomEntity.Id,
                    RoomNumber = roomEntity.RoomNumber,
                    OldStatus = oldStatus,
                    NewStatus = roomEntity.Status
                };

                _logger.LogInformation("Room status changed: {RoomId} - {OldStatus} -> {NewStatus}", roomEntity.Id, oldStatus, roomEntity.Status);

                return Result<ChangeRoomStatusResponseDto>.SuccessResult(responseDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing room status: {RoomId}", request.RequestDto.RoomId);
                return Result<ChangeRoomStatusResponseDto>.InternalServerError();
            }
        }

       
    }

    public class ChangeRoomStatusResponseDto
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
    }
    public class ChangeRoomStatusRequestDto
    {
        public int RoomId { get; set; }
        public string NewStatus { get; set; }
    }
}