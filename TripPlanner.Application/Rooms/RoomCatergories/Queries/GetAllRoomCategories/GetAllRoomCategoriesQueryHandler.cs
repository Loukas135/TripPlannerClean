using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.CarTypes.Dtos;
using TripPlanner.Application.Rooms.RoomCatergory.Dtos;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Rooms.RoomCatergories.Queries.GetAllRoomCategories
{
	public class GetAllRoomCategoriesQueryHandler(IRoomCategoriesRespository roomCategoriesRespository,
		IMapper mapper) : IRequestHandler<GetAllRoomCategoriesQuery, IEnumerable<RoomCategoryDto>>
	{
		public async Task<IEnumerable<RoomCategoryDto>> Handle(GetAllRoomCategoriesQuery request, CancellationToken cancellationToken)
		{
			var rooms = await roomCategoriesRespository.GetAllAsync();
			var allTypes = mapper.Map<IEnumerable<RoomCategoryDto>>(rooms);
			return allTypes;
		}
	}
}
