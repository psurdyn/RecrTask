namespace RecruitmentTask.Models.Responses
{
    public class ErrorResponse<T> : ResponseBaseClass<T> where T : class
    {
        public Exception? Exception { get; set; }
        public string? Message { get; set; }

        public ErrorResponse(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }

        public ErrorResponse(int statusCode, Exception e)
        {
            StatusCode = statusCode;
            Exception = e;
        }

        public string? GetMessage()
        {
            return Message ?? Exception?.ToString();
        }
    }
}
