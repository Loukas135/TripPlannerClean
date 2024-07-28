using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Dtos
{
    public class NumberOfUsersDto
    {
        public string TypeName { get; set; } = default!;
        public int NumberOfType { get; set; } = default!;
    }
}
