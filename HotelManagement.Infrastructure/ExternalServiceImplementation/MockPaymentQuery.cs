using System;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.Enum;
using MediatR;

namespace HotelManagement.Infrastructure.ExternalServiceImplementation
{

    public class MockPaymentQuery : IRequest<Result<MockPaymentResponse>>
    {
        internal readonly MockPaymentRequest _request;
        

        public MockPaymentQuery(MockPaymentRequest request)
        {
            _request = request;
           
        }
    }
    public class MockPaymentResponse
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string PayerId { get; set; }
        public string PayerEmail { get; set; }
        public string PayerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ApprovalUrl { get; set; } 
    }

    public class MockPaymentRequest
    {
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
        public DateTime PaymentDate { get; set; }
    }
    public class MockPaymentHandler : IRequestHandler<MockPaymentQuery, Result<MockPaymentResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MockPaymentHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<MockPaymentResponse>> Handle(MockPaymentQuery request, CancellationToken cancellationToken)
        {
            // if (string.IsNullOrEmpty(request._request.PayerId))
            // {
            //     request._request.PayerId = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10);
            // }

            // if (string.IsNullOrEmpty(request._request.PayerEmail))
            // {
            //     request._request.PayerEmail = $"{request._request.PayerId}@example.com";
            // }

            // if (string.IsNullOrEmpty(request._request.PayerName))
            // {
            //     request._request.PayerName = "John Doe";
            // }

            if(request.)

            var payment = new Payment()
            {
                PaymentDate = DateTime.Now,
                Amount = request._request.Amount,

            };
            await _unitOfWork.PaymentRepository.AddAsync(payment);
            var save = await _unitOfWork.Save();
            var mockResponse = new MockPaymentResponse
            {
                Id = payment.Id,
                Status = "COMPLETED",
                Amount = request._request.Amount,
                Currency = "NGN",
                PayerId = request._request.PayerId,
                PayerEmail = request._request.PayerEmail,
                PayerName = request._request.PayerName,
                CreatedAt = DateTime.UtcNow,
                ApprovalUrl = null // Not needed in immediate payment scenarios
            };


            

            
            if(save <1)
            {
                return  Result<MockPaymentResponse>.InternalServerError();
            }

            return  Result<MockPaymentResponse>.SuccessResult(mockResponse);
        }

        
    }

}