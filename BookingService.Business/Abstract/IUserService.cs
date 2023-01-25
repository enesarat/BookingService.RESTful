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
        public Task<string> GetUserFirstName(int id);
        public Task<string> GetUserLastName(int id);
        public Task<string> GetUserEmail(int id);    
        public Task<string> GetUserPhoneNo(int id);
        public Task<bool> DeleteItemWithRecordCheck(int id);

    }
}
