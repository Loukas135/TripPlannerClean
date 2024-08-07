﻿using AutoMapper;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.AuthEntity;
using TripPlanner.Domain.Repositories;
using TripPlanner.Infrastructure.Persistence;

namespace TripPlanner.Infrastructure.Seeders.Users
{
    public class UserSeeder(TripPlannerDbContext dbContext, UserManager<User> userManager,IAccountRepository accountRepository,IMapper mapper) : IUserSeeder
    {
        public async Task Seed()
        {
            if(await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Users.Any())
                {
                     GetUsers();
                    await dbContext.SaveChangesAsync();
                }
            }
        }
        public async Task GetUsers()
        {
           List<UserSeedingRequest> users = [
                new(){
                 UserName="User1",
                 Email="User1@gmail.com",
                 Password="P@ssword1"
                },
                 new(){
                 UserName="User2",
                 Email="User2@gmail.com",
                 Password="P@ssword1"
                },
                new(){
                 UserName="User3",
                 Email="User3@gmail.com",
                 Password="P@ssword1"
                }
            ];

            foreach (var user in users)
            {
                var Userr=mapper.Map<User>(user);
                await accountRepository.Register(Userr, user.Password,"User");
            }
            UserSeedingRequest adminCredentials = new()
            {
                UserName = "Admin1",
                Email = "Admin1@gmail.com",
                Password = "P@ssword1"
            };
            var admin = mapper.Map<User>(adminCredentials);
            await accountRepository.RegisterAdmin(admin, adminCredentials.Password);
            List<UserSeedingRequest> owners = [
              new(){
                 UserName="Owner1",
                 Email="Owner1@gmail.com",
                 Password="P@ssword1",
                 Role="HotelOwner"
                },
                 new(){
                 UserName="Owner2",
                 Email="Owner2@gmail.com",
                 Password="P@ssword1",
                 Role="TourismOffice"
                },
                new(){
                 UserName="Owner3",
                 Email="Owner3@gmail.com",
                 Password="P@ssword1",
                 Role="CarRental"
                }
          ];
            foreach(var owner in owners)
            {
                var ownerValue = mapper.Map<User>(owner);
                await accountRepository.Register(ownerValue, owner.Password,owner.Role);
            }
        }
    }
}
