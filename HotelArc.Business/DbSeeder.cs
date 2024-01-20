using HotelArc.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Business
{
    public class DbSeeder
    {
        public static void Seed(DatabaseContext _context)
        {
            SeedRoles(_context);
            SeedAppUsers(_context);
            SeedRooms(_context);
            SeedReservation(_context);
        }

        private static void SeedRoles(DatabaseContext _context)
        {
            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(
                new Role { RoleName = "Admin" },
                new Role { RoleName = "User" }
                );
                _context.SaveChanges();
            }
        }

        private static void SeedAppUsers(DatabaseContext _context)
        {
            if (!_context.AppUsers.Any())
            {
                _context.AppUsers.AddRange(
                 new AppUser
                 {
                     Email = "admin@HotelArc.com",
                     Password = "Admin123.",
                     RoleId = _context.Roles.FirstOrDefault(x => x.RoleName == "Admin").RoleId,
                     UserName = "Admin",
                     PhoneNumber = "1234567890",

                 },

                 new AppUser
                 {
                     Email = "teknoman@gmail.com",
                     Password = "Teknoman123.",
                     RoleId = _context.Roles.FirstOrDefault(x => x.RoleName == "User").RoleId,
                     UserName = "Teknoman",
                     PhoneNumber = "1234567890",
                 },

                 new AppUser
                 {
                     Email = "teknogirl@gmail.com",
                     Password = "Teknogirl123.",
                     RoleId = _context.Roles.FirstOrDefault(x => x.RoleName == "User").RoleId,
                     UserName = "Teknogirl",
                     PhoneNumber = "1234567890",
                 }
                 );

                _context.SaveChanges();
            }
        }

        private static void SeedRooms(DatabaseContext _context)
        {
            if (!_context.Rooms.Any())
            {
                _context.Rooms.AddRange(
                 new Room
                 {
                     RoomNumber = "101A",
                     RoomType = "Single",
                     Price = 600,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,

                 },

                 new Room
                 {
                     RoomNumber = "101B",
                     RoomType = "Single",
                     Price = 650,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "102A",
                     RoomType = "Double",
                     Price = 700,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "102B",
                     RoomType = "Double",
                     Price = 750,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "103A",
                     RoomType = "Triple",
                     Price = 800,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "103B",
                     RoomType = "Triple",
                     Price = 850,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "104A",
                     RoomType = "Quad",
                     Price = 900,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "104B",
                     RoomType = "Quad",
                     Price = 950,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "105A",
                     RoomType = "Queen",
                     Price = 1000,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "105B",
                     RoomType = "Queen",
                     Price = 1050,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "106A",
                     RoomType = "King",
                     Price = 1100,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 },

                 new Room
                 {
                     RoomNumber = "106B",
                     RoomType = "King",
                     Price = 1150,
                     RoomImage = "HotelArchSuitDefault.png",
                     IsAvaliable = true,
                 }

              );
                _context.SaveChanges();
            }
        }

        private static void SeedReservation(DatabaseContext _context)
        {
            if (!_context.Reservations.Any())
            {
                _context.Reservations.AddRange(

                    new Reservation
                    {
                        CheckIn = DateTime.Now,
                        CheckOut = DateTime.Now.AddDays(4),
                        RoomId = _context.Rooms.FirstOrDefault(x => x.RoomNumber == "101A").RoomId,
                        AppUserId = _context.AppUsers.FirstOrDefault(x=> x.UserName == "Teknoman").AppUserId
                        
                    },

                    new Reservation
                    {
                        CheckIn = DateTime.Now,
                        CheckOut = DateTime.Now.AddDays(4),
                        RoomId = _context.Rooms.FirstOrDefault(x => x.RoomNumber == "101B").RoomId,
                        AppUserId = _context.AppUsers.FirstOrDefault(x => x.UserName == "Teknogirl").AppUserId
                    }

                    );
                _context.SaveChanges();
            }
        }
    }
}
