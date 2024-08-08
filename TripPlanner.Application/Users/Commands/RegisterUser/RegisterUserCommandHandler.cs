using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.RegisterUser
{
	public class RegisterUserCommandHandler(IMapper mapper,
		IAccountRepository accountRepository) : IRequestHandler<RegisterUserCommand, IEnumerable<IdentityError>>
	{
		public async Task<IEnumerable<IdentityError>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
		{
			var user = mapper.Map<User>(request);
			user.UserName = request.UserName;
			user.ProfileImagePath = await accountRepository.SaveUserProfileAsync(request.UserProfile);

			var user_id= await accountRepository.RegisterUser(user, request.Password);
			return user_id;
		}
		private async Task SendEmailForVerification(string userEmail,string code)
		{
			var emailMessage = new MimeMessage();
			emailMessage.From.Add(MailboxAddress.Parse("eldon.reilly25@ethereal.email"));
			emailMessage.To.Add(MailboxAddress.Parse(userEmail));
			emailMessage.Subject = "Code for Verification";
			emailMessage.Body = new TextPart(TextFormat.Html)
			{
				Text = "This is the code to verify your account" + code
			};
			using var smtp = new SmtpClient();
			await smtp.ConnectAsync("smtp.ethereal.email", 587, MailKit.Security.SecureSocketOptions.StartTls);
			smtp.Authenticate("eldon.reilly25@ethereal.email", "1xyrdZx7msYpj4KPgJ");
			smtp.Send(emailMessage);
			await smtp.DisconnectAsync(true);
			return;
		}
	}
}
