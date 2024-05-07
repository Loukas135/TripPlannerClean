using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.LoginUser
{
	public class LoginUserCommandHandler(ILogger<LoginUserCommandHandler> logger,
		ITokenRepository tokenRepository,
		UserManager<User> userManager) : IRequestHandler<LoginUserCommand, string>
	{
		public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
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
				var token = tokenRepository.GenerateToken(user.Id);
				return token;
			}

			return "Invalid Credentials";
		}
	}
}
