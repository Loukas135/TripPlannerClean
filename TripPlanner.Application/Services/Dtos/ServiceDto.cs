using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;

namespace TripPlanner.Application.Services.Dtos
{
	public class ServiceDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Address { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string ContactNumber { get; set; } = default!;
		public string? ContactEmail { get; set; }

		public List<Room>? Rooms { get; set; }
		public List<Car>? Cars { get; set; }
		public List<Trip>? Trips { get; set; }
	}
}
