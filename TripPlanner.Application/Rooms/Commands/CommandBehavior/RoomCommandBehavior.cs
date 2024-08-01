using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Rooms.Commands.CreateRoom;
using TripPlanner.Application.Rooms.Commands.DeleteRoom;
using TripPlanner.Application.Rooms.Commands.UpdateRoom;
using TripPlanner.Application.Users;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Rooms.Commands.CommandBehavior
{
	public class RoomCommandBehavior<TRequest, TResponse>(IUserContext userContext,
		IServiceRepository serviceRepository) :
		IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
	{
		public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			int serviceId = 0;

			if(request is CreateRoomCommand createCommand) serviceId = createCommand.ServiceId;

			else if(request is DeleteRoomCommand deleteCommand) serviceId = deleteCommand.ServiceId;

			if(serviceId != 0)
			{
				var user = userContext.GetCurrentUser();
				var service = await serviceRepository.GetById(serviceId);
                if (service.ServiceTypeId != 1)
                {
					throw new ServiceTypeException("Hotel");
                }

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
