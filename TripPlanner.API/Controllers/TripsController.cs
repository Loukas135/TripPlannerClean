using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Trips.Commands.CreateTrip;
using TripPlanner.Application.Trips.Commands.DeleteTrip;
using TripPlanner.Application.Trips.Dtos;
using TripPlanner.Application.Trips.Queries.GetAllTrips;
using TripPlanner.Application.Trips.Queries.GetTripById;

namespace TripPlanner.API.Controllers
{
	[ApiController]
	[Route("api/services/{serId}/trips")]
	public class TripsController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		public async Task<IActionResult> AddTripForService(int serId, CreateTripCommand command)
		{
			command.ServiceId = serId;
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetTripByIdFromService), new { serId, id }, null);
		}

		[HttpGet]
		[Route("{tripId}")]
		public async Task<ActionResult<TripDto>> GetTripByIdFromService(int serId, int tripId)
		{
			var trip = await mediator.Send(new GetTripByIdQuery(serId, tripId));
			return Ok(trip);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<TripDto>>> GetAllTripsFromService(int serId)
		{
			var trips = await mediator.Send(new GetAllTripsQuery(serId));
			return Ok(trips);
		}

		[HttpDelete]
		[Route("{tripId}")]
		public async Task<ActionResult<TripDto>> DeleteTripFromService(int serId, int tripId)
		{
			await mediator.Send(new DeleteTripCommand(serId, tripId));
			return NoContent();
		}
	}
}
