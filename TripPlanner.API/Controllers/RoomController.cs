using MediatR;
using Microsoft.AspNetCore.Mvc;
using TripPlanner.Application.Rooms.Commands.CreateRoom;
using TripPlanner.Application.Rooms.Commands.DeleteRoom;
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
		public async Task<IActionResult> AddRoomForService(int serId, int rcId, CreateRoomCommand command)
		{
			command.ServiceId = serId;
			command.RoomCategoryId = rcId;
			int id = await mediator.Send(command);
			return CreatedAtAction(nameof(GetRoomFromService), new { serId, id }, null);
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
	}
}
