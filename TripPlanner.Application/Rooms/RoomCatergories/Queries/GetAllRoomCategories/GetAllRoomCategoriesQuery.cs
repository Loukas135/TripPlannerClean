using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Rooms.RoomCatergory.Dtos;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;

namespace TripPlanner.Application.Rooms.RoomCatergories.Queries.GetAllRoomCategories
{
	public class GetAllRoomCategoriesQuery : IRequest<IEnumerable<RoomCategoryDto>>
	{

	}
}
