using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotelManagement.Application.Query.RoomAmenity
{
    public class GetAmenitiesByRoomAmenitiesId : IRequest<Result<List<GetAmenitiesByRoomAmenitiesIdResponseDto>>>
    {
        public int RoomAmenitiesId { get; }

        public GetAmenitiesByRoomAmenitiesId(int roomAmenitiesId)
        {
            RoomAmenitiesId = roomAmenitiesId;
        }
    }


    public class GetAmenitiesByRoomAmenitiesIdHandler : IRequestHandler<GetAmenitiesByRoomAmenitiesId, Result<List<GetAmenitiesByRoomAmenitiesIdResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAmenitiesByRoomAmenitiesIdHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetAmenitiesByRoomAmenitiesIdResponseDto>>> Handle(GetAmenitiesByRoomAmenitiesId request, CancellationToken cancellationToken)
        {
            // Fetch RoomAmenities including the related Amenities by RoomAmenitiesId
            var roomAmenities = await _unitOfWork.RoomAmenityRepository.GetWhereAndIncludeAsync(
                ra => ra.Id == request.RoomAmenitiesId,
                include: ra => ra.Include(ra => ra.Amenities)
            );

            // Check if RoomAmenities were found
            var roomAmenitiesEntity = roomAmenities.FirstOrDefault();
            if (roomAmenitiesEntity == null || !roomAmenitiesEntity.Amenities.Any())
            {
                return Result<List<GetAmenitiesByRoomAmenitiesIdResponseDto>>.NotFound("No amenities found for the specified RoomAmenitiesId.");
            }

            // Map amenities to DTOs
            var amenityDtos = roomAmenitiesEntity.Amenities.Select(amenity => new GetAmenitiesByRoomAmenitiesIdResponseDto
            {
                Id = amenity.Id,
                Name = amenity.Name,
                Description = amenity.Description,
                IsActive = amenity.IsActive
            }).ToList();

            return Result<List<GetAmenitiesByRoomAmenitiesIdResponseDto>>.SuccessResult(amenityDtos);
        }

        
    }

    public class GetAmenitiesByRoomAmenitiesIdResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IsActive { get; set; }
    }
}