using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Dtos;
using TripPlanner.Application.Trips.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Trips.Queries.GetAllTrips
{
	public class GetAllTripsQueryHandler(ILogger<GetAllTripsQueryHandler> logger,
		IServiceRepository serviceRepository,
		IMapper mapper) : IRequestHandler<GetAllTripsQuery, IEnumerable<TripDto>>
	{
		public async Task<IEnumerable<TripDto>> Handle(GetAllTripsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all cars from service with id: {ServiceId}", request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var trips = mapper.Map<IEnumerable<TripDto>>(service.Trips);
			return trips;
		}
	}
}
