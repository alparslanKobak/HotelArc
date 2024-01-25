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
    public class RoleService : Service<Role>, IRoleService
    {
        public RoleService(DatabaseContext context) : base(context)
        {
        }

        public async Task<Role> GetRoleByIncludeAsync(Expression<Func<Role, bool>> expression)
        {
            return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(expression);
        }

        public async Task<Role> GetRoleByIncludeAsync(Guid id)
        {
           return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .FirstOrDefaultAsync(x => x.RoleId == id);
        }

        public async Task<List<Role>> GetAllRolesByIncludeAsync()
        {
           return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Role>> GetAllRolesByIncludeAsync(Expression<Func<Role, bool>> expression)
        {
           return await _dbSet.AsNoTracking()
                .Where(x => !x.IsDeleted)
                .Where(expression)
                .ToListAsync();
        }
    }
}
