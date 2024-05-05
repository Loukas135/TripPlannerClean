using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Trips.Commands.CreateTrip
{
	public class CreateTripCommand : IRequest<int>
	{
		public string From { get; set; } = default!;
		public string To { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Days { get; set; }
		public float Price { get; set; }

		public int ServiceId { get; set; }
	}
}
