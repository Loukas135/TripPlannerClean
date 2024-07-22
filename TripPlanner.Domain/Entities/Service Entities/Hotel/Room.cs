using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Hotel
{
	public class Room
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string Description { get; set; } = default!;
		public int NumberOfPeople { get; set; }
		public int Quantity { get; set; }
		public float PricePerNight { get; set; }

		public int RoomCategoryId { get; set; }
		public int ServiceId { get; set; }

		public List<Reservation>? Reservations { get; set; }
		public string? ImagePath { get; set; }

		//public List<IFormFile> RoomImages { get; set; } = new List<IFormFile>();
	}
}
