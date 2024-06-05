using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;
using TripPlanner.Domain.Exceptions;

namespace TripPlanner.Application.Reservations.Queries.GetRoomReservations
{
	public class GetRoomReservationQueryHandler(IServiceRepository serviceRepository,
		IMapper mapper) : IRequestHandler<GetRoomReservationQuery, IEnumerable<RoomReservationDto>>
	{
		public async Task<IEnumerable<RoomReservationDto>> Handle(GetRoomReservationQuery request, CancellationToken cancellationToken)
		{
			var service = await serviceRepository.GetServiceWithReservations(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}
			var reservations = service.Reservations?.ToList();

			var results = mapper.Map<IEnumerable<RoomReservationDto>>(reservations);
			return results;
		}
	}
}
