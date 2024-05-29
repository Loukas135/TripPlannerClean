using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Commands.RegisterUser;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.RegisterAdmin
{
    public class RegisterAdminCommandHandler(IMapper mapper,
        IAccountRepository accountRepository) : IRequestHandler<RegisterAdminCommand, IEnumerable<IdentityError>>
    {
        public async Task<IEnumerable<IdentityError>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
        {
            var admin = mapper.Map<User>(request);
            admin.UserName = request.UserName;
            var errors = await accountRepository.RegisterAdmin(admin, request.Password);
            return errors ;
        }
    }
}
