using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetServiceReservations
{
	public class GetServiceReservationsQuery(int govId, int serId) : IRequest<IEnumerable<ReservationDto?>>
	{
		public int GovernorateId { get; } = govId;
		public int ServiceId { get; } = serId;
	}
}
