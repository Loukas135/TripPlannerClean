using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Services.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Queries.GetServiceById
{
	public class GetServiceByIdQueryHandler(IServiceRepository serviceRepository,
		ILogger<GetServiceByIdQueryHandler> logger,
		IMapper mapper) : IRequestHandler<GetServiceByIdQuery, ServiceDto?>
	{
		public async Task<ServiceDto?> Handle(GetServiceByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Getting the service with id: {request.Id}");
			var service = await serviceRepository.GetById(request.Id);

			var serviceDto = mapper.Map<ServiceDto?>(service);
			return serviceDto;
		}
	}
}
