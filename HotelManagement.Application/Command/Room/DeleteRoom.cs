using System;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HotelManagement.Application.Command.Room
{
    public class DeleteRoomCommand : IRequest<Result<Unit>>
    {
        public int Id { get; }

        public DeleteRoomCommand(int id)
        {
            Id = id;
        }
    }
    public class DeleteRoomHandler : IRequestHandler<DeleteRoomCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteRoomHandler> _logger;

        public DeleteRoomHandler(IUnitOfWork unitOfWork, ILogger<DeleteRoomHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<Unit>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (request.Id <= 0)
                {
                    _logger.LogWarning("Invalid room ID: {Id}", request.Id);
                    return Result<Unit>.NotFound("Invalid Id");
                }

                var roomEntity = await _unitOfWork.RoomRepository.GetByColumnAsync(r => r.Id == request.Id);

                if (roomEntity == null)
                {
                    _logger.LogWarning("Room not found: {Id}", request.Id);
                    return Result<Unit>.NotFound("Room not found");
                }

                await _unitOfWork.RoomRepository.DeleteAsync(roomEntity.Id);
                await _unitOfWork.Save();

                _logger.LogInformation("Room deleted: {Id}", request.Id);

                return Result<Unit>.SuccessResult(Unit.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting room: {Id}", request.Id);
                return Result<Unit>.InternalServerError();
            }
        }
    }
}