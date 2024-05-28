using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Domain.Entities.Service_Entities.Hotel
{
	public class RoomImage
	{
		[Key]
		public int Id { get; set; }
		public int RoomId { get; set; }
		public string RoomImagePath { get; set; }
	}
}
