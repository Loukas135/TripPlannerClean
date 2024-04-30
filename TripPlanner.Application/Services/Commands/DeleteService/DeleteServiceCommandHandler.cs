using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Services.Commands.DeleteService
{
	public class DeleteServiceCommandHandler(ILogger<DeleteServiceCommandHandler> logger, 
		IServiceRepository serviceRepository) : IRequestHandler<DeleteServiceCommand, bool>
	{
		public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation($"Deleting Service with id: {request.Id}");
			var service = await serviceRepository.GetById(request.Id);
			if (service == null)
			{
				return false;
			}
			await serviceRepository.Delete(service);
			return true;
		}
	}
}
