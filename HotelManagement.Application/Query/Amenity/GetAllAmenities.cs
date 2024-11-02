using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Query.Amenity
{
    public class GetAmenities : IRequest<Result<List<GetAmenitiesResponseDto>>>
    {
    }
    public class GetAmenitiesHandler : IRequestHandler<GetAmenities, Result<List<GetAmenitiesResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAmenitiesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAmenitiesResponseDto>>> Handle(GetAmenities request, CancellationToken cancellationToken)
        {
            var amenities = await _unitOfWork.AmenityRepository.GetAll();

            var amenityDtos = amenities.Select(amenity => new GetAmenitiesResponseDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Description = amenity.Description,
                IsActive = amenity.IsActive,
                 RoomAmenitiesId = amenity.RoomAmenityId
            }).ToList();

            return Result<List<GetAmenitiesResponseDto>>.SuccessResult(amenityDtos);
        }

        
    }

    public class GetAmenitiesResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
        public int? RoomAmenitiesId { get; set; }
    }
}