using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Application.Reservations.Statuses
{
	public class GetReservationStatusesQuery : IRequest<IEnumerable<Status>>
	{
	}
}
