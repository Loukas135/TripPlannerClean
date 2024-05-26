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
	public class GovernoratesRepository : SeededValuesRepository<Governorate>, IGovernoratesRepository
	{
        private readonly TripPlannerDbContext dbContext;

        public GovernoratesRepository(TripPlannerDbContext dbContext):base(dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Governorate?> GetById(int id)
		{
			var governorate = await dbContext.Governorates
				.Include(g => g.Services)
				.FirstOrDefaultAsync(g => g.Id == id);
			return governorate;
		}
	}
}
