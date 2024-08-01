using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Exceptions
{
    public class ServiceTypeException(string ServiceType):Exception($" Your Service is not of Type {ServiceType}")
    {
    }
}
