using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Cars.Commands.CreateCar;
using TripPlanner.Application.Cars.Commands.DeleteCar;
using TripPlanner.Application.Cars.Commands.UpdateCar;
using TripPlanner.Application.Cars.Dtos;
using TripPlanner.Application.Cars.Queries.GetAllCars;
using TripPlanner.Application.Cars.Queries.GetCarById;
using TripPlanner.Application.Reservations.Commands.Car;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Application.Reservations.Queries.GetCarReservations;
using TripPlanner.Application.Trips.Commands.UpdateTrip;

namespace TripPlanner.API.Controllers
{
	[ApiController]
	[Route("api/services/{serId}/cars")]
	public class CarsController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		[Route("{ccId}")]
		public async Task<IActionResult> AddCarForService([FromRoute]int serId, [FromRoute]int ccId, [FromForm]CreateCarCommand command)
		{
		
			command.ServiceId = serId;
			command.CarCategoryId = ccId;
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetCarByIdForService), new { serId, carId=id }, null);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<CarDto>>> GetAllCarsForService(int serId)
		{
			var cars = await mediator.Send(new GetAllCarsQuery(serId));
			if (cars == null)
			{
				return NotFound();
			}
			return Ok(cars);
		}

		[HttpGet]
		[Route("{carId}")]
		public async Task<ActionResult<CarDto>> GetCarByIdForService(int serId, int carId)
		{
			var car = await mediator.Send(new GetCarByIdQuery(serId, carId));
			if (car == null)
			{
				return NotFound();
			}
			return Ok(car);
		}

		[HttpDelete]
		[Route("{carId}")]
		public async Task<IActionResult> DeleteCarForService(int serId, int carId)
		{
			await mediator.Send(new DeleteCarCommand(serId, carId));
			return NoContent();
		}

		[HttpPatch]
		[Route("{carId}")]
		public async Task<IActionResult> UpdateCar([FromRoute] int carId, [FromForm]UpdateCarCommand command)
		{
			command.CarId = carId;
			await mediator.Send(command);
			return NoContent();
		}

		[HttpPost]
		[Route("carreservations/{carId}")]
		public async Task<IActionResult> AddCarReservation(int serId, int carId, ReserveCarCommand command)
		{
			command.ServiceId = serId;
			command.CarId = carId;
			int id = await mediator.Send(command);
			return Ok(id);
		}

		[HttpGet]
		[Route("reservations")]
		public async Task<ActionResult<IEnumerable<CarReservationDto>>> GetCarReservations([FromRoute]int serId)
		{
			var reservations = await mediator.Send(new GetCarReservationsQuery(serId));
			return Ok(reservations);
		}
	}
}

