using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.RoomAmenity
{
    public class CreateRoomAmenityCommand : IRequest<Result<string>>
    {
        
    }

    public class CreateRoomAmenityHandler : IRequestHandler<CreateRoomAmenityCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomAmenityHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<string>> Handle(CreateRoomAmenityCommand request, CancellationToken cancellationToken)
        {
            var RoomAmenity = new Domain.Entities.RoomAmenity();
            await _unitOfWork.RoomAmenityRepository.AddAsync(RoomAmenity);
            await _unitOfWork.Save();

            return Result<string>.SuccessResult("Room amenity successfully created");
        }
    }
}