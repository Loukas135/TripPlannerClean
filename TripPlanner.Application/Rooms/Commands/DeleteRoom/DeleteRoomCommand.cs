using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Rooms.Commands.DeleteRoom
{
	public class DeleteRoomCommand(int serId, int roomId) : IRequest
	{
		public int ServiceId { get; } = serId;
		public int RoomId { get; } = roomId;
	}
}
