using HotelArc.Kernel.Entities;
using HotelArc.Process.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Process.Abstract
{
    public interface IRoomService : IService<Room>
    {
        
        Task<List<Room>> GetRoomsByIncludeAsync();  

        Task<List<Room>> GetRoomsByIncludeAsync(Expression<Func<Room, bool>> expression);

        Task<Room> GetRoomByIncludeAsync(Expression<Func<Room, bool>> expression);

        Task<Room> GetRoomByIncludeAsync(Guid id);


    }
}
