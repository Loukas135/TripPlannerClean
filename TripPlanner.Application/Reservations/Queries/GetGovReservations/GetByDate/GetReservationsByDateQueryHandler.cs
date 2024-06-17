using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetGovReservations.GetByDate
{
	public class GetReservationsByDateQueryHandler(IReservationRespository reservationRespository,
		IMapper mapper) : IRequestHandler<GetReservationsByDateQuery, IEnumerable<ReservationDto>>
	{
		public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsByDateQuery request, CancellationToken cancellationToken)
		{
			var reservations = await reservationRespository.GetReservationsByDate(request.Year, request.Month,
				request.GovId);

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			return results;
		}
	}
}
