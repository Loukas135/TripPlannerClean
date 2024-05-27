using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class RoomCategoriesRespository : SeededValuesRepository<RoomCategory>, IRoomCategoriesRespository
	{
		public RoomCategoriesRespository(TripPlannerDbContext dbContext) : base(dbContext)
		{

		}
	}
}
