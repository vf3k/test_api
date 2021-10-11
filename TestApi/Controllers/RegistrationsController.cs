using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApi.Core.Entity;
using TestApi.Core.Services;
using TestApi.DTO;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RegistrationsController : ControllerBase
    {
        private readonly RegistrationService registrationService;
        private readonly ILogger<RegistrationsController> logger;

        public RegistrationsController(RegistrationService registrationService, ILogger<RegistrationsController> logger)
        {
            this.registrationService = registrationService;
            this.logger = logger;
        }

        [HttpPost()]
        public async Task<ActionResult<RegistrationResponse>> Registration(Registration model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);
            var res = await registrationService.CreateAsync(model);
            if (!res)
            {
                logger.LogError(res.Exception, res.Error);
                return StatusCode((int) HttpStatusCode.InternalServerError,
                    new ErrorResponse(res.Error, ErrorCodes.InternalServerError));
            }

            return CreatedAtAction("Registration", new RegistrationResponse {RegistrationId = res.Data.Id});
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetRegistrationResponse>> Registration(Guid id)
        {
            var registration = await registrationService.GetByIdAsync(id);
            if (registration == null)
                return NotFound(new ErrorResponse("Registration not found", ErrorCodes.InternalServerError));

            return Ok(registration);
        }

        /*[HttpGet]
        public async Task<ActionResult<List<Registration>>> Registration()
        {
            return Ok(await registrationService.GetAllAsync());
        }*/
    }
}