using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;

namespace TripPlanner.Domain.Repositories
{
	public interface IReservationRespository
	{
		public Task<int> Add(Reservation entity);
		public Task<IEnumerable<Reservation>> GetAll();
		public Task<IEnumerable<Reservation>> GetAllByGov(int id);
		public Task<IEnumerable<Reservation>> GetAllInCurrentMonth(int id);
		public Task<IEnumerable<Reservation>> GetServiceTypeReservations(int govId, int stId);
		
		public Task<IEnumerable<Reservation>> GetReservationsByDate(int year, int month, int govId/*, string year, string month*/);
		public Task UpdateReservation(Reservation reservation);
		public Task<Reservation> GetById(int id);
		public Task<IEnumerable<Reservation>> GetBySubServiceId(int subServiceId);
		public Task DeleteReservation(Reservation entity);
		public Task<IEnumerable<Reservation>> GetUserReservations(string userId);
		public Task<IEnumerable<Reservation>> GetByStatus(string status, string userId);
		public Task<IEnumerable<Status>> GetStatuses();
    }
}
