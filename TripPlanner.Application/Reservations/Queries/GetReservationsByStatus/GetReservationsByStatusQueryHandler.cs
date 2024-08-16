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
		IMapper mapper, IUserContext userContext,
		IAccountRepository accountRepository)
		: IRequestHandler<GetReservationsByStatusQuery, IEnumerable<ReservationDto>>
	{
		public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsByStatusQuery request, CancellationToken cancellationToken)
		{
			var currentUser = userContext.GetCurrentUser();
			var currentUserId = currentUser.Id;
			var reservations = await reservationRespository.GetByStatus(request.Status, currentUserId);

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			foreach (var result in results)
			{
				var user = await accountRepository.GetUserAsync(currentUserId);

                result.UserName = user.UserName;
			}
			return results;
		}
	}
}
