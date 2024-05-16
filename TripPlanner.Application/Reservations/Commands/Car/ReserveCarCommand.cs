using MediatR;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Commands.Car
{
	public class ReserveCarCommand : IRequest<int>
	{
		public int ServiceId { get; set; }
		public int CarId { get; set; }
		public string From { get; set; } = default!;
		public string To { get; set; } = default!;
		public int Months { get; set; }
		public string Payment { get; set; } = default!;
	}
}