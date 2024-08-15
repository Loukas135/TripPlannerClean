using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Exceptions;
using TripPlanner.Domain.Repositories;

namespace TripPlanner.Application.Users.Commands.DeleteAccount
{
    internal class DeleteAccountCommandHandler(IAccountRepository accountRepository,
        IUserContext userContext,
        IReservationRespository reservationRespository ) : IRequestHandler<DeleteAccountCommand>
    {
        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var userId = userContext.GetCurrentUser()!.Id;
            var user = await accountRepository.GetUserAsync(userId);
            var checkPassword = await accountRepository.CheckPassword(userId, request.Password);
            if (!checkPassword)
            {
                throw new Exception();
            }
            var reservations = await reservationRespository.GetUserReservations(userId);
            if (reservations != null) {
                foreach (var reservation in reservations)
                {
                    if (reservation.ElectronicPayment)
                    {
                        user!.Wallet += reservation.Cost;
                    }

                    await reservationRespository.DeleteReservation(reservation);
                }
        }
            await accountRepository.DeleteUserRatings(user);
            await accountRepository.DeleteAccount(userId);
            return;
        }
    }
}
