using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Domain.Repositories
{
	public interface IServiceRepository
	{
		public Task<int> Add(Service entity);
		public Task<IEnumerable<Service>> Get();
		public Task<Service?> GetById(int id);
		public Task Delete(Service entity);
		public Task SaveChanges();
	}
}
