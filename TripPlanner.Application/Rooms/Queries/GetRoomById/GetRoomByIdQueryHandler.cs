using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Rooms.Dtos;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Rooms.Queries.GetRoomById
{
	public class GetRoomByIdQueryHandler(ILogger<GetRoomByIdQueryHandler> logger,
		IServiceRepository serviceRepository,
		IMapper mapper,
		IRoomRepository roomRepository) : IRequestHandler<GetRoomByIdQuery, RoomDto>
	{
		public async Task<RoomDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Deleting a Room with id:{RoomId} from service with id: {ServiceId}",
				request.RoomId, request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}

			var room = await roomRepository.GetById(request.RoomId);
			if (room == null)
			{
				throw new NotFoundException(nameof(Room), request.RoomId.ToString());
			}

			var result = mapper.Map<RoomDto>(room);
			return result;
		}
	}
}
