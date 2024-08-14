using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Queries
{
    internal class GetCurrentUserQueryHandler(IUserContext userContext,
        IMapper mapper,
        IAccountRepository accountRepository) : IRequestHandler<GetCurrentUserQuery, FullCurrentUserDto>
    {
        
        public async Task<FullCurrentUserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var current_User = userContext.GetCurrentUser();
            var fullUser = await accountRepository.GetUserAsync(current_User.Id);
            var response = mapper.Map<FullCurrentUserDto>(fullUser);
            response.Role = current_User.Roles.First();
            return response;
        }
    
}
}
