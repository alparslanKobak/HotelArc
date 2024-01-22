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
    public class AppUserService : Service<AppUser>, IAppUserService
    {
        public AppUserService(DatabaseContext context) : base(context)
        {
        }

        public async Task<AppUser> GetAppUserByIncludeAsync(Guid id)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Reservations)
                .FirstOrDefaultAsync(x => x.AppUserId == id);
        }

        public async Task<List<AppUser>> GetAppUsersByIncludeAsync()
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Reservations)
                .ToListAsync();
        }

        public async Task<List<AppUser>> GetAppUsersByIncludeAsync(Expression<Func<AppUser, bool>> expression)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Include(x => x.Reservations)
                .Where(expression)
                .ToListAsync();
        }
    }
}
