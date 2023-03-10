using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IAppartmentService : IGenericService<Appartments>
    {
        public Task<bool> DeleteItemWithRecordCheck(int id);

    }
}
