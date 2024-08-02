using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using TripPlanner.Application.Reservations.Commands.ChangeStatus;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetAll;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetByDate;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetInCurrentMonth;
using TripPlanner.Application.Reservations.Queries.GetGovReservations.GetInServiceType;

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
		public async Task<ActionResult> ChangeReservationStatus([FromRoute]int reservationId, [FromQuery]bool isAccepted)
		{
			var request = new ChangeReservationStatusCommand(reservationId, isAccepted);
			await mediator.Send(request);
			return Ok();
		}


	}
}