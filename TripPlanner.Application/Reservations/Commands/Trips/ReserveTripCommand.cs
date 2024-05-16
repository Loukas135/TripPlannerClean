using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Commands.Trips
{
	public class ReserveTripCommand : IRequest<int>
	{
		public int TripId { get; set; }
		public int ServiceId { get; set; }
		public string Payment { get; set; } = default!;
	}
}
