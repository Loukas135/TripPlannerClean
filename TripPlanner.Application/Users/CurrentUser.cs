using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users
{
	public record CurrentUser(string Id, string Email, IEnumerable<string> Roles)
	{
		public bool isInRole(string role) => Roles.Contains(role);
	}
}
