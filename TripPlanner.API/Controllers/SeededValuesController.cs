using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Governorates.Queries;
using TripPlanner.Application.Roles.Queries;
using TripPlanner.Application.ServiceTypes.Queries;
using TripPlanner.Application.ServiceTypes.Queries.GetAllServiceTypes;

namespace TripPlanner.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class SeededValuesController(IMediator mediator):ControllerBase
    {
        [HttpGet]
        [Route("ServiceTypes")]
        public async Task<IActionResult> GetAllServiceTypes()
        {
            var query = new GetAllServiceTypesQuery();
            var serviceTypes = await mediator.Send(query);
            return Ok(serviceTypes);
        }
        [HttpGet]
        [Route("Roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var query = new GetRolesQuery();
            var roles = await mediator.Send(query);
            return Ok(roles);
        }
        [HttpGet]
        [Route("Governorates")]
        public async Task<IActionResult> GetAllGovernorates()
        {
            var query = new GetGovernoreatesQuery();
            var governorates = await mediator.Send(query);
            return Ok(governorates);
        }
    }
}
