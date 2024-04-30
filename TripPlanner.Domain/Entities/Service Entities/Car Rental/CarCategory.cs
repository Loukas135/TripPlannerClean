using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Car_Rental
{
	public class CarCategory
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public List<Car>? Cars { get; set; }
	}
}
