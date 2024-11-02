using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Query.Refund
{
    public class GetRefundQuery : IRequest<Result<List<GetRefundsResponseDto>>>
    {
    }
    public class GetRefundsHandler : IRequestHandler<GetRefundQuery, Result<List<GetRefundsResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRefundsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetRefundsResponseDto>>> Handle(GetRefundQuery request, CancellationToken cancellationToken)
        {
            var refunds = await _unitOfWork.RefundRepository.GetAll();

            var refundDtos = refunds.Select(refund => new GetRefundsResponseDto
            {
                Id = refund.Id,
                ReferenceDetails = refund.PaymentReference,
                Amount = refund.Amount,
                DateIssued = refund.DateIssued
            }).ToList();

            return Result<List<GetRefundsResponseDto>>.SuccessResult(refundDtos);
        }
    }

    public class GetRefundsResponseDto
    {
        public int Id { get; set; }
        public string ReferenceDetails { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateIssued { get; set; }
    }
}