
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.Users.Dtos;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Repositories
{
	public class AccountRepository(UserManager<User> userManager, TripPlannerDbContext dbcontext, IHostEnvironment hostEnvironment) : IAccountRepository
	{
		public async Task<bool> FillWallet(string email, int amount)
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return false;
			}
			user.Wallet += amount;
			await userManager.UpdateAsync(user);
			await dbcontext.SaveChangesAsync();
			return true;
		}

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

		public async Task<IEnumerable<IdentityError>> RegisterAdmin(User user, string password)
		{
			var check = await userManager.CreateAsync(user, password);
			if (check.Succeeded)
			{
				await userManager.AddToRoleAsync(user, "Administrator");
			}
			return check.Errors;
		}

		public async Task<IEnumerable<IdentityError>> RegisterUser(User user, string password)
		{
			await userManager.GenerateEmailConfirmationTokenAsync(user);
			var check = await userManager.CreateAsync(user, password);

			if (check.Succeeded)
			{
				await userManager.AddToRoleAsync(user, "User");
				user.Wallet = 0;
				await dbcontext.SaveChangesAsync();
			}
			return check.Errors;
		}

		public async Task<string> SaveUserProfileAsync(IFormFile userImage)
		{
			if (userImage == null)
				return null;

			var contentPath = hostEnvironment.ContentRootPath;
			var path = Path.Combine(contentPath, "Images/Users");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			var extension = Path.GetExtension(userImage.FileName);
			var imageName = $"{Guid.NewGuid().ToString()}{extension}";
			var fullName = Path.Combine(path, imageName);
			using var stream = new FileStream(fullName, FileMode.Create);
			await stream.CopyToAsync(stream);
			return fullName;
		}

		public async Task<bool> Verify(string email, string verficationToken)
		{
			var user = await userManager.FindByEmailAsync(email);
			if (user.VerificationToken == verficationToken)
			{
				user.VerifiedAt = DateTime.Now;
				user.EmailConfirmed = true;
				await dbcontext.SaveChangesAsync();
				return true;
			}
			return false;
		}

		public async Task<int> NumberOfUsersInRole(string roleId)
		{
			var num = await dbcontext.UserRoles.Where(ur => ur.RoleId == roleId).CountAsync();
			return num;
		}

		public async Task<int> NewUsersAfterMonth(int month, string roleId,int year)
		{
			var records = from ur in dbcontext.UserRoles
						  join u in dbcontext.Users
						  on ur.UserId equals u.Id
						  where u.CreatedAt.Month == month
						  where u.CreatedAt.Year == year
					where ur.RoleId == roleId
					  select ur;

			var num = await records.CountAsync();
			return num;
		}
        /*
        public async Task<IEnumerable<IdentityError>> Verify(string email, string verficationToken)
        {
            var user = await userManager.FindByEmailAsync(email);
            var check = await userManager.ConfirmEmailAsync(user, verficationToken);
            if (check.Succeeded)
            {
                user.VerifiedAt = DateTime.Now;
                user.EmailConfirmed = true;
                await dbcontext.SaveChangesAsync();
            }
            return check.Errors;
        }
        */

    }
}
