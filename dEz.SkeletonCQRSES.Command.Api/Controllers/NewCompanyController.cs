using dEz.SkeletonCQRSES.Command.Api.Commands;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using dEz.SkeletonCQRSES.Query.Domain.Entities;
using dEz.SkeletonCQRSES.Query.Domain.Services;
using dEz.SkeletonCQRSES.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace dEz.SkeletonCQRSES.Command.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "CommandApi")]
    [ApiController]
    public class NewCompanyController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandDispatcher"></param>
        public NewCompanyController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPost(Name = nameof(NewCompanyController))]
        public async Task<IActionResult> NewCompanyAsync(AddCompanyCommand command)
        {
            var id = Guid.NewGuid();
            try
            {
                command.Id = id;

                await _commandDispatcher.SendAsync(command);

                return CreatedAtRoute(nameof(NewCompanyController), new
                {
                    Id = id,
                    Message = "New company creation request completed successfully!"
                });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new 
                {
                    Message = ex.Message
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to create a new company!";
                return StatusCode(StatusCodes.Status500InternalServerError, new 
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }

    }
}
