using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IUserService : IGenericService<Users>
    {
        public Task<string> GetUserFirstName(Users item);
        public Task<string> GetUserLastName(Users item);
        public Task<string> GetUserEmail(Users item);
        public Task<string> GetUserPhoneNo(Users item);
        public Task<bool> DeleteItemWithRecordCheck(int id);

    }
}
