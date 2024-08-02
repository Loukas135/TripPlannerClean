using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Domain.Repositories
{
	public interface IServiceRepository
	{
		public Task<int> Add(Service entity);
		public Task<IEnumerable<Service>> Get();
		public Task<Service?> GetById(int id);
		public Task<Service?> GetByUserId(string ownerId);
		public Task<Service?> GetByIdWithRating(int id);
		public Task<float?> CalculateOverallRating(int id);
		public Task<IEnumerable<Service>> GetServicesOfType(int governorateId,int serviceTypeId);
		public Task<IEnumerable<Reservation>> PaidReservations(int id,int year,int month);
		public Task<int> PaidReservationsSum(int id, int year, int month);
        public Task Delete(Service entity);
		public Task FullyDeleteService(int id);

        public Task SaveChanges();

		public Task<Service> GetServiceWithReservations(int id);
		public Task<IEnumerable<Service>> GetServicesWithReservationsByGov(int id);
		public Task<IEnumerable<Service>> GetServiceFromUserReservation(string userid);

    }
}
