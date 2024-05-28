using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace TripPlanner.Application.Services.Commands.Images
{
    public class AddImageCommandHandler(IWebHostEnvironment environment) : IRequestHandler<AddImageCommand, string>
    {
        public async Task<string> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            if(request.ServiceImage==null)
            return null;
            var contentPath = environment.ContentRootPath;
            var path = Path.Combine(contentPath, "Images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var extension = Path.GetExtension(request.ServiceImage.FileName);
            var fileName = $"{Guid.NewGuid().ToString()}{extension}";
            var fullName = Path.Combine(path, fileName);
            using var stream = new FileStream(fullName, FileMode.Create);
            await request.ServiceImage.CopyToAsync(stream);
            return fullName;
        }
    }
}
