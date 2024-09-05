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
    public class ActivateDeactivateAmenity : IRequest<Result<ActivateDeactivateAmenityRequestDto>>
    {
        public int AmenityId { get; }
        public bool IsActive { get; }

        public ActivateDeactivateAmenity(int amenityId, bool isActive)
        {
            AmenityId = amenityId;
            IsActive = isActive;
        }
    }

    public class ActivateDeactivateAmenityHandler : IRequestHandler<ActivateDeactivateAmenity, Result<ActivateDeactivateAmenityRequestDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivateDeactivateAmenityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ActivateDeactivateAmenityRequestDto>> Handle(ActivateDeactivateAmenity request, CancellationToken cancellationToken)
        {

            var amenity = await _unitOfWork.AmenityRepository.GetByColumnAsync(x => x.Id == request.AmenityId);

            if (amenity == null)
            {

                return Result<ActivateDeactivateAmenityRequestDto>.NotFound("Amenity not found.");
            }


            amenity.IsActive = request.IsActive;


            _unitOfWork.AmenityRepository.UpdateASync(amenity);
            await _unitOfWork.Save();


            var responseDto = new ActivateDeactivateAmenityRequestDto
            {
                AmenityId = amenity.Id,
                IsActive = amenity.IsActive
            };


            return Result<ActivateDeactivateAmenityRequestDto>.SuccessResult(responseDto);
        }
    }
    public class ActivateDeactivateAmenityRequestDto
    {
        public int AmenityId { get; set; }
        public bool IsActive { get; set; }
    }
}