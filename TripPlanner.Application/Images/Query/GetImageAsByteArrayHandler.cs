using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Images.Query
{
    internal class GetImageAsByteArrayHandler : IRequestHandler<GetImageAsByteArray, byte[]>
    {
        public Task<byte[]> Handle(GetImageAsByteArray request, CancellationToken cancellationToken)
        {
            return File.ReadAllBytesAsync(request.path, cancellationToken);
        }
    }
}
