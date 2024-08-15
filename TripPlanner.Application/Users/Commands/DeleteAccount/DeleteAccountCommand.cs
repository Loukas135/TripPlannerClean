using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Commands.DeleteAccount
{
    public class DeleteAccountCommand:IRequest
    {
        public string Password { get; set; } = default!;
    }
}
