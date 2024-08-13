using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TripPlanner.Domain.Entities.Service_Entities
{
    public class Rate
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; } = default!;
        
        [Range(0,5)]
        public float? Rating { get; set; }
    }
}
