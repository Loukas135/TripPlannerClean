using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Seeders.ReservationsStatus
{
	public class StatusSeeder(TripPlannerDbContext dbContext) : IStatusSeeder
	{
		public async Task Seed()
		{
			if (await dbContext.Database.CanConnectAsync())
			{
				if (!dbContext.Statuses.Any())
				{
					var statuses = GetRoles();
					dbContext.Statuses.AddRange(statuses);
					await dbContext.SaveChangesAsync();
				}
			}
		}
		public IEnumerable<Status> GetRoles()
		{
			List<Status> statuses = [
				new ()
				{
					Name = "Pending"
					
				},
				new()
				{
					Name = "Accepted"
				},
				new()
				{
					Name = "Rejected"
				}
				];
			return statuses;
		}
	}
}
