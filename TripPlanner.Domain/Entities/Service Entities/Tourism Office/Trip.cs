using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;

namespace TripPlanner.Domain.Entities.Service_Entities.Tourism_Office
{
	public class Trip
	{
		public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime From { get; set; } = default!;
        [DataType(DataType.Date)]
        public DateTime To { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int Days { get; set; }
		public float Price { get; set; }

		public int ServiceId { get; set; }

		public List<Reservation>? Reservations { get; set; }

		public List<IFormFile> TripImages { get; set; } = new List<IFormFile>();
	}
}
