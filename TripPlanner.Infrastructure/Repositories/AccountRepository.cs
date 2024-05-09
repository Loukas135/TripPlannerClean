
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Infrastructure.Repositories
{
    public class AccountRepository(UserManager<User>userManager):IAccountRepository
    {
        public async Task<IEnumerable<IdentityError>> Register(User user,string password)
        {
            var res = await userManager.CreateAsync(user, password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(user, "User");
            }
            return res.Errors;
        }
    }
}
