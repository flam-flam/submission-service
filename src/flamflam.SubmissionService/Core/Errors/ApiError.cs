namespace flamflam.SubmissionService.Core.Errors
{
    public class ApiErrorResponse
    {
        public ApiErrorResponse(int status, string message, string? errorCode = null)
        {
            Status = status;
            Message = message;
            ErrorCode = errorCode;
        }

        public int Status { get; }

        public string Message { get; }

        public string? ErrorCode { get; }
    }
}
