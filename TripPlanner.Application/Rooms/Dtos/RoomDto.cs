using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Rooms.Dtos
{
	public class RoomDto
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string Description { get; set; } = default!;
		public int NumberOfPeople { get; set; }
		public int Quantity { get; set; }
		public float PricePerNight { get; set; }
		public string? ImagePath { get; set; }
		public List<ReservationDto>? Reservations { get; set; }
		public int RoomCategoryId { get; set; }
	}
}
