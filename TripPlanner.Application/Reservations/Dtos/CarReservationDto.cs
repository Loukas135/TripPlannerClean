using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Reservations.Dtos
{
	public class CarReservationDto : ReservationDto
	{
		public int CarId { get; set; }
	}
}
