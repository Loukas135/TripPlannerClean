using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Domain.Repositories
{
	public interface IServiceRepository
	{
		public Task<int> Add();
		public Task<IEnumerable<Service>> Get();
		public Task<Service> GetById(int id);
		public Task<bool> Delete(int id);
		public Task<bool> SaveChanges();
	}
}
