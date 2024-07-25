using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Cars.Commands.UpdateCar;
using TripPlanner.Application.Reservations.Commands.Room;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Application.Reservations.Queries.GetCarReservations;
using TripPlanner.Application.Reservations.Queries.GetRoomReservations;
using TripPlanner.Application.Rooms.Commands.CreateRoom;
using TripPlanner.Application.Rooms.Commands.DeleteRoom;
using TripPlanner.Application.Rooms.Commands.UpdateRoom;
using TripPlanner.Application.Rooms.Dtos;
using TripPlanner.Application.Rooms.Queries.GetAllRooms;
using TripPlanner.Application.Rooms.Queries.GetRoomById;

namespace TripPlanner.API.Controllers
{
	[ApiController]
	[Route("api/services/{serId}/rooms")]
	public class RoomController(IMediator mediator) : ControllerBase
	{
		[HttpPost]
		[Route("{rcId}")]
		public async Task<IActionResult> AddRoomForService(int serId, int rcId,[FromForm] CreateRoomCommand command)
		{
			command.ServiceId = serId;
			command.RoomCategoryId = rcId;
			int roomId = await mediator.Send(command);
			return CreatedAtAction(nameof(GetRoomFromService), new { serId, roomId }, null);
		}

		[HttpDelete]
		[Route("{roomId}")]
		public async Task<IActionResult> DeleteRoomForService(int serId, int roomId)
		{
			await mediator.Send(new DeleteRoomCommand(serId, roomId));
			return NoContent();
		}

		[HttpGet]
		[Route("{roomId}")]
		public async Task<ActionResult<RoomDto>> GetRoomFromService(int serId, int roomId)
		{
			var room = await mediator.Send(new GetRoomByIdQuery(serId, roomId));
			return Ok(room);
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<RoomDto>>> GetRoomsFromService(int serId)
		{
			var rooms = await mediator.Send(new GetAllRoomsQuery(serId));
			return Ok(rooms);
		}

		[HttpPatch]
		[Route("{roomId}")]
		public async Task<IActionResult> UpdateRoom([FromRoute] int roomId, UpdateRoomCommand command)
		{
			command.RoomId = roomId;
			await mediator.Send(command);
			return NoContent();
		}

		[HttpPost]
		[Route("roomreservations/{roomId}")]
		public async Task<IActionResult> AddRoomReservation(ReserveRoomCommand command, int serId, int roomId)
		{
			command.RoomId = roomId;
			command.ServiceId = serId;
			int id = await mediator.Send(command);
			return Ok(id);
		}

		[HttpGet]
		[Route("reservations")]
		public async Task<ActionResult<IEnumerable<RoomReservationDto>>> GetRoomReservations([FromRoute] int serId)
		{
			var reservations = await mediator.Send(new GetRoomReservationQuery(serId));
			return Ok(reservations);
		}

		[HttpGet]
		[Route("image")]
		public IActionResult GetImage()
		{
			var image = System.IO.File.OpenRead("C:\\Users\\Loukas\\source\\repos\\TripPlannerClean\\TripPlanner.API\\Images/Rooms\\4c165a25-8689-427b-b63a-3dd1762e3b87.zip");
			return File(image, "application/zip");
		}
	}
}
