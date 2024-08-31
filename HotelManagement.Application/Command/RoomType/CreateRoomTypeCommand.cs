using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Dtos.Request;
using HotelManagement.Application.Dtos.Response;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.RoomType
{
    public class CreateRoomTypeCommand : IRequest<Result<RoomTypeResponseDto>>
    {
        internal readonly RoomTypeRequestDto requestDto;

        public CreateRoomTypeCommand(RoomTypeRequestDto requestDto)
        {
            this.requestDto = requestDto;
        }
    }


    public class CreateRoomTypeHandler : IRequestHandler<CreateRoomTypeCommand, Result<RoomTypeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomTypeHandler (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<RoomTypeResponseDto>> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var exist = await _unitOfWork.RoomTypeRepository.GetByColumnAsync(x => x.TypeName == request.requestDto.TypeName);
            if(exist != null)
            {
                return Result<RoomTypeResponseDto>.Conflict("Amenity Already Exist");
            }

            var newAmenity =  new Domain.Entities.RoomType()
            {
                TypeName = request.requestDto.TypeName,
                Description = request.requestDto.Description,
                AccessibilityFeatures =  request.requestDto.AccessibilityFeatures,
            };


            await _unitOfWork.RoomTypeRepository.AddAsync(newAmenity);
            var save = await _unitOfWork.Save();

            if(save < 1)
            {
                return Result<RoomTypeResponseDto>.InternalServerError();
            }

            var response = new RoomTypeResponseDto()
            {
                Id = newAmenity.Id,
                AccessibilityFeatures = newAmenity.AccessibilityFeatures,
                TypeName = newAmenity.TypeName,
                Description = newAmenity.Description
            };

            return Result<RoomTypeResponseDto>.SuccessResult(response);
        }
    }
}