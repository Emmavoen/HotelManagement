using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.Amenity
{
    public class AssignAmenityToRoomCommand : IRequest<Result<Unit>>
    {
        internal readonly int RoomAmenitiesId;
        internal readonly int AmenityId;

        public AssignAmenityToRoomCommand(int roomAmenitiesId, int amenityId)
        {
            RoomAmenitiesId = roomAmenitiesId;
            AmenityId = amenityId;
        }
    }

    public class AssignAmenityToRoomHandler : IRequestHandler<AssignAmenityToRoomCommand, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignAmenityToRoomHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(AssignAmenityToRoomCommand request, CancellationToken cancellationToken)
        {
            var roomAmenities = await _unitOfWork.RoomAmenityRepository.GetByColumnAsync(x => x.Id == request.RoomAmenitiesId);

            if (roomAmenities == null)
            {
                return Result<Unit>.NotFound("Room Amenities not found");
            }

            var amenity = await _unitOfWork.AmenityRepository.GetByColumnAsync(x => x.Id == request.AmenityId);

            if (amenity == null)
            {
                return Result<Unit>.NotFound("Amenity not found");
            }

            // if (roomAmenities.Amenities == null)
            // {
            //     roomAmenities.Amenities = new List<Domain.Entities.Amenity>();
            // }
            // roomAmenities.Amenities.Add(amenity);
            amenity.RoomAmenityId = request.RoomAmenitiesId;
            _unitOfWork.AmenityRepository.UpdateASync(amenity);
            await _unitOfWork.Save();

            return Result<Unit>.SuccessResult("Amenity assigned to Room Amenities successfully");
        }

    }
}