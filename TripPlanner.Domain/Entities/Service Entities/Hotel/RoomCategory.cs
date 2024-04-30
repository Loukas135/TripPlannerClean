using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Hotel
{
	public class RoomCategory
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public List<Room>? Rooms { get; set; }
	}
}
