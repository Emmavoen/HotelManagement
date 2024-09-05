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
    public class DeleteAmenity : IRequest<Result<Unit>>
    {
        public int Id { get; }

        public DeleteAmenity(int id)
        {
            Id = id;
        }
    }
    public class DeleteAmenityHandler : IRequestHandler<DeleteAmenity, Result<Unit>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAmenityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Unit>> Handle(DeleteAmenity request, CancellationToken cancellationToken)
        {
            var amenityEntity = await _unitOfWork.AmenityRepository.GetByColumnAsync(a => a.Id == request.Id);

            if (amenityEntity == null)
            {
                return Result<Unit>.NotFound("Amenity not found");
            }

           await  _unitOfWork.AmenityRepository.DeleteAsync(amenityEntity.Id);
            await _unitOfWork.Save();

            return Result<Unit>.SuccessResult(Unit.Value);
        }
    }
}