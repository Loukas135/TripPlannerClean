using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetServiceById
{
	public class GetServiceByIdQueryHandler(ILogger<GetServiceByIdQueryHandler> logger,
		IGovernoratesRepository governoratesRepository,
		IMapper mapper) : IRequestHandler<GetServiceByIdQuery, ServiceDto?>
	{
		public async Task<ServiceDto?> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Getting the service with id: {request.ServiceId} from governorate id: {request.GovernorateId}");
			var governorate = await governoratesRepository.GetById(request.GovernorateId);
			if (governorate == null)
				throw new NotFoundException(nameof(Governorate), request.GovernorateId.ToString());

			var service = governorate.Services.FirstOrDefault(s => s.Id == request.GovernorateId);
			if (service == null)
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());

			var result = mapper.Map<ServiceDto>(service);
			return result;
		}
	}
}
