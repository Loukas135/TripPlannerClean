using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Rooms.Dtos;

namespace TripPlanner.Application.Rooms.Queries.GetRoomById
{
	public class GetRoomByIdQuery(int serId, int roomId) : IRequest<RoomDto>
	{
		public int ServiceId { get; } = serId;
		public int RoomId { get; } = roomId;
	}
}
