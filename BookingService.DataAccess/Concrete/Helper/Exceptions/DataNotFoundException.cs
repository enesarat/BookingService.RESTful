using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Concrete.Helper.Exceptions
{
    public class DataNotFoundException : Exception
    {
        public DataNotFoundException(string type, object id)
        : base($"{id} id değerine sahip, {type} tipinde olan herhangi bir obje bulunamadı! ") { }

        public DataNotFoundException(string type)
        : base($"{type} tipindeki obje, işlem için bulunamamıştır!") { }

    }
}
