using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json.Nodes;
using TripPlanner.Application.CarTypes.Queries.GetAllCarTypes;
using TripPlanner.Application.Governorates.Queries;
using TripPlanner.Application.Roles.Queries;
using TripPlanner.Application.Rooms.RoomCatergories.Queries.GetAllRoomCategories;
using TripPlanner.Application.ServiceTypes.Queries;
using TripPlanner.Application.ServiceTypes.Queries.GetAllServiceTypes;
using TripPlanner.Domain.Entities;

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
        [Route("NonAdminRoles")]
        public async Task <ActionResult> GetRolesNotAdmin()
        {
            var query = new GetRolesQuery();
            var roles = await mediator.Send(query);
            var neededRoles = roles.Where(r => r.Name != "Administrator");
            return Ok(neededRoles);
        }
        [HttpGet]
        [Route("Governorates")]
        public async Task<ActionResult> GetAllGovernorates()
        {
            var query = new GetGovernoreatesQuery();
            var governorates = await mediator.Send(query);
            var gov = new { governorates = governorates };
            return Ok(gov);
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
