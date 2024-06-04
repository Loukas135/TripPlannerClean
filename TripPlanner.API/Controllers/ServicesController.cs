using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Application.Reservations.Queries.GetServiceReservations;
using TripPlanner.Application.Services.Commands.CreateService;
using TripPlanner.Application.Services.Commands.DeleteService;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Application.Services.Queries.GetAllServices;
using TripPlanner.Application.Services.Queries.GetServiceById;
using TripPlanner.Application.Services.Queries.GetServiceByType;
using TripPlanner.Domain.Entities;

namespace TripPlanner.API.Controllers
{
    [ApiController]
	[Route("api/governorates/{governorateId}/services")]
	public class ServicesController(IMediator mediator) : ControllerBase
	{
		//private static List<string> AllowedRoles = ["User","HotelOwner", "CarRental", "TourismOffice", "Restaurant"];
		[HttpPost]
		[Route("servicetypes/{serviceTypeId}")]
		[Authorize(Roles = "Administrator,HotelOwner")]
		public async Task<IActionResult> AddService([FromRoute]int governorateId, [FromRoute]int serviceTypeId, [FromBody] CreateServiceCommand command)
		{
			command.GovernorateId = governorateId;
			command.ServiceTypeId = serviceTypeId;
			int serviceId = await mediator.Send(command);
			return CreatedAtAction(nameof(GetServiceById), new { governorateId, serviceId }, null);
			//I shouldn't forget to add rate to services table
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServicesFromGovernorate(int governorateId)
        {
			var services = await mediator.Send(new GetAllServicesQuery(governorateId));
			return Ok(services);
		}

		[HttpDelete]
		[Route("{serviceId}")]
		public async Task<IActionResult> DeleteService(int governorateId, int serviceId)
		{
			await mediator.Send(new DeleteServiceCommand(governorateId, serviceId));
			return NoContent();
		}

		[HttpGet]
		[Route("{serviceId}")]
		public async Task<ActionResult<ServiceDto>> GetServiceById(int governorateId, int serviceId)
		{
			var service = await mediator.Send(new GetServiceByIdQuery(governorateId, serviceId));
			return Ok(service);
		}
		[HttpGet]
		[Route("serviceTypes/{serviceTypeId}")]
		public async Task<ActionResult<IEnumerable<ServiceDto>>> GetServiceByType(int governorateId, int serviceTypeId)
		{
			var request = new GetServiceByTypeQuery(governorateId, serviceTypeId);
			var result=await mediator.Send(request);
			if (result == null)
			{
				return NotFound();
			}

            return Ok(result);
		}

		[HttpGet]
		[Route("{serviceId}/reservations")]
		public async Task<ActionResult<IEnumerable<ReservationDto>>> GetServiceReservations(int governorateId, int serviceId)
		{
			var reservations = await mediator.Send(new GetServiceReservationsQuery(governorateId, serviceId));
			return Ok(reservations);
		}
		
	}
}

/*
{
  "name": "majd motors",
  "address": "al nejme square",
  "description": "we have the fastest cars",
  "contactNumber": "+96395557777",
  "contactEmail": "motors@contact.com",
  "governorateId": 0,
  "serviceTypeId": 0,
  "ownerId": "1c554b54-bd7c-46af-b7fd-9f1592ea3e0d",
  "hasWiFi": true,
  "hasPool": true,
  "hasRestaurant": true
} 
*/