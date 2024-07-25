using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Trips.Commands.CreateTrip;
using TripPlanner.Application.Trips.Commands.DeleteTrip;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Trips.Commands.CommandBehavior
{
	public class TripCommandBehavior<TRequest, TResponse> (IUserContext userContext,
		IServiceRepository serviceRepository) :
		IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			int serviceId = 0;

			if (request is CreateTripCommand createCommand) serviceId = createCommand.ServiceId;

			else if (request is DeleteTripCommand deleteCommand) serviceId = deleteCommand.ServiceId;

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
