
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MimeKit.Text;
using MimeKit;
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
using MailKit.Net.Smtp;
using TripPlanner.Domain.Exceptions;
using Azure.Core;

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

		public async Task<IEnumerable<IdentityError>> RegisterUser(User user, string password,string verifyUrl)
		{
			if(await userManager.FindByEmailAsync(user.Email) != null)
			{
				throw new UserAlreadyExistsException(user.Email);
			}

			var verificationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

			user.VerificationToken = verificationToken;

			var check = await userManager.CreateAsync(user, password);

			if (check.Succeeded)
			{
				await userManager.AddToRoleAsync(user, "User");
				user.Wallet = 0;
				await dbcontext.SaveChangesAsync();
			}
			await SendEmailForVerification(user.Email, verificationToken,verifyUrl);
			return check.Errors;
		}
        private async Task SendEmailForVerification(string userEmail, string code,string verifyUrl)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(MailboxAddress.Parse("eldon.reilly25@ethereal.email"));
            emailMessage.To.Add(MailboxAddress.Parse(userEmail));
            emailMessage.Subject = "Code for Verification";
			string fullUrl = verifyUrl + "/" + code;
            emailMessage.Body = new TextPart(TextFormat.Html)
            {
                Text = "To Verify Your Account Press this Link <a href=\"fullUrl\"> Click here </a>"
            };
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("eldon.reilly25@ethereal.email", "1xyrdZx7msYpj4KPgJ");
            smtp.Send(emailMessage);
            await smtp.DisconnectAsync(true);
            return;
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
			await userImage.CopyToAsync(stream);
			return fullName;
		}
        public bool DeleteImage(string imageName)
		{
			if (imageName == null)
			{
				return false;
			}
			if (!File.Exists(imageName))
			{
				return false;
			}
			File.Delete(imageName);
			return true;
		}
		public async Task<bool> UpdateUserImage(string userId,IFormFile newImage)
		{
            if (userId== null || newImage== null)
            {
				return false;
            }
			var user = await userManager.FindByIdAsync(userId);
			var success=DeleteImage(user.ProfileImagePath);
			if (!success)
			{
				return false;
			}
			var newImagePath = await SaveUserProfileAsync(newImage);
			user.ProfileImagePath = newImagePath;
			await userManager.UpdateAsync(user);
			await dbcontext.SaveChangesAsync();
			return true;
        }
		public async Task<bool> Verify(string verficationToken)
		{
			var user = await dbcontext.Users.FirstOrDefaultAsync(u=>u.VerificationToken== verficationToken);
			if (user!=null)
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
