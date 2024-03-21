using dEz.Skeleton.CQRSES.Query.Api.Queries;
using dEz.SkeletonCQRSES.ES.Core.Infrastructure;
using dEz.SkeletonCQRSES.Query.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace dEz.Skeleton.CQRSES.Query.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiExplorerSettings(GroupName = "QueryApi")]
    [ApiController]
    public class CompanyLookupController : ControllerBase
    {
        private readonly IQueryDispatcher<Company> _queryDispatcher;

        /// <summary>
        /// Controller.
        /// </summary>
        /// <param name="queryDispatcher"></param>
        public CompanyLookupController(IQueryDispatcher<Company> queryDispatcher)
        {
            _queryDispatcher = queryDispatcher;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCompaniesAsync()
        {
            try
            {
                var companies = await _queryDispatcher.SendAsync(new FindAllCompaniesQuery());

                if (companies is null || !companies.Any()) 
                    return NoContent();

                var count = companies.Count();
                return Ok(new
                {
                    Companies = companies,
                    Message = $"Successfully returned {count} {(count > 1 ? "companies" : "company")}!"
                });

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = "Error while prcessing request to retrieve all companies!"
                });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetCompanyById(Guid id)
        {
            try
            {
                var company = await _queryDispatcher.SendAsync(new FindCompanyByIdQuery() { Id = id });

                if (company is null || !company.Any())
                    return NoContent();

                return Ok(new
                {
                    Companies = company,
                    Message = $"Successfully returned company!"
                });

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    Message = $"Error while prcessing request to retrieve company with id {id.ToString()}!"
                });
            }
        }
    }
}
