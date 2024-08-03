using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Queries.GetNewUsers
{
    public class GetNewUsersQueryHandler (IAccountRepository accountRepository, 
        IRolesRepository rolesRepository)
        : IRequestHandler<GetNewUsersQuery, IEnumerable<NumberOfUsersDto>>
    {
        public async Task<IEnumerable<NumberOfUsersDto>> Handle(GetNewUsersQuery request, CancellationToken cancellationToken)
        {
            var roles = await rolesRepository.GetAllAsync();

            List<NumberOfUsersDto> users = [];

            foreach (var role in roles)
            {
                users.Add(new NumberOfUsersDto
                {
                    TypeName = role.Name,
                    NumberOfType = await accountRepository.NewUsersAfterMonth(request.Month, role.Id,request.Year)
                });

            }

            return users;
        }
    }
}
