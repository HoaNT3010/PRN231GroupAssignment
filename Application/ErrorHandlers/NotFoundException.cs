using System.Net;

namespace Application.ErrorHandlers
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string? message) : base(message)
        {
            StatusCode = (int)HttpStatusCode.NotFound;
            Title = "Not found";
        }

        public NotFoundException()
        {
            StatusCode = (int)HttpStatusCode.NotFound;
            Title = "Not found";
        }
    }
}
