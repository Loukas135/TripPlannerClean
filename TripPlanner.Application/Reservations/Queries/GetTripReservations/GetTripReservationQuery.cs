using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetTripReservations
{
	public class GetTripReservationQuery(int serId) : IRequest<IEnumerable<TripReservationDto>>
	{
		public int ServiceId { get; set; }
	}
}
