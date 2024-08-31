using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Dtos.Response;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.RoomType
{
    public class DeleteRoomTypeCommand : IRequest<Result<RoomTypeResponseDto>>
    {
        internal readonly string typeName;

        public DeleteRoomTypeCommand(string typeName)
        {
            this.typeName = typeName;
        }
    }

    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, Result<RoomTypeResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async  Task<Result<RoomTypeResponseDto>> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
           var exist = await _unitOfWork.RoomTypeRepository.GetByColumnAsync(x => x.TypeName == request.typeName);
            if(exist == null)
            {
                return Result<RoomTypeResponseDto>.NotFound("RoomType not found");
            }

             await _unitOfWork.RoomTypeRepository.DeleteAsync(exist.Id);
             var save  = await _unitOfWork.Save();

             if(save < 1)
            {
                return Result<RoomTypeResponseDto>.InternalServerError();
            }

            return Result<RoomTypeResponseDto>.SuccessResult("Success");
        }
        
    }
}