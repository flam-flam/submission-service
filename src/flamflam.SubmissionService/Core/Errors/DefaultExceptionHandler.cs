namespace flamflam.SubmissionService.Core.Errors
{
    public class DefaultExceptionHandler : IAppExceptionHandler
    {
        public bool IsExceptionSupported(Exception ex)
        {
            return true;
        }

        public ApiErrorResponse ToApiErrorResponse(Exception ex)
        {
            return new ApiErrorResponse(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
