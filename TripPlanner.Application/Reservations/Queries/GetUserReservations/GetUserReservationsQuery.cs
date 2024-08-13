using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetUserReservations
{
	public class GetUserReservationsQuery : IRequest<IEnumerable<ReservationDto>>
	{

	}
}
