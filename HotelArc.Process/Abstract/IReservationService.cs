using HotelArc.Kernel.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HotelArc.Process.Abstract
{
    public interface IReservationService : IService<Reservation>
    {
        Task<List<Reservation>> GetReservationsByIncludeAsync();

        Task<List<Reservation>> GetReservationsByIncludeAsync(Expression<Func<Reservation, bool>> expression);

        Task<Reservation> GetReservationByIncludeAsync(Expression<Func<Reservation, bool>> expression);

        Task<Reservation> GetReservationByIncludeAsync(Guid id);

        Task<bool> IsRoomReserved(Guid roomId, DateTime checkIn, DateTime checkOut, Guid? currentReservationId = null);
    }
}
