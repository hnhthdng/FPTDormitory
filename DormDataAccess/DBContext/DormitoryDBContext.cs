using DormDataAccess.EntityConfiguration;
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
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderSideService> OrderSideServices { get; set; }
        public DbSet<Payment> Payment { get; set; }
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
            modelBuilder.ApplyConfiguration(new InvoiceConfiguration());
            modelBuilder.ApplyConfiguration(new ItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
            modelBuilder.ApplyConfiguration(new OrderSideServiceConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new RoomConfiguration());
            modelBuilder.ApplyConfiguration(new SideServiceConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());

        }
    }
}
