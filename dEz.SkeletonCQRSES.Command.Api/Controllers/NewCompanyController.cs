using dEz.SkeletonCQRSES.Command.Api.Commands;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Query.Domain.Services;
using dEz.SkeletonCQRSES.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace dEz.SkeletonCQRSES.Command.Api.Controllers
{
    [Route("api/company")]
    [ApiController]
    public class NewCompanyController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandDispatcher"></param>
        /// <param name="companyService"></param>
        public NewCompanyController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost("cqrs")]
        public async Task<IActionResult> NewCompanyAsync([FromBody] AddCompanyCommand command)
        {
            var id = Guid.NewGuid();
            command.Id = id;

            await _commandDispatcher.SendAsync(command);

            return StatusCode(StatusCodes.Status201Created, new
            {
                Id = id,
                Message = "New company creation request completed successfully!"
            });
        }
    }
}
