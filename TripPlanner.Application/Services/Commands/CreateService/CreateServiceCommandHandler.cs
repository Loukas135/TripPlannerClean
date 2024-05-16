using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Commands.CreateService
{
	public class CreateServiceCommandHandler(IServiceRepository serviceRepository, 
		ILogger<CreateServiceCommandHandler> logger,
		IGovernoratesRepository governorateRepository,
		IMapper mapper) : IRequestHandler<CreateServiceCommand, int>
	{
		public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Creating Service");
			var governorate = await governorateRepository.GetById(request.GovernorateId);
			if (governorate == null)
				throw new NotFoundException(nameof(Governorate), request.GovernorateId.ToString());

			var service = mapper.Map<Service>(request);
			service.OwnerId = request.OwnerId;
			//service.Rate = request.Rate;
			return await serviceRepository.Add(service);
		}
	}
}
