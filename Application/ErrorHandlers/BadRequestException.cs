using System.Net;

namespace Application.ErrorHandlers
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string? message) : base(message)
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Title = "Bad request";
        }

        public BadRequestException()
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Title = "Bad request";
        }
    }
}
