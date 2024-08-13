using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetReservationsByStatus
{
	public class GetReservationsByStatusQuery(string status) : IRequest<IEnumerable<ReservationDto>>
	{
		public string Status { get; } = status;
	}
}
