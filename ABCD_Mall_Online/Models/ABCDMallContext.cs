using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ABCD_Mall_Online.Models
{
    public class ABCDMallContext : DbContext
    {
        public ABCDMallContext(DbContextOptions<ABCDMallContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Film> Films { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
