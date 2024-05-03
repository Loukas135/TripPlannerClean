using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Dtos
{
	public class ReservationDto
	{
		public int Id { get; set; }
		public int Cost { get; set; }
		public DateTime From { get; set; }
		public DateTime To { get; set; }
		//public int ServiceId { get; set; }
	}
}
