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
    public class RoomService : Service<Room>, IRoomService
    {
        public RoomService(DatabaseContext context) : base(context)
        {
        }

        public async Task<Room> GetRoomByIncludeAsync(Expression<Func<Room, bool>> expression)
        {
            return await _dbSet.AsNoTracking()
                .Where(x=> !x.IsDeleted)
                .Include(x=> x.Reservations)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<Room> GetRoomByIncludeAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Reservations)
                .FirstOrDefaultAsync(x=> x.RoomId == id);
        }

        public async Task<List<Room>> GetRoomsByIncludeAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Reservations)
                .ToListAsync();
        }

        public async Task<List<Room>> GetRoomsByIncludeAsync(Expression<Func<Room, bool>> expression)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Reservations)
                .Where(expression)
                .ToListAsync();
        }
    }
}
