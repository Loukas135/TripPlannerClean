using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Exceptions
{
	public class NoResourceAvailable(string resourceType) : Exception($"No {resourceType} available")
	{

	}
}
