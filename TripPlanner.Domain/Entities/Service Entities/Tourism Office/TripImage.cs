using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Tourism_Office
{
	public class TripImage
	{
		public int Id { get; set; }
		public int TripId { get; set; }
		public string TripImagePath { get; set; }
	}
}
