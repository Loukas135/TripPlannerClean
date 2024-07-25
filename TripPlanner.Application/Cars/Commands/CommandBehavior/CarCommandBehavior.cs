using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Cars.Commands.CreateCar;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Cars.Commands.CommandBehavior
{
	public class CarCommandBehavior<TRequest, TResponse>(IServiceRepository serviceRepository, 
		IUserContext userContext) :
		IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			int serviceId = 0;

			if (request is CreateCarCommand createCommand) serviceId = createCommand.ServiceId;

			else if (request is CreateCarCommand deleteCommand) serviceId = deleteCommand.ServiceId;

			if (serviceId != 0)
			{
				var user = userContext.GetCurrentUser();
				var service = await serviceRepository.GetById(serviceId);

				if (service.OwnerId == user.Id)
				{
					return await next();
				}

				throw new OwnershipException("You are not allowed to edit in this service");
			}
			return await next();
		}
	}
}
