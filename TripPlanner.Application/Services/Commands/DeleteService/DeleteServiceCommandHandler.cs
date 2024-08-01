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

namespace TripPlanner.Application.Services.Commands.DeleteService
{
	public class DeleteServiceCommandHandler(ILogger<DeleteServiceCommandHandler> logger, 
		IServiceRepository serviceRepository,
		IGovernoratesRepository governoratesRepository) : IRequestHandler<DeleteServiceCommand>
	{
		public async Task Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Deleting Service with id: {request.ServiceId} from governorate id: {request.GovernorateId}");
			var governorate = await governoratesRepository.GetById(request.GovernorateId);
			if (governorate == null)
				throw new NotFoundException(nameof(Governorate), request.GovernorateId.ToString());

			var service = governorate.Services.FirstOrDefault(g => g.Id == request.ServiceId);
			if (service == null)
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			
			await serviceRepository.FullyDeleteService(service.Id);
		}
	}
}
