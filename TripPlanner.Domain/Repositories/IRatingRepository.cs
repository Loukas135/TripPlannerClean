using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Domain.Repositories
{
    public interface IRatingRepository
    {
        public Task<int> AddRating(Ratings entity);
        public Task UpdateRating(Ratings entity);
        public Task DeleteRating(Ratings entity);
        public Task<IEnumerable<Ratings>>GetRatingsOfUser(string userId);
    }
}
