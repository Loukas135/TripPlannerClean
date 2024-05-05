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

namespace TripPlanner.Application.Trips.Queries.GetTripById
{
	public class GetTripByIdQueryHandler(ILogger<GetTripByIdQueryHandler> logger,
		IServiceRepository serviceRepository,
		IMapper mapper) : IRequestHandler<GetTripByIdQuery, TripDto>
	{
		public async Task<TripDto> Handle(GetTripByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting trip with id: {TripId} from service with id: {ServiceId}",
				request.TripId, request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var trip = service.Trips?.FirstOrDefault(c => c.Id == request.TripId);

			var result = mapper.Map<TripDto>(trip);
			return result;
		}
	}
}
