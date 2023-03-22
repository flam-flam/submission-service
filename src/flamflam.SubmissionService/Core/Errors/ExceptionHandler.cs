using flamflam.SubmissionService.Core.Telemetry;

namespace flamflam.SubmissionService.Core.Errors
{
    public class ExceptionHandlerFacade
    {
        private readonly ITelemetryReporter _telemetry;
        private readonly DefaultExceptionHandler _default;
        private readonly IEnumerable<IAppExceptionHandler> _handlers;

        public ExceptionHandlerFacade(
            ITelemetryReporter<ExceptionHandlerFacade> telemetry,
            DefaultExceptionHandler @default,
            IEnumerable<IAppExceptionHandler> handlers)
        {
            _telemetry = telemetry;
            _default = @default;
            _handlers = handlers;
        }

        public ApiErrorResponse HandleException(Exception exception)
        {
            _telemetry.TraceError("error", exception);

            var handler = GetHandler(exception);
            return handler.ToApiErrorResponse(exception);
        }

        private IAppExceptionHandler GetHandler(Exception exception)
        {
            return _handlers.FirstOrDefault(h => h.IsExceptionSupported(exception)) ?? _default;
        }
    }
}
