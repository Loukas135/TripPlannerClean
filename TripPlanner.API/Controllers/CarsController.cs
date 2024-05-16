using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Cars.Commands.CreateCar;
using TripPlanner.Application.Cars.Commands.DeleteCar;
using TripPlanner.Application.Cars.Dtos;
using TripPlanner.Application.Cars.Queries.GetAllCars;
using TripPlanner.Application.Cars.Queries.GetCarById;
using TripPlanner.Application.Reservations.Commands.Car;

namespace TripPlanner.API.Controllers
{
	[ApiController]
	[Route("api/service/{serId}/cars")]
	public class CarsController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		[Route("{ccId}")]
		public async Task<IActionResult> AddCarForService(int serId, int ccId, CreateCarCommand command)
		{
			command.ServiceId = serId;
			command.CarCategoryId = ccId;
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetCarByIdForService), new { serId, id }, null);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCarsForService(int serId)
		{
			var cars = await mediator.Send(new GetAllCarsQuery(serId));
			return Ok(cars);
		}

		[HttpGet]
		[Route("{carId}")]
		public async Task<ActionResult<CarDto>> GetCarByIdForService(int serId, int carId)
		{
			var car = await mediator.Send(new GetCarByIdQuery(serId, carId));
			return Ok(car);
		}

		[HttpDelete]
		[Route("{carId}")]
		public async Task<IActionResult> DeleteCarForService(int serId, int carId)
		{
			await mediator.Send(new DeleteCarCommand(serId, carId));
			return NoContent();
		}

		[HttpPost]
		[Route("/reservations/{carId}")]
		public async Task<IActionResult> AddReservation(int serId, int carId, ReserveCarCommand command)
		{
			command.ServiceId = serId;
			command.CarId = carId;
			int id = await mediator.Send(command);
			return Ok(id);
		}
	}
}

