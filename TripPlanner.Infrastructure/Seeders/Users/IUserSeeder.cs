﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Infrastructure.Seeders.Users
{
    public interface IUserSeeder
    {
        Task Seed();
    }
}
