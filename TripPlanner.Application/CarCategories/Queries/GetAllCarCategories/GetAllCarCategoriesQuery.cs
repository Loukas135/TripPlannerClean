using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Application.CarTypes.Dtos;

namespace TripPlanner.Application.CarTypes.Queries.GetAllCarTypes
{
    public class GetAllCarCategoriesQuery:IRequest<IEnumerable<CarCategoryDto>>
    {
        
    }
}
