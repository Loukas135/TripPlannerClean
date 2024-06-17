using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.FillWallet
{
    public class FillWalletCommandHandler(IAccountRepository accountRepository) : IRequestHandler<FillWalletCommand, bool>
    {
        public Task<bool> Handle(FillWalletCommand request, CancellationToken cancellationToken)
        {
            var success = accountRepository.FillWallet(request.EmailAddress, request.Amount);
            return success;
        }
    }
}
