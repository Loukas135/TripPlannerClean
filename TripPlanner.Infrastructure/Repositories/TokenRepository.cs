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
using TripPlanner.Application.Users;
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

            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]));
            var credintials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
            var roles = await userManager.GetRolesAsync(_user);
            var role_claims = roles.Select(x => new Claim(ClaimTypes.Role, x));
            var userClaims = await userManager.GetClaimsAsync(_user);
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub,_user.Id),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email,_user.Email),

            }.Union(userClaims).Union(role_claims);
            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credintials
                );
            var finalToken= new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthResponse
            {
                Token = finalToken.ToString(),
                RefreshToken = await CreateRefreshToken(),
                Expires = _expiresInMinutes,
                Username = _user!.UserName,
                Role = roles.FirstOrDefault()
            };
		}

        public async Task<string> CreateRefreshToken()
        {
            await userManager.RemoveAuthenticationTokenAsync(_user!, _loginProvidor, _refreshToken);
            var NewToken = await userManager.GenerateUserTokenAsync(_user!, _loginProvidor, _refreshToken);
            var res = await userManager.SetAuthenticationTokenAsync(_user!, _loginProvidor, _refreshToken, NewToken);
            return NewToken;
        }

        public async Task<AuthResponse?> VerifyRefreshToken(RefreshTokenRequest request)
        {
             /*var jwtSecurityHandler = new JwtSecurityTokenHandler();
             var tokenContent = jwtSecurityHandler.ReadJwtToken(request.Token);

             var username = tokenContent.Claims.ToList().FirstOrDefault(q => q.Type == JwtRegisteredClaimNames.Sub)?.Value;*/
            _user = await userManager.FindByIdAsync(request.user_id);
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
