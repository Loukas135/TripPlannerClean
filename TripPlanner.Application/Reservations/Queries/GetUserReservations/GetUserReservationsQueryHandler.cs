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

namespace TripPlanner.Application.Reservations.Queries.GetUserReservations
{
	public class GetUserReservationsQueryHandler(IReservationRespository reservationRespository,
		IMapper mapper, IUserContext userContext)
		: IRequestHandler<GetUserReservationsQuery, IEnumerable<ReservationDto>>
	{
		public async Task<IEnumerable<ReservationDto>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
		{
			var currentUserId = userContext.GetCurrentUser().Id;

			var reservations = await reservationRespository.GetUserReservations(currentUserId);

			var result = mapper.Map<IEnumerable<ReservationDto>>(reservations);

			return result;
		}
	}
}
