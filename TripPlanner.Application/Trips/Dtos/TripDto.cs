using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Trips.Dtos
{
	public class TripDto
	{
		public int Id { get; set; }
		public DateOnly From { get; set; } = default!;
		public DateOnly To { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Days { get; set; }
		public float Price { get; set; }
		public string ImagePath { get; set; }

		public List<ReservationDto>? Reservations { get; set; }
	}
}
