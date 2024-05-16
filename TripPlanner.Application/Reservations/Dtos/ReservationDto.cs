using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Dtos
{
	public class ReservationDto
	{
		public int Id { get; set; }
		public int Cost { get; set; }
		public DateOnly From { get; set; } = default!;
		public DateOnly To { get; set; } = default!;
		public int ServiceId { get; set; }
		public string Payment { get; set; } = default!;
		public string UserId { get; set; } = default!;
	}
}
