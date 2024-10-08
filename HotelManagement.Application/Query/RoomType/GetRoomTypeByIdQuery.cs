using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Dtos.Response;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Query.RoomType
{
    public class GetRoomTypeByIdQuery : IRequest<Result<RoomTypeResponseDto>>
    {
        internal readonly int id;

        public GetRoomTypeByIdQuery(int id)
        {
            this.id = id;
        }
    }

    public class GetRoomTypeByIdQueryHandler : IRequestHandler<GetRoomTypeByIdQuery, Result<RoomTypeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoomTypeByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<RoomTypeResponseDto>> Handle(GetRoomTypeByIdQuery request, CancellationToken cancellationToken)
        {
            var exist =  await _unitOfWork.RoomTypeRepository.GetByColumnAsync(x => x.Id == request.id);
            if (exist == null)  
            {
                return Result<RoomTypeResponseDto>.NotFound("RoomType Not Found");
            }

            var response  = new RoomTypeResponseDto()
            {
                Id = exist.Id,
                TypeName = exist.TypeName,
                Description = exist.Description,
                AccessibilityFeatures = exist.AccessibilityFeatures,

            };
            return Result<RoomTypeResponseDto>.SuccessResult(response);
        }
    }
}