using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Trips.Dtos;

namespace TripPlanner.Application.Trips.Queries.GetTripById
{
	public class GetTripByIdQuery(int serId, int tripId) : IRequest<TripDto>
	{
		public int ServiceId { get; set; } = serId;
		public int TripId { get; set; } = tripId;
	}
}
