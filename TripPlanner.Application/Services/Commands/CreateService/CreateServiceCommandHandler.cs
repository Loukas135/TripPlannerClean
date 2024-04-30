using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Commands.CreateService
{
	public class CreateServiceCommandHandler(IServiceRepository serviceRepository, 
		ILogger<CreateServiceCommandHandler> logger,
		IMapper mapper) : IRequestHandler<CreateServiceCommand, int>
	{
		public async Task<int> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Creating Service");
			var service = mapper.Map<Service>(request);
			int id = await serviceRepository.Add(service);
			return id;
		}
	}
}
