using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.DeleteAccount
{
    internal class DeleteAccountCommandHandler(IAccountRepository accountRepository,IUserContext userContext) : IRequestHandler<DeleteAccountCommand>
    {
        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var userId = userContext.GetCurrentUser().Id.ToString();
            var sucess = await accountRepository.DeleteAccount(userId, request.Password);
            if (!sucess)
            {
                throw new Exception();
            }
            return;
        }
    }
}
