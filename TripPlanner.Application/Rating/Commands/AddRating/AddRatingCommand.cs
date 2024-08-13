using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Rating.Commands.AddRating
{
    public class AddRatingCommand : IRequest<int>
    {
        public int? ServiceId { get; set; }
        public string? UserId { get; set; }
        public float Rating { get; set; } = 0;
    }
}
