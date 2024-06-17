using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetGovReservations.GetInServiceType
{
	public class GetReservationsInServiceTypeQueryHandler(IReservationRespository reservationRespository,
		IMapper mapper) : IRequestHandler<GetReservationsInServiceTypeQuery, IEnumerable<ReservationDto>>
	{
		public async Task<IEnumerable<ReservationDto>> Handle(GetReservationsInServiceTypeQuery request, CancellationToken cancellationToken)
		{
			var reservations = await reservationRespository.GetServiceTypeReservations(request.GovernorateId,
				request.ServiceTypeId);

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			return results;
		}
	}
}
