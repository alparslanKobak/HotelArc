using HotelArc.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Process.Abstract
{
    public interface IRoleService : IService<Role>
    {
        Task<List<Role>> GetAllRolesByIncludeAsync();

        Task<List<Role>> GetAllRolesByIncludeAsync(Expression<Func<Role, bool>> expression);

        Task<Role> GetRoleByIncludeAsync(Expression<Func<Role, bool>> expression);

        Task<Role> GetRoleByIncludeAsync(Guid id);


    }
}
