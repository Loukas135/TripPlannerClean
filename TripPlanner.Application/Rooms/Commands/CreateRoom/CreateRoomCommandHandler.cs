using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Rooms.Commands.CreateRoom
{
	public class CreateRoomCommandHandler(ILogger<CreateRoomCommandHandler> logger,
		IServiceRepository serviceRepository,
		IRoomRepository roomRepository,
		IMapper mapper) : IRequestHandler<CreateRoomCommand, int>
	{
		public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Creating new Room in service with id:{ServiceId} from Room category: {RoomCategoryId}",
				request.ServiceId, request.RoomCategoryId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if(service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var room = mapper.Map<Room>(request);
			return await roomRepository.Add(room);
		}
	}
}
