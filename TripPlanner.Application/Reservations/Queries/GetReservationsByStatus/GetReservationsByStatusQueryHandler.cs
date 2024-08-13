using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetReservationsByStatus
{
	public class GetReservationsByStatusQueryHandler(IReservationRespository reservationRespository,
		IMapper mapper, IUserContext userContext)
		: IRequestHandler<GetReservationsByStatusQuery, IEnumerable<ReservationDto>>
	{
		public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsByStatusQuery request, CancellationToken cancellationToken)
		{
			var currentUserId = userContext.GetCurrentUser().Id;
			var reservations = await reservationRespository.GetByStatus(request.Status, currentUserId);

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			return results;
		}
	}
}
