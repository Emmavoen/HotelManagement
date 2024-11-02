using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.Amenity
{
    public class UpdateAmenity : IRequest<Result<UpdateAmenityResponseDto>>
    {
        public int Id { get; }
        public UpdateAmenityRequestDto RequestDto { get; }

        public UpdateAmenity(int id, UpdateAmenityRequestDto requestDto)
        {
            Id = id;
            RequestDto = requestDto;
        }
    }


    public class UpdateAmenityHandler : IRequestHandler<UpdateAmenity, Result<UpdateAmenityResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAmenityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<UpdateAmenityResponseDto>> Handle(UpdateAmenity request, CancellationToken cancellationToken)
        {
            var amenityEntity = await _unitOfWork.AmenityRepository.GetByColumnAsync(a => a.Id == request.Id);

            if (amenityEntity == null)
            {
                return Result<UpdateAmenityResponseDto>.NotFound("Amenity not found");
            }

            // Update amenity entity with the new details from RequestDto
            amenityEntity.Name = request.RequestDto.Name;
            amenityEntity.Description = request.RequestDto.Description;
            amenityEntity.IsActive = request.RequestDto.IsActive;
            amenityEntity.RoomAmenityId = request.RequestDto.RoomAmenitiesId;

            _unitOfWork.AmenityRepository.UpdateASync(amenityEntity);
            await _unitOfWork.Save();

            var responseDto = new UpdateAmenityResponseDto
            {
                Id = amenityEntity.Id,
                Name = amenityEntity.Name,
                Description = amenityEntity.Description,
                IsActive = amenityEntity.IsActive,
                RoomAmenitiesId = amenityEntity.RoomAmenityId
            };

            return Result<UpdateAmenityResponseDto>.SuccessResult(responseDto);
        }

        
    }

    public class UpdateAmenityResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public int? RoomAmenitiesId { get; set; }
    }
    public class UpdateAmenityRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public int RoomAmenitiesId { get; set; }
    }

}