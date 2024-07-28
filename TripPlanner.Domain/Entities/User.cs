using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Domain.Entities
{
	public class User : IdentityUser
	{
		public int Wallet { get; set; } = 0;
		public List<Rate> Ratings { get; set; } = [];
		public DateTime CreatedAt { get; set; } = DateTime.Today;
		public DateTime UpdatedAt { get; set; } = DateTime.Today;
		public Service? OwnedService { get; set; }

		public string? VerificationToken { get; set; }
		public DateTime? VerifiedAt { get; set; }
		[AllowNull]
		public string? ProfileImagePath { get; set; }
	}
}
