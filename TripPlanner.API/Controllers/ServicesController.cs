using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Services.Commands.CreateService;
using TripPlanner.Application.Services.Commands.DeleteService;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Application.Services.Queries.GetAllServices;
using TripPlanner.Application.Services.Queries.GetServiceById;
using TripPlanner.Application.Services.Queries.GetServiceByType;

namespace TripPlanner.API.Controllers
{
    [ApiController]
	[Route("api/governorates/{governorateId}/services")]
	public class ServicesController(IMediator mediator) : ControllerBase
	{
		//private static List<string> AllowedRoles = ["User","HotelOwner", "CarRental", "TourismOffice", "Restaurant"];
		[HttpPost]
		[Route("servicetype/{serviceTypeId}")]
		[Authorize(Roles = "Administrator,HotelOwner")]
		public async Task<IActionResult> AddService(int governorateId, int serviceTypeId, [FromBody] CreateServiceCommand command)
		{
			command.GovernorateId = governorateId;
			command.ServiceTypeId = serviceTypeId;
			int serId = await mediator.Send(command);
			return CreatedAtAction(nameof(GetServiceById), new { governorateId, serId }, null);
			//I shouldn't forget to add rate to services table
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAllServicesFromGovernorate(int govId)
		{
			var services = await mediator.Send(new GetAllServicesQuery(govId));
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
		[Route("serviceType/{serviceTypeId}")]
		public async Task<ActionResult<IEnumerable<ServiceDto>>>GetServiceByType(int governorateId, int serviceTypeId)
		{
			var request = new GetServiceByTypeQuery(governorateId, serviceTypeId);
			var result=await mediator.Send(request);
			if (result == null)
			{
				return NotFound();
			}

            return Ok(result);
		}
		
	}
}
