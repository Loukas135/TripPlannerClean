using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommandHandler(IMapper mapper,
		IAccountRepository accountRepository) : IRequestHandler<RegisterUserCommand, IEnumerable<IdentityError>>
	{
		public async Task<IEnumerable<IdentityError>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var user = mapper.Map<User>(request);
			user.UserName = request.UserName;
			return await accountRepository.RegisterUser(user, request.Password);
		}
	}
}
