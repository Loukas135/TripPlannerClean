using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Statuses
{
	public class GetReservationStatusesQueryHandler(IReservationRespository reservationRespository)
		: IRequestHandler<GetReservationStatusesQuery, IEnumerable<Status>>
	{
		public async Task<IEnumerable<Status>> Handle(GetReservationStatusesQuery request, CancellationToken cancellationToken)
		{
			return await reservationRespository.GetStatuses();
		}
	}
}
