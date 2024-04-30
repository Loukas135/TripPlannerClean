using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TripPlanner.Domain.Entities;
using TripPlanner.Domain.Entities.Service_Entities;
using TripPlanner.Domain.Entities.Service_Entities.Car_Rental;
using TripPlanner.Domain.Entities.Service_Entities.Hotel;
using TripPlanner.Domain.Entities.Service_Entities.Tourism_Office;

namespace TripPlanner.Infrastructure.Persistence
{
	public class TripPlannerDbContext(DbContextOptions<TripPlannerDbContext> options) : DbContext(options)
	{
		internal DbSet<Governorate> Governorates { get; set; }
		internal DbSet<ServiceType> ServiceTypes { get; set; }
		internal DbSet<Service> Services { get; set; }
		internal DbSet<RoomCategory> RoomCategories { get; set; }
		internal DbSet<Room> Rooms { get; set; }
		internal DbSet<CarCategory> CarCategories { get; set; }
		internal DbSet<Car> Cars { get; set; }
		internal DbSet<Trip> Trips { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			//Each Governorate and Service Type has many Services
			modelBuilder.Entity<Governorate>()
				.HasMany(g => g.Services)
				.WithOne()
				.HasForeignKey(s => s.GovernorateId);

			modelBuilder.Entity<ServiceType>()
				.HasMany(st => st.Services)
				.WithOne()
				.HasForeignKey(s => s.ServiceTypeId);

			//Each Service has many Cars, Trips or Rooms
			modelBuilder.Entity<Service>()
				.HasMany(s => s.Trips)
				.WithOne()
				.HasForeignKey(t => t.ServiceId);

			modelBuilder.Entity<Service>()
				.HasMany(s => s.Rooms)
				.WithOne()
				.HasForeignKey(r => r.ServiceId);

			modelBuilder.Entity<Service>()
				.HasMany(s => s.Cars)
				.WithOne()
				.HasForeignKey(c => c.ServiceId);

			modelBuilder.Entity<Service>()
				.HasMany(s => s.Reservations)
				.WithOne()
				.HasForeignKey(r => r.ServiceId);

			//Each Room, Car or Trip has many reservations
			modelBuilder.Entity<Room>()
				.HasMany(r => r.Reservations)
				.WithOne()
				.HasForeignKey(res => res.RoomId);

			modelBuilder.Entity<Car>()
				.HasMany(c => c.Reservations)
				.WithOne()
				.HasForeignKey(res => res.RoomId);

			modelBuilder.Entity<Trip>()
				.HasMany(t => t.Reservations)
				.WithOne()
				.HasForeignKey(res => res.RoomId);

			// Each RoomCategory or CarCategory has many Rooms or Cars
			modelBuilder.Entity<RoomCategory>()
				.HasMany(rc => rc.Rooms)
				.WithOne()
				.HasForeignKey(r => r.RoomCategoryId);

			modelBuilder.Entity<CarCategory>()
				.HasMany(cc => cc.Cars)
				.WithOne()
				.HasForeignKey(c => c.CarCategoryId);
		}
	}
}
