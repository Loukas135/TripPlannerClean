using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.CarTypes.Queries.GetAllCarTypes;
using TripPlanner.Application.Governorates.Queries;
using TripPlanner.Application.Roles.Queries;
using TripPlanner.Application.Rooms.RoomCatergories.Queries.GetAllRoomCategories;
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

        [HttpGet]
        [Route("CarCategories")]
        public async Task<IActionResult> GetAllCarCategories()
        {
            var query = new GetAllCarCategoriesQuery();
            var cars = await mediator.Send(query);
            return Ok(cars);
        }

		[HttpGet]
		[Route("RoomCategories")]
		public async Task<IActionResult> GetAllRoomCategories()
		{
			var query = new GetAllRoomCategoriesQuery();
			var rooms = await mediator.Send(query);
			return Ok(rooms);
		}
	}
}
