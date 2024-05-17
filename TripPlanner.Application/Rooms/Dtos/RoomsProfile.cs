using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Rooms.Commands.CreateRoom;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;

namespace TripPlanner.Application.Rooms.Dtos
{
	public class RoomsProfile : Profile
	{
		public RoomsProfile()
		{
			CreateMap<Room, RoomDto>().ForMember(d => d.Reservations, opt => opt.MapFrom(src => src.Reservations));

			CreateMap<CreateRoomCommand, Room>();
		}
	}
}
