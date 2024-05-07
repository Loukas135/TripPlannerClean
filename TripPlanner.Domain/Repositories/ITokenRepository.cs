using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Repositories
{
	public interface ITokenRepository
	{
		public string GenerateToken(string UserIdentifier);
	}
}
