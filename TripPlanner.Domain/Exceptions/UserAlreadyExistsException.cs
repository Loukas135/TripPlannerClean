using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Exceptions
{
	public class UserAlreadyExistsException(string existsEmail) : Exception($"User with email{existsEmail} already exists")
	{
	}
}
