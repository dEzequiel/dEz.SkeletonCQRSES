using dEz.SkeletonCQRSES.Command.Api.Commands;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace dEz.SkeletonCQRSES.Command.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "CommandApi")]
    [ApiController]
    public class DeleteCompanyController : ControllerBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="commandDispatcher"></param>
        public DeleteCompanyController(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteCompanyAsync(Guid id, DeleteCompanyCommand command)
        {
            try
            {
                command.Id = id;
                await _commandDispatcher.SendAsync(command);
                return Ok(new
                {
                    Id = id,
                    Message = "Delete company request completed successfully!"
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
                const string SAFE_ERROR_MESSAGE = "Error while processing request to delete a company!";
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Id = id,
                    Message = SAFE_ERROR_MESSAGE
                });
            }
        }
    }
}
