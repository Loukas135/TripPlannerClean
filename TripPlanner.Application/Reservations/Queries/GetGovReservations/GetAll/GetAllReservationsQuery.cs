using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetGovReservations.GetAll
{
	public class GetAllReservationsQuery(int govId) : IRequest<IEnumerable<ReservationDto>>
	{
		public int GovernorateId { get; } = govId;
	}
}
