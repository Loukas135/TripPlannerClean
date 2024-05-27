using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;

namespace TripPlanner.Application.Rooms.RoomCatergory.Dtos
{
	public class RoomCategoryProfile : Profile
	{
		public RoomCategoryProfile()
		{
			CreateMap<RoomCategory, RoomCategoryDto>();
		}
	}
}
