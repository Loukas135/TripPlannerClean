using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TripPlanner.Application.Services.Commands.CreateService;
using TripPlanner.Domain.Entities.Service_Entities;

namespace TripPlanner.Application.Services.Dtos
{
	public class ServiceProfile : Profile
	{
		public ServiceProfile() 
		{
			CreateMap<Service, ServiceDto>()
				.ForMember(d => d.Rooms, opt => opt.MapFrom(src => src.Rooms))
				.ForMember(d => d.Cars, opt => opt.MapFrom(src => src.Cars))
				.ForMember(d => d.Trips, opt => opt.MapFrom(src => src.Trips));

			CreateMap<CreateServiceCommand, Service>();
		}
	}
}
