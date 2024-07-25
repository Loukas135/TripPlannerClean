using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;

namespace TripPlanner.Application.Rooms.Commands.UpdateRoom
{
	public class UpdateRoomCommandHandler(ILogger<UpdateRoomCommandHandler> logger, IMapper mapper,
		IRoomRepository roomRepository) : IRequestHandler<UpdateRoomCommand>
	{
		public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
		{

			var room = await roomRepository.GetById(request.RoomId);
			if(room == null)
			{
				throw new NotFoundException(nameof(Room), request.RoomId.ToString());
			}

			mapper.Map(request, room);
			await roomRepository.SaveChanges();
		}
	}
}
