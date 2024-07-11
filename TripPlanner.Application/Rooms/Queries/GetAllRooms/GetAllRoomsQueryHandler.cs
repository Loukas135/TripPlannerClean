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
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Rooms.Queries.GetAllRooms
{
	public class GetAllRoomsQueryHandler(ILogger<GetAllRoomsQueryHandler> logger,
		IMapper mapper,
		IServiceRepository serviceRepository,
		IRoomCategoriesRespository roomCategoriesRespository) : IRequestHandler<GetAllRoomsQuery, IEnumerable<RoomDto>>
	{
		public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
		{
			logger.LogInformation("Getting all Rooms from service with id: {ServiceId}",
				request.ServiceId);

			var service = await serviceRepository.GetById(request.ServiceId);
			if (service == null)
			{
				throw new NotFoundException(nameof(Service), request.ServiceId.ToString());
			}
			var rooms = mapper.Map<IEnumerable<RoomDto>>(service.Rooms);
			return rooms;
		}
	}
}
