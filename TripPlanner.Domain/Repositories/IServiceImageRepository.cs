using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Domain.Repositories
{
    public interface IServiceImageRepository
    {
        public Task<int> addServiceImageAsync(ServiceImage serviceImage);
    }
}
