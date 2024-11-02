using System;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.Refund
{
    public class CreateRefundCommand : IRequest<Result<CreateRefundResponseDto>>
    {
        public CreateRefundRequestDto RequestDto { get; }

        public CreateRefundCommand(CreateRefundRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
    public class CreateRefundHandler : IRequestHandler<CreateRefundCommand, Result<CreateRefundResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateRefundHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateRefundResponseDto>> Handle(CreateRefundCommand request, CancellationToken cancellationToken)
        {
            var refundEntity = new Domain.Entities.Refund
            {
                PaymentReference = request.RequestDto.ReferenceDetails,
                Amount = request.RequestDto.Amount,
                DateIssued = DateTime.UtcNow // Set the current date and time
            };

            await _unitOfWork.RefundRepository.AddAsync(refundEntity);
            await _unitOfWork.Save();

            var responseDto = new CreateRefundResponseDto
            {
                Id = refundEntity.Id,
                ReferenceDetails = refundEntity.PaymentReference,
                Amount = refundEntity.Amount,
                DateIssued = refundEntity.DateIssued
            };

            return Result<CreateRefundResponseDto>.SuccessResult(responseDto);
        }
    }

    public class CreateRefundResponseDto
    {
        public int Id { get; set; }
        public string ReferenceDetails { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }
    }

    public class CreateRefundRequestDto
    {
        public string ReferenceDetails { get; set; }
        public decimal Amount { get; set; }
    }
}