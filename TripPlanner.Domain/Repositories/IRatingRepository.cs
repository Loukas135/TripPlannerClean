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
        public Task<int> AddRating(Rate entity);
        public Task UpdateRating(Rate entity);
        public Task DeleteRating(Rate entity);
        public Task<IEnumerable<Rate>>GetRatingsOfUser(string userId);
    }
}
