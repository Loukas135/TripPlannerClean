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

namespace TripPlanner.Application.Users.Commands.Register
{
    public class RegisterCommandHandler(IAccountRepository accountRepository,IMapper mapper) : IRequestHandler<RegisterCommand, IEnumerable <IdentityError>>
    {
        public async Task<IEnumerable <IdentityError>>Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            User user = mapper.Map<User>(request);
            user.UserName = request.FirstName;
            var password = request.Password;
            var record = await accountRepository.Register(user, password);
            return record;
        }
    }
}
