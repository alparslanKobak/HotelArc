using HotelArc.Business;
using HotelArc.Kernel.Entities;
using HotelArc.Process.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Process.Concrete
{
    public class ReservationService : Service<Reservation>, IReservationService
    {
        public ReservationService(DatabaseContext context) : base(context)
        {
        }

        public async Task<Reservation> GetReservationByIncludeAsync(Expression<Func<Reservation, bool>> expression)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.AppUser)
                .Include(x => x.Room)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<Reservation> GetReservationByIncludeAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.AppUser)
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.ReservationId == id);
        }

        public async Task<List<Reservation>> GetReservationsByIncludeAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.AppUser)
                .Include(x => x.Room)
                .ToListAsync();
        }

        public Task<List<Reservation>> GetReservationsByIncludeAsync(Expression<Func<Reservation, bool>> expression)
        {
            return _dbSet.AsNoTracking()
                 .Where(x => !x.IsDeleted)
                 .Include(x => x.AppUser)
                 .Include(x => x.Room)
                 .Where(expression)
                 .ToListAsync();
        }

        public async Task<bool> IsRoomReserved(Guid roomId, DateTime checkIn, DateTime checkOut, Guid? currentReservationId = null)
        {
            bool isReserved = await _dbSet.AsNoTracking()
                                         .AnyAsync(r => r.RoomId == roomId
                                                        && r.CheckIn < checkOut
                                                        && r.CheckOut > checkIn
                                                        && !r.IsDeleted
                                                        && (!currentReservationId.HasValue || r.ReservationId != currentReservationId.Value)); // Eğer güncelleme işlemi yapılıyorsa, güncellenen rezervasyonun kendisini hariç tutulmalıdır.

            return isReserved;
        }
    }
}
