using System;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Dtos.Request;
using HotelManagement.Application.Dtos.Response;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.RoomType
{
    public class UpdateRoomTypeCommand : IRequest<Result<RoomTypeResponseDto>>
    {
        internal readonly RoomTypeRequestDto requestDto;

        public UpdateRoomTypeCommand(RoomTypeRequestDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }

    public class UpdateRoomTypeHandler : IRequestHandler<UpdateRoomTypeCommand, Result<RoomTypeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomTypeHandler (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<RoomTypeResponseDto>> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.RoomTypeRepository.GetByColumnAsync(x => x.TypeName == request.requestDto.TypeName);
            if(exist == null)
            {
                return Result<RoomTypeResponseDto>.NotFound("RoomType not found");
            }

            exist.TypeName = request.requestDto.TypeName;
            exist.Description = request.requestDto.Description;
            exist.AccessibilityFeatures = request.requestDto.AccessibilityFeatures;
            _unitOfWork.RoomTypeRepository.UpdateASync(exist);
            var save = await _unitOfWork.Save();
            if(save < 1)
            {
                return Result<RoomTypeResponseDto>.InternalServerError();
            }

            var response = new RoomTypeResponseDto()
            {
                TypeName = request.requestDto.TypeName,
                Description = request.requestDto.Description,
                AccessibilityFeatures = request.requestDto.AccessibilityFeatures

            };

            return Result<RoomTypeResponseDto>.SuccessResult(response);
        }
    }
}