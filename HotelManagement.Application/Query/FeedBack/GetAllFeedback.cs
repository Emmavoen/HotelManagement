using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Query.FeedBack
{
    public class GetFeedbackQuery : IRequest<Result<List<GetFeedbacksResponseDto>>>
    {
    }
    public class GetFeedbacksHandler : IRequestHandler<GetFeedbackQuery, Result<List<GetFeedbacksResponseDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetFeedbacksHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<List<GetFeedbacksResponseDto>>> Handle(GetFeedbackQuery request, CancellationToken cancellationToken)
        {
            var feedbacks = await _unitOfWork.FeedbackRepository.GetAll();

            var feedbackDtos = feedbacks.Select(feedback => new GetFeedbacksResponseDto
            {
                Id = feedback.Id,
                Comments = feedback.Comments,
                Rating = feedback.Rating,
                DateSubmitted = feedback.DateSubmitted,
                UserId = feedback.UserId,
                UserName = feedback.User?.UserName
            }).ToList();

            return Result<List<GetFeedbacksResponseDto>>.SuccessResult(feedbackDtos);
        }
    }

    public class GetFeedbacksResponseDto
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}