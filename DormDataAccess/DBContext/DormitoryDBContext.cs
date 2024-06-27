using DormDataAccess.DBContext.EntityConfiguration;
using DormModel.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DormDataAccess.DBContext
{
    public class DormitoryDBContext : IdentityDbContext<AppUser>
    {

        public DormitoryDBContext(DbContextOptions<DormitoryDBContext> options)
        : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Dorm> Dorms { get; set; }
        public DbSet<DormFloor> DormFloors { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<FloorRoom> FloorRooms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<RoomOrder> RoomOrders { get; set; }
        public DbSet<OrderSideService> OrderSideServices { get; set; }
        public DbSet<SideService> SideServices { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new DormConfiguration());
            modelBuilder.ApplyConfiguration(new DormFloorConfiguration());
            modelBuilder.ApplyConfiguration(new FloorConfiguration());
            modelBuilder.ApplyConfiguration(new FloorRoomConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderSideServiceConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new SideServiceConfiguration());
            modelBuilder.ApplyConfiguration(new RoomOrderConfiguration());

            // Optionally, seed data
            modelBuilder.Entity<Dorm>().HasData(
                new Dorm { Id = 1, Name = "Dorm A" },
                new Dorm { Id = 2, Name = "Dorm B" }
            );

            modelBuilder.Entity<Floor>().HasData(
                new Floor { Id = 1, Name = "Floor 1" },
                new Floor { Id = 2, Name = "Floor 2" },
                new Floor { Id = 3, Name = "Floor 3" }
            );

            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, Name = "Room 101", MaximumNumberOfPeople = 4, CurrentNumberOfPeople = 2, IsMaximum = false, Price = 700000 },
                new Room { Id = 2, Name = "Room 102", MaximumNumberOfPeople = 2, CurrentNumberOfPeople = 2, IsMaximum = true, Price = 900000 }
            );

            modelBuilder.Entity<SideService>().HasData(
                new SideService { Id = 1, Name = "Laundry", Description = "Nothing", Price = 100000 },
                new SideService { Id = 2, Name = "Fixing", Description = "Nothing", Price = 50000 }
            );

            modelBuilder.Entity<DormFloor>().HasData(
                new DormFloor { DormId = 1, FloorId = 1 },
                new DormFloor { DormId = 1, FloorId = 2 },
                new DormFloor { DormId = 2, FloorId = 2 },
                new DormFloor { DormId = 2, FloorId = 3 }
            );

            modelBuilder.Entity<FloorRoom>().HasData(
                new FloorRoom { FloorId = 1, RoomId = 1 },
                new FloorRoom { FloorId = 1, RoomId = 2 },
                new FloorRoom { FloorId = 2, RoomId = 1 }
            );

        }
    }
}
