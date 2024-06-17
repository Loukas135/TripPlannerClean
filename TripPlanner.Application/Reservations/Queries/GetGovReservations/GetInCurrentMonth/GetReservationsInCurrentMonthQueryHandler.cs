using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetGovReservations.GetInCurrentMonth
{
	public class GetReservationsInCurrentMonthQueryHandler(IReservationRespository reservationRespository,
		IMapper mapper) : IRequestHandler<GetReservationsInCurrentMonthQuery, IEnumerable<ReservationDto>>
	{
		public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsInCurrentMonthQuery request, CancellationToken cancellationToken)
		{
			var reservations = await reservationRespository.GetAllInCurrentMonth(request.GovernorateId);

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			return results;
		}
	}
}
