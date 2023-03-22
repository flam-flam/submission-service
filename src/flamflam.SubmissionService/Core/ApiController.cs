using flamflam.SubmissionService.Core.Telemetry;
using Microsoft.AspNetCore.Mvc;

namespace flamflam.SubmissionService.Core
{
    public abstract class ApiController : ControllerBase
    {
        protected ApiController(ITelemetryReporter telemetry)
        {
            Telemetry = telemetry;
        }

        protected ITelemetryReporter Telemetry { get; }
    }
}
