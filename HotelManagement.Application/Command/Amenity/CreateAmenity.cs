using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;
using static HotelManagement.Application.Command.Amenity.CreateAmenityHandler;

namespace HotelManagement.Application.Command.Amenity
{

    public class CreateAmenity : IRequest<Result<CreateAmenityResponseDto>>
    {
        public CreateAmenityRequestDto RequestDto { get; }

        public CreateAmenity(CreateAmenityRequestDto requestDto)
        {
            RequestDto = requestDto;
        }

    }
    public class CreateAmenityHandler : IRequestHandler<CreateAmenity, Result<CreateAmenityResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAmenityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateAmenityResponseDto>> Handle(CreateAmenity request, CancellationToken cancellationToken)
        {
            var amenityEntity = new Domain.Entities.Amenity
            {
                Name = request.RequestDto.Name,
                Description = request.RequestDto.Description,
                IsActive = request.RequestDto.IsActive,
                RoomAmenityId = request.RequestDto.RoomAmenitiesId
            };

            await _unitOfWork.AmenityRepository.AddAsync(amenityEntity);
            await _unitOfWork.Save();

            var responseDto = new CreateAmenityResponseDto
            {
                AmenityId = amenityEntity.Id,
                Name = amenityEntity.Name,
                Description = amenityEntity.Description,
                IsActive = amenityEntity.IsActive,
                RoomAmenitiesId = amenityEntity.RoomAmenityId
            };

            return Result<CreateAmenityResponseDto>.SuccessResult(responseDto);

    }
    public class CreateAmenityResponseDto
    {
        public int AmenityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int RoomAmenitiesId { get; set; }
    }
    public class CreateAmenityRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int RoomAmenitiesId { get; set; }
    }
    }
}