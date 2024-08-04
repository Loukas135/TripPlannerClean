using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.UpdateImage
{
    internal class UpdateUserImageCommandHandler(IUserContext userContext
        , IAccountRepository accountRepository) : IRequestHandler<UpdateUserImageCommand>
    {
        public async Task Handle(UpdateUserImageCommand request, CancellationToken cancellationToken)
        {
            var user = userContext.GetCurrentUser();
            var success = await accountRepository.UpdateUserImage(user.Id.ToString(), request.newImage);
            if (!success)
            {
                throw new Exception();
            }
        }
    }
}
