using flamflam.SubmissionService.Core.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace flamflam.SubmissionService.Controllers
{
    [ApiController]
    [Route("api/health")]
    [Produces("application/json")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;

        public HealthController(HealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Gets service health status",
            OperationId = "health/get"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HealthReport))]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            return report.Status == HealthStatus.Healthy 
                ? Ok(report)
                : StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }
    }
}
