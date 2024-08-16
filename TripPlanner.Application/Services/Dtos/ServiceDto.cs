using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Dtos;
using TripPlanner.Application.Rooms.Dtos;
using TripPlanner.Application.Trips.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;

namespace TripPlanner.Application.Services.Dtos
{
	public class ServiceDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
        public int GovernorateId { get; set; }
        public string Address { get; set; } = default!;
		public string Description { get; set; } = default!;
		public string ContactNumber { get; set; } = default!;
		public string? ContactEmail { get; set; }
		public float OverallRate { get; set; } = default!;

        public List<RoomDto>? Rooms { get; set; }
		public List<CarDto>? Cars { get; set; }
		public List<TripDto>? Trips { get; set; }

		public bool HasWiFi { get; set; } = false;
		public bool HasPool { get; set; } = false;
		public bool HasRestaurant { get; set; } = false;
        public string? ImagePath { get; set; }
    }
}
