﻿using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Reservations.Dtos;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Reservations.Queries.GetCarReservations
{
	public class GetCarReservationsQueryHandler(IServiceRepository serviceRepository,
		IMapper mapper) : IRequestHandler<GetCarReservationsQuery, IEnumerable<CarReservationDto?>>
	{
		public async Task<IEnumerable<CarReservationDto?>> Handle(GetCarReservationsQuery request, CancellationToken cancellationToken)
		{
			var service = await serviceRepository.GetServiceWithReservations(request.ServiceId);
			if(service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var reservations = service.Reservations?.ToList();

			var results = mapper.Map<IEnumerable<CarReservationDto>>(reservations);
			return results;
		}
	}
}
