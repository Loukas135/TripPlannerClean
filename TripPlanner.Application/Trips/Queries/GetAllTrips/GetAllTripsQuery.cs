using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Trips.Dtos;

namespace TripPlanner.Application.Trips.Queries.GetAllTrips
{
	public class GetAllTripsQuery(int serId) : IRequest<IEnumerable<TripDto>>
	{
		public int ServiceId { get; } = serId;
	}
}
