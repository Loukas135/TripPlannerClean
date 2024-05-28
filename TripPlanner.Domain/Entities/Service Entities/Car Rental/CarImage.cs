using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Car_Rental
{
	public class CarImage
	{
		[Key]
		public int Id { get; set; }
		public int CarId { get; set; }
		public string CarImagePath { get; set; }
	}
}
