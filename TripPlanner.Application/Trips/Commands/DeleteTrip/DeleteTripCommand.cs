using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Trips.Commands.DeleteTrip
{
	public class DeleteTripCommand(int serId, int tripId) : IRequest
	{
		public int ServiceId { get; } = serId;
		public int TripId { get; } = tripId;
	}
}
