using HotelArc.Kernel.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Business
{
    public class DatabaseContext : DbContext
    {

        public DbSet<AppUser> AppUsers { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=HotelArc;Trusted_Connection=True;");
        //}

        public DatabaseContext()
        {

        }


        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }


    }
}
