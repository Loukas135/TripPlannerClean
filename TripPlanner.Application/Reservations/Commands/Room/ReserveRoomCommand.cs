using MediatR;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Commands.Room
{
	public class ReserveRoomCommand : IRequest<int>
	{
		public int ServiceId { get; set; }
		public int RoomId { get; set; }
		public int Nights { get; set; }
		public string From { get; set; } = default!;
		public string To { get; set; } = default!;
		public string Payment { get; set; } = default!;
	}
}
