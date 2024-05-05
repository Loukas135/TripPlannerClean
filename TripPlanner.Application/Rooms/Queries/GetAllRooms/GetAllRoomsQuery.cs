using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Rooms.Dtos;

namespace TripPlanner.Application.Rooms.Queries.GetAllRooms
{
	public class GetAllRoomsQuery(int serId) : IRequest<IEnumerable<RoomDto>>
	{
		public int ServiceId { get; } = serId;
	}
}
