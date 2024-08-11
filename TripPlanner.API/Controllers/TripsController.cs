using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Reservations.Commands.Trips;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Application.Reservations.Queries.GetCarReservations;
using TripPlanner.Application.Reservations.Queries.GetTripReservations;
using TripPlanner.Application.Trips.Commands.CreateTrip;
using TripPlanner.Application.Trips.Commands.DeleteTrip;
using TripPlanner.Application.Trips.Commands.UpdateTrip;
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
		public async Task<IActionResult> AddTripForService(int serId, [FromForm] CreateTripCommand command)
		{
			command.ServiceId = serId;
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetTripByIdFromService), new { serId, tripId = id }, null);
		}

		[HttpGet]
		[Route("{tripId}")]
		public async Task<ActionResult<TripDto>> GetTripByIdFromService([FromRoute]int serId, int tripId)
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

		[HttpPatch]
		[Route("{tripId}")]
		public async Task<IActionResult> UpdateTrip([FromRoute]int tripId, [FromForm]UpdateTripCommand command)
		{
			command.TripId = tripId;
			await mediator.Send(command);
			return NoContent();
		}

		[HttpPost]
		[Route("tripreservations/{tripId}")]
		public async Task<IActionResult> AddTripReservation(ReserveTripCommand command, [FromRoute]int tripId, int serId)
		{
			command.TripId = tripId;
			command.ServiceId = serId;
			int id = await mediator.Send(command);
			return Ok(id);
		}

		[HttpGet]
		[Route("reservations")]
		public async Task<ActionResult<IEnumerable<TripReservationDto>>> GetTripReservations([FromRoute] int serId)
		{
			var reservations = await mediator.Send(new GetTripReservationQuery(serId));
			return Ok(reservations);
		}
	}
}

