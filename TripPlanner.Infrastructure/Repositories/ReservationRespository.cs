using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class ReservationRespository(TripPlannerDbContext dbContext) : IReservationRespository
	{
		public async Task<int> Add(Reservation entity)
		{
			dbContext.Reservations.Add(entity);
			await dbContext.SaveChangesAsync();
			return entity.Id;
		}

		public async Task<IEnumerable<Reservation>> GetAll()
		{
			var reservations = await dbContext.Reservations.ToListAsync();
			return reservations;
		}
		
		public async Task<IEnumerable<Reservation>> GetAllByGov(int id)
		{
			var reservations = from r in dbContext.Reservations
							   join s in dbContext.Services
									on r.ServiceId equals s.Id
							   where s.GovernorateId == id
							   select r;

			return await reservations.ToListAsync();
		}

		public async Task<IEnumerable<Reservation>> GetAllInCurrentMonth(int id)
		{
			var reservations = from r in dbContext.Reservations
							   join s in dbContext.Services
									on r.ServiceId equals s.Id
							   where s.GovernorateId == id
							   where r.From.Month == DateTime.Now.Month
							   select r;

			return await reservations.ToListAsync();
		}

		
		public async Task<IEnumerable<Reservation>> GetServiceTypeReservations(int govId, int stId)
		{
			var reservations = from r in dbContext.Reservations
							   join s in dbContext.Services
									on r.ServiceId equals s.Id
							   where s.GovernorateId == govId
							   where s.ServiceTypeId == stId
							   select r;

			return await reservations.ToListAsync();
		}
		public async Task<IEnumerable<Reservation>> GetReservationsByDate(int year, int month, int govId)
		{
			var reservations = from r in dbContext.Reservations
							   join s in dbContext.Services
									on r.ServiceId equals s.Id
							   where s.GovernorateId == govId
							   where (int)r.From.Year == year
							   where (int)r.From.Month == month
							   select r;

			return await reservations.ToListAsync();
		}
		public async Task UpdateReservation(Reservation reservation)
		{
			dbContext.Reservations.Update(reservation);
			await dbContext.SaveChangesAsync();
		}
		public async Task<Reservation>GetById(int id)
		{
			var reservation = await dbContext.Reservations.FirstOrDefaultAsync(r => r.Id == id);
			return reservation;
		}
	}
}
