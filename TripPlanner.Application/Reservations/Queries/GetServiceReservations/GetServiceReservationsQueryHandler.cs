using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetServiceReservations
{
	public class GetServiceReservationsQueryHandler(IServiceRepository serviceRepository,
		IMapper mapper) : IRequestHandler<GetServiceReservationsQuery, IEnumerable<ReservationDto?>>
	{
		public async Task<IEnumerable<ReservationDto?>> Handle(GetServiceReservationsQuery request, CancellationToken cancellationToken)
		{
			var service = await serviceRepository.GetServiceWithReservations(request.ServiceId);

			var reservations = service!.Reservations?.ToList();

			var results = mapper.Map<IEnumerable<ReservationDto>>(reservations);
			return results;
		}
	}
}
