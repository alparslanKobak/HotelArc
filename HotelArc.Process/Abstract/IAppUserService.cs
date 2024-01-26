using HotelArc.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Process.Abstract
{
    public interface IAppUserService : IService<AppUser>
    {
        Task<AppUser> GetAppUserByIncludeAsync(Guid id);
 
        Task<List<AppUser>> GetAppUsersByIncludeAsync();
        Task<List<AppUser>> GetAppUsersByIncludeAsync(Expression<Func<AppUser, bool>> expression);
       Task<AppUser> GetAppUserByIncludeAsync(Expression<Func<AppUser, bool>> expression);



    }
}
