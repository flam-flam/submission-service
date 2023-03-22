namespace flamflam.SubmissionService.Core.Errors
{
    public interface IAppExceptionHandler
    {
        bool IsExceptionSupported(Exception ex);

        ApiErrorResponse ToApiErrorResponse(Exception ex);
    }
}
