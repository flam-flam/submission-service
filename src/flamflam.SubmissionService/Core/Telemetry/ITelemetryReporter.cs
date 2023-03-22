namespace flamflam.SubmissionService.Core.Telemetry
{
    public interface ITelemetryReporter
    {
        void TraceError<TPayload>(string type, TPayload data);
        void TraceInfo<TPayload>(string type, TPayload data);
        void TraceEvent<TPayload>(LogLevel level, string type, TPayload data);
    }

    public interface ITelemetryReporter<T> : ITelemetryReporter
    {
    }
}