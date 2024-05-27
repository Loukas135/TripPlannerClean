using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;

namespace TripPlanner.Application.CarTypes.Dtos
{
    public class CarCategoriesProfile:Profile
    {
        public CarCategoriesProfile()
        {
            CreateMap<CarCategory, CarCategoryDto>();
        }
    }
}
