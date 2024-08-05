using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Commands.UpdateImage
{
    public class UpdateUserImageCommand:IRequest
    {
        public IFormFile newImage { get; set; }
    }
}
