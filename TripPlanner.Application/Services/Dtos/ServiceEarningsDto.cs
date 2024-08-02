using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Services.Dtos
{
    public class ServiceEarningsDto
    {
        public int ServiceId { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Earnings { get; set; }
    }
}
