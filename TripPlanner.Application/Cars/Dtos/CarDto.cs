using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Cars.Dtos
{
	public class CarDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public float PricePerMonth { get; set; }
		public int Quantity { get; set; }

		public List<ReservationDto>? Reservations { get;}
	}
}
