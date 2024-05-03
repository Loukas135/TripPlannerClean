using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;

namespace TripPlanner.Domain.Repositories
{
	public interface ICarRepository
	{
		public Task<int> Add(Car entity);
		public Task Delete(Car entity);
	}
}
