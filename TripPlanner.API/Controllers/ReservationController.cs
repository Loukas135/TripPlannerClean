using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Reservations.Dtos;

//this controller exsists to help the admin filter reservations by: current month/all in gov/service/....
namespace TripPlanner.API.Controllers
{
	[ApiController]
	[Route("api/governorates/{governorateId}/reservations/")]
	public class ReservationController : ControllerBase
	{
		/*
		[HttpGet]
		public Task<ActionResult<ReservationDto>> GetAllInGovernorate(int governorateId) 
		{

		}

		
		[HttpGet]
		public Task<ActionResult<ReservationDto>> GetAllCurrentMonth(int governorateId)
		{

		}

		[HttpGet]
		[Route("serviceType/serviceTypeId")]
		public Task<ActionResult<ReservationDto>> GetAllInServiceType(int governorateId, int serviceTypeId)
		{

		}
		*/
	}
}
