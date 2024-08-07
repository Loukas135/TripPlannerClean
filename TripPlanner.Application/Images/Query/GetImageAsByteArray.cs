using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Images.Query
{
    public class GetImageAsByteArray(string path) : IRequest<byte[]>
    {
        public string path { get; set;
        } = path;
    }
}
