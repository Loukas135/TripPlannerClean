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

namespace TripPlanner.Application.Services.Queries.GetAllServices
{
	public class GetAllServicesQueryHandler(ILogger<GetAllServicesQueryHandler> logger,
		IGovernoratesRepository governoratesRepository,
		IMapper mapper) : IRequestHandler<GetAllServicesQuery, IEnumerable<ServiceDto>?>
	{
		public async Task<IEnumerable<ServiceDto>?> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Getting all the services from governorate id: {request.GovernorateId}");

			var governorate = await governoratesRepository.GetById(request.GovernorateId);
			if( governorate == null )
				throw new NotFoundException(nameof(Governorate), request.GovernorateId.ToString());

			var services = mapper.Map<IEnumerable<ServiceDto>>(governorate.Services);
			return services;
		}
	}
}
