using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco_InternShip_MVC.Models;

namespace Umbraco_InternShip_MVC.Data
{
    public class MvcDrawContext : DbContext
    {
        public MvcDrawContext(DbContextOptions<MvcDrawContext> options)
            : base(options)
        {
        }

        public DbSet<UserDraw> Users { get; set; }
        public DbSet<SerialNumber> SerialNumbers { get; set; }
        public DbSet<Draw> Draws { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Draw>().ToTable("Draw");
            modelBuilder.Entity<UserDraw>().ToTable("UserDraw");
            modelBuilder.Entity<SerialNumber>().ToTable("SerialNumber");
        }
    }
}

