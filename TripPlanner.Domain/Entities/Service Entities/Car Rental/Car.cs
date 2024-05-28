using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Car_Rental
{
	public class Car
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public float PricePerMonth { get; set; }
		public int Quantity { get; set; }

		public int CarCategoryId { get; set; }
		public int ServiceId { get; set; }

		public List<Reservation>? Reservations { get; set; }

		public List<IFormFile> CarImages { get; set; } = new List<IFormFile>();
	}
}
