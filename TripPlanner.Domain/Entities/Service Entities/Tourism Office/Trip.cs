using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Tourism_Office
{
	public class Trip
	{
		public int Id { get; set; }
		public DateOnly From { get; set; } = default!;
		public DateOnly To { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Days { get; set; }
		public float Price { get; set; }

		public int ServiceId { get; set; }

		public List<Reservation>? Reservations { get; set; }
	}
}
