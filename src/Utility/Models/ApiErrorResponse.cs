namespace Utility.Models
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public bool IsSuccess { get; set; } = false;
        public int StatusCode { get; set; }
    }
}