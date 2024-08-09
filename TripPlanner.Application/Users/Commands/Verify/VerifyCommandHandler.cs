using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.Verify
{
	public class VerifyCommandHandler(IAccountRepository accountRepository) :
		IRequestHandler<VerifyCommand, bool>
	{
		public Task<bool> Handle(VerifyCommand request, CancellationToken cancellationToken)
		{
			return accountRepository.Verify(request.VerificationToken);
		}
	}
	
}
