using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities
{
	public class ServiceImage
	{
		[Key]
		public int Id { get; set; }
		public int ServiceId { get; set; }
		public string ServiceImagePath { get; set; }
		
	}
}
