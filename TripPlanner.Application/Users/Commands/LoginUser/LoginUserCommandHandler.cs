using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.AuthEntity;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.LoginUser
{
	public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
		ITokenRepository tokenRepository,
		UserManager<User> userManager) : IRequestHandler<LoginUserCommand, AuthResponse>
	{
		public async Task<AuthResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
		{
			logger.LogInformation("looking for user with email: {Email}", request.Email);

			var user = await userManager.FindByEmailAsync(request.Email);
			if (user == null)
			{
				throw new NotFoundException(nameof(User), request.Email);
			}
			bool isValidCredentials = await userManager.CheckPasswordAsync(user, request.Password);

			if (isValidCredentials)
			{
				var token = await tokenRepository.GenerateToken(user.Id);
				return new AuthResponse
				{
					Username = request.Email,
					Token = token.ToString(),
                    RefreshToken = await tokenRepository.CreateRefreshToken()
				};
			}
			return null;
		}
	}
}
