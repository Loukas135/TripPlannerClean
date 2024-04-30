using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Domain.Entities
{
	public class Reservation
	{
		public int Id { get; set; }
		public int Cost { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }

		public int ServiceId { get; set; }

		public int? RoomId { get; set; }

		public int? TripId { get; set; }

		public int? CarId { get; set; }
	}
}
