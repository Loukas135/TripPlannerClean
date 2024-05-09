using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.AuthEntity;

namespace TripPlanner.Domain.Repositories
{
	public interface ITokenRepository
	{
		public Task<string> GenerateToken(string UserIdentifier);
		public Task<string> CreateRefreshToken();
		public Task<AuthResponse> VerifyRefreshToken(AuthResponse request);


	}
}
