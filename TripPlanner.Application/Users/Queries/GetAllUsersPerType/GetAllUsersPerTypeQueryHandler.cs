using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Queries.GetAllUsersPerType
{
    public class GetAllUsersPerTypeQueryHandler(IAccountRepository accountRepository, IRolesRepository rolesRepository) : IRequestHandler<GetAllUsersPerTypeQuery, IEnumerable<NumberOfUsersDto>>

    {
        public async Task<IEnumerable<NumberOfUsersDto>> Handle(GetAllUsersPerTypeQuery request, CancellationToken cancellationToken)
        {
            var roles = await rolesRepository.GetAllAsync();
            List<NumberOfUsersDto> users = [];
            foreach (var role in roles)
            {
                users.Add(new NumberOfUsersDto
                {
                    TypeName = role.Name,
                    NumberOfType = await accountRepository.NumberOfUsersInRole(role.Id)
                });

            }
            return users;
        }
    }
}
