using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.CarTypes.Dtos;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.CarTypes.Queries.GetAllCarTypes
{
    public class GetAllCarCategoriesQueryHandler(ICarCategoriesRepository carTypes,
        IMapper mapper) : IRequestHandler<GetAllCarCategoriesQuery, IEnumerable<CarCategoryDto>>
    {
        public async Task<IEnumerable<CarCategoryDto>> Handle(GetAllCarCategoriesQuery request, CancellationToken cancellationToken)
        {
            var cars = await carTypes.GetAllAsync();
            var allTypes = mapper.Map<IEnumerable<CarCategoryDto>>(cars);
            return allTypes;
        }

       
    }
}
