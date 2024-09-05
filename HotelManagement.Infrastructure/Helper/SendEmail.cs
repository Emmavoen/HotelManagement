using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using HotelManagement.Application.Contracts.UnitOfWork;
using HotelManagement.Application.Dtos.Request;
using HotelManagement.Application.Helpers;
using MediatR;

namespace HotelManagement.Infrastructure.Helper
{

    public class SendEmailCommand : IRequest<Result<string>>
    {
        internal readonly EmailRequest request;

        public SendEmailCommand(EmailRequest request)
        {
            this.request = request;
        }
    }
    public class EmailRequest
    {
         public string UserEmail { get; set; }
        public string Subject { get; set; }
        public int BookingId { get; set; }

    }
    public class EmailMessage
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
    public class SendEmailHandler : IRequestHandler<SendEmailCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpClientFactory _httpClientFactory;

        public SendEmailHandler(IUnitOfWork unitOfWork, IHttpClientFactory httpClientFactory)
        {
            _unitOfWork = unitOfWork;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Result<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            var booking = await _unitOfWork.BookingRepository.GetByColumnAsync(x => x.Id == request.request.BookingId);

            var httpclient = _httpClientFactory.CreateClient();
            var emailMessage = new EmailMessage
            {
                To = request.request.UserEmail,
                Subject = request.request.Subject,
                Body = @"
                    <html>
                    <head>
                        <meta charset='utf-8'/>
                        <title></title>
                    </head>
                    <body>
                        <img style='background:black;' src='https://www.aspsnippets.com/assets/img/logo_ns.png'/><br /><br />
                        <div style='border-top: 3px solid #61028D'>&nbsp;</div>
                        <span style='font-family: Arial; font-size: 10pt'>
                            Hello <b>{UserId}</b>,<br /><br />
                            Your booking (ID: {BookingId}) has been confirmed.<br /><br />
                            Booking Details:<br />
                            <b>Booking Date:</b> {BookingDate}<br />
                            <b>Check-In Date:</b> {CheckInDate}<br />
                            <b>Check-Out Date:</b> {CheckOutDate}<br />
                            <b>Number of Occupants:</b> {NumberOfOcupant}<br />
                            <b>Room ID:</b> {RoomId}<br />
                            <br /><br />
                            Thank you for booking with us!<br />
                            Regards,<br />
                            Hotel Management Team
                        </span>
                    </body>
                    </html>"

            };

            emailMessage.Body = emailMessage.Body
            .Replace("{UserId}", booking.UserId)
            .Replace("{BookingId}", booking.Id.ToString())
            .Replace("{BookingDate}", booking.BookingDate.ToString("MMMM dd, yyyy"))
            .Replace("{CheckInDate}", booking.CheckInDate.ToString("MMMM dd, yyyy"))
            .Replace("{CheckOutDate}", booking.CheckOutDate.ToString("MMMM dd, yyyy"))
            .Replace("{NumberOfOcupant}", booking.NumberOfOcupant.ToString())
            .Replace("{RoomId}", booking.RoomId.ToString());
             

            var sendEmail = await httpclient.PostAsJsonAsync("https://localhost:7168/api/Notification", emailMessage);


            if (sendEmail.IsSuccessStatusCode)
            {


                return Result<string>.SuccessResult("Email successfully sent");
            }


            return Result<string>.InternalServerError();
        }

    }


}