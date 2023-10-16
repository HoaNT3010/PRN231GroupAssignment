namespace Infrastructure.DTOs.Response
{
    public class ResponseObject<T> where T : class
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
