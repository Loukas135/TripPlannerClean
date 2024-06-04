using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;

namespace TripPlanner.Application.Reservations.Queries.GetCarReservations
{
	public class GetCarReservationsQuery(int serId, int carId) : IRequest<IEnumerable<ReservationDto?>>
	{
		public int ServiceId { get; } = serId;
		public int CarId { get; } = carId;
	}
}
