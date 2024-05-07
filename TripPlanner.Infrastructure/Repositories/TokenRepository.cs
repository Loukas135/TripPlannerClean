using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Infrastructure.Repositories
{
	public class TokenRepository(IConfiguration configuration) : ITokenRepository
	{
		public string GenerateToken(string UserIdentifier)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity (new[] {new Claim(ClaimTypes.NameIdentifier, UserIdentifier)}),
				Issuer = configuration["JwtSettings:Issuer"],
				Audience = configuration["JwtSettings:Audience"],
				Expires = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

	}
}
