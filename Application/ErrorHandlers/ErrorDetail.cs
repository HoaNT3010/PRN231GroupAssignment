namespace Application.ErrorHandlers
{
    public class ErrorDetail
    {
        public int StatusCode { get; set; }
        public string? Title { get; set; }
        public string? Message { get; set; }
        public DateTime Time { get; set; } = DateTime.UtcNow;
    }
}
