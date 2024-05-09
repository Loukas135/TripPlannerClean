using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.AuthEntity;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Infrastructure.Repositories
{
	public class TokenRepository(IConfiguration configuration, UserManager<User>userManager) : ITokenRepository
	{
		private readonly string _loginProvidor = "TripPlannerTokenProvidor";
		private readonly string _refreshToken = "RefreshToken";
		private User? _user;
        private readonly int _expiresInMinutes = Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"]);



		public async Task<AuthResponse?> GenerateToken(string UserIdentifier)
		{
            _user = await userManager.FindByIdAsync(UserIdentifier);

            if (_user == null)
            {
                return null;
            }

			var roles = await userManager.GetRolesAsync(_user!);
			var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x));
			var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!);

			var tokenHandler = new JwtSecurityTokenHandler();

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity (new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, UserIdentifier),
                    //new Claim(JwtRegisteredClaimNames.Sub, _user.Id),
                    //new Claim(JwtRegisteredClaimNames.Email, _user.Email),
					new Claim(ClaimTypes.Name, _user!.UserName!)
				}.Union(roleClaims)),
				Issuer = configuration["JwtSettings:Issuer"],
				Audience = configuration["JwtSettings:Audience"],
				Expires = DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			var finalToken = tokenHandler.WriteToken(token);
            return new AuthResponse
            {
                Token = finalToken,
                RefreshToken = await CreateRefreshToken(),
                Expires = _expiresInMinutes,
                Username = _user!.UserName,
            };
		}

        public async Task<string> CreateRefreshToken()
        {
            await userManager.RemoveAuthenticationTokenAsync(_user!, _loginProvidor, _refreshToken);
            var NewToken = await userManager.GenerateUserTokenAsync(_user!, _loginProvidor, _refreshToken);
            var res = await userManager.SetAuthenticationTokenAsync(_user!, _loginProvidor, _refreshToken, NewToken);
            return NewToken;
        }

        public async Task<AuthResponse?> VerifyRefreshToken(AuthResponse request)
        {

            var jwtSecurityHandler = new JwtSecurityTokenHandler();
            var tokenContent = jwtSecurityHandler.ReadJwtToken(request.Token);

            var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Sub)?.Value;

            _user = await userManager.FindByIdAsync(username!);
            if (_user is null)
            {
                return null;
            }

            var isValidRefreshToken = await userManager.VerifyUserTokenAsync(_user, _loginProvidor, _refreshToken, request.RefreshToken);
            if (isValidRefreshToken)
            {
                var token = await GenerateToken(_user.Id.ToString());
                return new AuthResponse
                {
                    Token = token?.Token,
                    Username = _user.UserName,
                    RefreshToken = await CreateRefreshToken(),
                    Expires = _expiresInMinutes
				};
            }

            await userManager.UpdateSecurityStampAsync(_user);
            return null;
        }
        public async Task TokenDelete(User user)
        {
            _user = user;
            await userManager.RemoveAuthenticationTokenAsync(_user, _loginProvidor, _refreshToken);
            await userManager.UpdateSecurityStampAsync(_user);
        }
    }
}
