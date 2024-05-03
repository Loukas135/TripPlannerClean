using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Domain.Repositories
{
	public interface IGovernoratesRepository
	{
		public Task<Governorate?> GetById(int id);
	}
}
