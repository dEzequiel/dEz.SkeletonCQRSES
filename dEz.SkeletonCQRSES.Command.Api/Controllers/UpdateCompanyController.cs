using dEz.SkeletonCQRSES.Command.Api.Commands;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace dEz.SkeletonCQRSES.Command.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UpdateCompanyController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandDispatcher"></param>
        public UpdateCompanyController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateCompanyAsync(Guid id, [FromBody] UpdateCompanyCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);
                return Ok(new
                {
                    Id = id,
                    Message = "Update company request completed successfully!"
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
                const string SAFE_ERROR_MESSAGE = "Error while processing request to update a company!";
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
