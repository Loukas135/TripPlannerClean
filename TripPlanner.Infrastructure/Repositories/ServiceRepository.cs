using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Infrastructure.Repositories
{
	public class ServiceRepository : IServiceRepository
	{
		public Task<int> Add()
		{
			throw new NotImplementedException();
		}

		public Task<bool> Delete(int id)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<Service>> Get()
		{
			throw new NotImplementedException();
		}

		public Task<Service> GetById(int id)
		{
			throw new NotImplementedException();
		}

		public Task<bool> SaveChanges()
		{
			throw new NotImplementedException();
		}
	}
}
