
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
    public class AccountRepository(UserManager<User>userManager) : IAccountRepository
    {
        public async Task<string> Register(User owner, string password, string role)
        {
            var res = await userManager.CreateAsync(owner, password);
            if (res.Succeeded)
            {
                await userManager.AddToRoleAsync(owner, role);
                return owner.Id;
            }
            return "Error when adding owner";
        }
    }
}
