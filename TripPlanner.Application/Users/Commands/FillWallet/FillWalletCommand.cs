using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Users.Commands.FillWallet
{
    public class FillWalletCommand:IRequest<bool>
    {
        public string EmailAddress { get; set; }
        public int Amount { get; set; }
    }
}
