using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class ReservationRespository(TripPlannerDbContext dbContext,
		UserManager<User>userManager
		) : IReservationRespository
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
		public async Task<IEnumerable<Reservation>>GetBySubServiceId(int subServiceId)
		{
			var reservations = await dbContext.Reservations.Where(r => (r.RoomId == subServiceId ||
			r.CarId == subServiceId ||
			r.TripId == subServiceId)).ToListAsync();
			return reservations;
		}
		public async Task DeleteReservation(Reservation entity)
		{
			dbContext.Reservations.Remove(entity);
			await dbContext.SaveChangesAsync();
		}

		public async Task<IEnumerable<Reservation>> GetUserReservations(string userId)
		{
			var reservations = await dbContext.Reservations.Where(r => r.UserId == userId).ToListAsync();
			return reservations;
		}

		public async Task<IEnumerable<Reservation>> GetByStatus(string status, string userId)
		{
			var reservations = await dbContext.Reservations.Where(r => r.Status == status)
				.Where(r => r.UserId == userId).ToListAsync();
			return reservations;
		}

		public async Task<IEnumerable<Status>> GetStatuses()
		{
			return await dbContext.Statuses.ToListAsync();
		}

		public async Task<IEnumerable<AllGovsEarnings>> GetAllGovsEarnings(int month, int year)
		{
			var reservation = from g in dbContext.Governorates
							  join s in dbContext.Services
								  on g.Id equals s.GovernorateId into serviceGroup
							  from sg in serviceGroup.DefaultIfEmpty()
							  join r in dbContext.Reservations
								  on sg.Id equals r.ServiceId into reservationGroup
							  from rg in reservationGroup.DefaultIfEmpty()
							  where rg == null || (rg.Status == "Accepted" && rg.From.Year == year && rg.From.Month == month)
							  group rg by g.Name into gGroup
							  select new AllGovsEarnings
							  {
								  Name = gGroup.Key,
								  TotalEarnings = gGroup.Sum(reservation => reservation == null ? 0 : reservation.Cost)
							  };
			return await reservation.ToListAsync();
		}
		public async Task<bool> DeleteReservationAsync(int id)
		{
			var reservation = await GetById(id);
			if (reservation == null)
			{
				return false;
			}
			var user = await userManager.FindByIdAsync(reservation.UserId);
			if (reservation.Status == "Paid")
			{
				user.Wallet += reservation.Cost;
			}
			await userManager.UpdateAsync(user);
			dbContext.Reservations.Remove(reservation);
			await dbContext.SaveChangesAsync();
			return true;
		}
	}
}
