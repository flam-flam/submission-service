using flamflam.SubmissionService.Core;
using flamflam.SubmissionService.Core.Errors;
using flamflam.SubmissionService.Core.Telemetry;
using flamflam.SubmissionService.Models;
using flamflam.SubmissionService.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Annotations;

namespace flamflam.SubmissionService.Controllers
{
    [ApiController]
    [Route("api/submissions")]
    [Produces("application/json")]
    public class SubmissionsController : ApiController
    {
        private readonly ISubmissionRepository _repository;

        public SubmissionsController(ITelemetryReporter<SubmissionsController> telemetry, ISubmissionRepository repository) : base(telemetry)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets submission via id",
            OperationId = "submissions/get"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Submission))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> GetAsync([SwaggerParameter("Submission id", Required = true)] string id)
        {
            Telemetry.TraceInfo("requested", new { message = $"Submission with id {id} requested" });

            var match = await _repository.GetAsync(id);
            return match != null 
                ? Ok(match)
                : NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound, $"Entity with id '{id}' was not found"));
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates submission TEST",
            OperationId = "submissions/create"
        )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Submission))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(ApiErrorResponse))]
        public async Task<IActionResult> PostAsync(Submission submission)
        {
            Telemetry.TraceInfo("received", submission);

            await _repository.CreateAsync(submission);
            return CreatedAtAction("Get", new { id = submission.Id }, submission);
        }
    }
}
