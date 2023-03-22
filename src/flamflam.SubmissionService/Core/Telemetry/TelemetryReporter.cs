using flamflam.SubmissionService.Setup;
using Newtonsoft.Json;

namespace flamflam.SubmissionService.Core.Telemetry
{
    public class TelemetryReporter : ITelemetryReporter
    {
        private readonly ILogger _logger;

        public TelemetryReporter(ILogger logger)
        {
            _logger = logger;
        }

        public void TraceInfo<TPayload>(string type, TPayload data) =>
            TraceEvent(LogLevel.Information, type, data);

        public void TraceError<TPayload>(string type, TPayload data) =>
            TraceEvent(LogLevel.Error, type, data);

        public void TraceEvent<TPayload>(LogLevel level, string type, TPayload data)
        {
            var payload = new { type, data };
            var payloadAsJson = JsonConvert.SerializeObject(payload, Json.DefaultSettings);

            _logger.Log(level, payloadAsJson);
        }
    }

    public class TelemetryReporter<T> : TelemetryReporter, ITelemetryReporter<T>
    {
        public TelemetryReporter(ILogger<T> logger) : base(logger)
        {
        }
    }
}
