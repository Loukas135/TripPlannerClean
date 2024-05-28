using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Application.Services.Commands.Images
{
    public class AddImageCommand:IRequest<string>
    {
       public int ServiceId { get; set; }
        public IFormFile? ServiceImage { get; set; }
    }
}
