using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using TripPlanner.Domain.Repositories;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Application.Services.Commands.Images
{
    public class AddImageCommandHandler(IWebHostEnvironment environment,
        IMapper mapper, 
        IServiceImageRepository serviceImageRepository) :
        IRequestHandler<AddImageCommand, string>
    {
        public async Task<string> Handle(AddImageCommand request, CancellationToken cancellationToken)
        {
            if(request.ServiceImage == null)
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
            var serviceImage = new ServiceImage
            {
                Id = 0,
                ServiceId = request.ServiceId,
                ServiceImagePath = fullName
            };
            var serviceImageId = serviceImageRepository.addServiceImageAsync(serviceImage);
            return fullName;
        }
    }
}
