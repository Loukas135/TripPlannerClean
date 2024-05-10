using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Domain.Entities.Service_Entities
{
    public class Ratings
    {
        public int Id { get; set; }
        [ForeignKey(nameof (ServiceId))]
        public int ServiceId { get; set; }
        
        [ForeignKey(nameof(UserId))]
        public string UserId { get; set; }
        
        [Range(0,5)]
        public int? Rating { get; set; }
    }
}
