﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripPlanner.Application.Services.Commands.DeleteService
{
	public class DeleteServiceCommand(int govId, int serId) : IRequest
	{
		public int GovernorateId { get; } = govId;
		public int ServiceId { get; } = serId;
	}
}
