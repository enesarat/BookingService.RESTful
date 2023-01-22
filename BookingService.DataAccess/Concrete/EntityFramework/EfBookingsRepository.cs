using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Concrete.Repository;
using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Concrete.EntityFramework
{
    public class EfBookingsRepository : EfGenericRepository<Bookings>, IBookingsDAL
    {
    }
}
