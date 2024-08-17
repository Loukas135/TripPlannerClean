using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TripPlanner.Application.Reservations.Commands.ChangeStatus;
using TripPlanner.Application.Reservations.Commands.Delete;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetAll;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetByDate;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetInCurrentMonth;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetInServiceType;
using TripPlanner.Application.Reservations.Queries.GetReservationsByStatus;
using TripPlanner.Application.Reservations.Queries.GetUserReservations;

//this controller exsists to help the admin filter reservations by: current month/all in gov/service/....
namespace TripPlanner.API.Controllers
{
	[ApiController]
	[Route("api/governorates/{governorateId}/reservations/")]
	public class ReservationController(IMediator mediator) : ControllerBase
	{
		
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllInGovernorate(int governorateId) 
		{
			var reservations = await mediator.Send(new GetAllReservationsQuery(governorateId));
			return Ok(reservations);
		}

		
		[HttpGet]
		[Route("ThisMonth")]
		public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllCurrentMonth(int governorateId)
		{
			var reservations = await mediator.Send(new GetReservationsInCurrentMonthQuery(governorateId));
			return Ok(reservations);
		}
		
		[HttpGet]
		[Route("serviceType/{serviceTypeId}")]
		public async Task<ActionResult<ReservationDto>> GetAllInServiceType(int governorateId, int serviceTypeId)
		{
			var reservations = await mediator.Send(new GetReservationsInServiceTypeQuery(governorateId, serviceTypeId));
			return Ok(reservations);
		}

		[HttpGet]
		[Route("{year}/{month}")]
		public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllInSpecificDate(int governorateId,
			[FromRoute]int year, [FromRoute] int month)
		{
			var reservations = await mediator.Send(new GetReservationsByDateQuery(year, month, governorateId));
			return Ok(reservations);
		}

		[HttpPut]
		[Route("/api/[controller]/{reservationId}")]
		public async Task<ActionResult> ChangeReservationStatus([FromRoute]int reservationId, [FromBody]ChangeReservationStatusCommand command)
		{
			command.reservationId=reservationId;
			await mediator.Send(command);
			return Ok();
		}
		[HttpGet]
		[Route("/api/[controller]/services/{serviceId}")]
		public async Task<ActionResult> GetReservationsInServiceInDate([FromRoute]int serviceId,
			[FromQuery]int year, [FromQuery] int month)
		{
			var response = new GetReservationsByDateQuery(serviceId, year, month);
			if (response == null)
			{
				return NoContent();
			}
			return Ok(response);
		}

		[HttpGet]
		[Route("/api/[controller]/reservations/currentUser")]
		public async Task<ActionResult> GetReservationsForCurrentUser()
		{
			var reservations = await mediator.Send(new GetUserReservationsQuery());
			return Ok(reservations);
		}

		[HttpGet]
		[Route("/api/[controller]/reservations/{status}")]
		public async Task<ActionResult> GetReservationsByStatus([FromRoute]string status)
		{
			var reservations = await mediator.Send(new GetReservationsByStatusQuery(status));
			return Ok(reservations);
		}

		[HttpDelete]
		[Route("/api/[controller]/cancel/{reservationId}")]
		public async Task<ActionResult> CancelReservation(int reservationId)
		{
			await mediator.Send(new DeleteReservationCommand(reservationId));
			return NoContent();
		}
	}
}