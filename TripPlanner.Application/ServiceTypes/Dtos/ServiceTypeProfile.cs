using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Application.ServiceTypes.Dtos
{
    public class ServiceTypeProfile : Profile
    {
        public ServiceTypeProfile()
        {
            CreateMap<ServiceType,ServiceTypesDto>();
        }
    }
}
