using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetGovReservations.GetAll
{
	
	public class GetAllReservationsQueryHandler(IGovernoratesRepository governoratesRepository,
		IServiceRepository serviceRepository,
		IReservationRespository reservationRespository,
		IMapper mapper) : IRequestHandler<GetAllReservationsQuery, IEnumerable<ReservationDto>>
	{
		public async Task<IEnumerable<ReservationDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
		{
			var reservations = await reservationRespository.GetAllByGov(request.GovernorateId);

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			return results;
		}
	}
	
}
