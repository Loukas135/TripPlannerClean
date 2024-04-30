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

namespace TripPlanner.Application.Services.Queries.GetAllServices
{
	public class GetAllServicesQueryHandler(ILogger<GetAllServicesQueryHandler> logger,
		IServiceRepository serviceRepository,
		IMapper mapper) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDto>?>
	{
		public async Task<IEnumerable<ServiceDto>?> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Getting all the services");
			var services = await serviceRepository.Get();
			
			var serviceDtos = mapper.Map<IEnumerable<ServiceDto>>(services);
			return serviceDtos!;
		}
	}
}
