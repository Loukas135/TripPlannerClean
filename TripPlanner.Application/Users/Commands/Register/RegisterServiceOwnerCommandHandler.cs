using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.Register
{
    public class RegisterServiceOwnerCommandHandler(IAccountRepository accountRepository,
        IMapper mapper)
        : IRequestHandler<RegisterServiceOwnerCommand, string>
    {
        public async Task<string> Handle(RegisterServiceOwnerCommand request, CancellationToken cancellationToken)
        {
           /*
            User user = mapper.Map<User>(request);
            user.UserName = request.FirstName;
            var password = request.Password;
            var record = await accountRepository.Register(user, password);
            return record;
            */
            var owner = mapper.Map<User>(request);
            owner.UserName = request.Email;
            var ownerId = await accountRepository.Register(owner, request.Password, request.Role!);
            return ownerId;
        }
    }
}
