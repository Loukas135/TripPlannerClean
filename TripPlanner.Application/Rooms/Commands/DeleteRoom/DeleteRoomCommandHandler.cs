﻿using MediatR;
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

namespace TripPlanner.Application.Rooms.Commands.DeleteRoom
{
	public class DeleteRoomCommandHandler(ILogger<DeleteRoomCommandHandler> logger,
		IRoomRepository roomRepository,
		IServiceRepository serviceRepository) : IRequestHandler<DeleteRoomCommand>
	{
		public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting a Room with id:{RoomId} from service with id: {ServiceId}",
				request.RoomId, request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var room = service.Rooms?.FirstOrDefault(r => r.Id == request.RoomId);
			if (room == null)
			{
				throw new NotFoundException(nameof(Room), request.RoomId.ToString());
			}

			await roomRepository.Delete(room);
		}
	}
}
