using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Application.Command.FeedBack
{
   public class CreateFeedbackCommand : IRequest<Result<CreateFeedbackResponseDto>>
    {
        public CreateFeedbackRequestDto RequestDto { get; }

        public CreateFeedbackCommand(CreateFeedbackRequestDto requestDto)
        {
            RequestDto = requestDto;
        }
    }
    public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand, Result<CreateFeedbackResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateFeedbackHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CreateFeedbackResponseDto>> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
        {
            var feedbackEntity = new Domain.Entities.Feedback
            {
                Comments = request.RequestDto.Comments,
                Rating = request.RequestDto.Rating,
                DateSubmitted = DateTime.UtcNow, // Set the current date and time
                UserId = request.RequestDto.UserId
            };

            await _unitOfWork.FeedbackRepository.AddAsync(feedbackEntity);
            await _unitOfWork.Save();

            var responseDto = new CreateFeedbackResponseDto
            {
                Id = feedbackEntity.Id,
                Comments = feedbackEntity.Comments,
                Rating = feedbackEntity.Rating,
                DateSubmitted = feedbackEntity.DateSubmitted,
                UserId = feedbackEntity.UserId
            };

            return Result<CreateFeedbackResponseDto>.SuccessResult(responseDto);
        }
    }

    public class CreateFeedbackResponseDto
    {
        public int Id { get; set; }
        public string Comments { get; set; }
        public int Rating { get; set; }
        public DateTime DateSubmitted { get; set; }
        public string UserId { get; set; }
    }

    public class CreateFeedbackRequestDto
    {
        public string Comments { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
    }

}