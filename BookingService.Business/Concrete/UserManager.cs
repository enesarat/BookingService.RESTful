using BookingService.Business.Abstract;
using BookingService.DataAccess.Abstract;
using BookingService.Entity.Concrete;
using BookingService.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUsersDAL usersAccess;
        IBookingsDAL bookingsAccess;
        public UserManager(IUsersDAL usersAccess, IBookingsDAL bookingsAccess)
        {
            this.usersAccess = usersAccess;
            this.bookingsAccess = bookingsAccess;
        }
        public async Task DeleteItem(int id)
        {
            await usersAccess.DeleteItem(id);
        }

        public async Task<bool> DeleteItemWithRecordCheck(int id)
        {
            var deleteItem = usersAccess.GetItemById(id);
            var isUsed = bookingsAccess.GetAllItemsByFilter(x => x.user_id == deleteItem.Result.id);
            if (isUsed.Count == 0)
            {
                await usersAccess.DeleteItem(deleteItem.Result.id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Users>> GetAllElement()
        {
            var usersList = await usersAccess.GetAllItems();
            return usersList;
        }

        public List<Users> GetAllItemsByFilter(Expression<Func<Users, bool>> filter)
        {
            return usersAccess.GetAllItemsByFilter(filter);
        }

        public async Task<Users> GetElementById(int id)
        {
            var user = await usersAccess.GetItemById(id);
            return user;
        }

        public async Task<List<Users>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            var userList = await usersAccess.GetElementsByPaging(pagingParameters);
            return userList;
        }

        public async Task<string> GetUserEmail(Users item)
        {
            string email = item.email;
            return email;
        }

        public async Task<string> GetUserFirstName(Users item)
        {
            string firstName = item.first_name;
            return firstName;
        }

        public async Task<string> GetUserLastName(Users item)
        {
            string lastName = item.last_name;
            return lastName;
        }

        public async Task<string> GetUserPhoneNo(Users item)
        {
            string phoneNo = item.phone;
            return phoneNo;
        }

        public async Task<Users> InsertElement(Users item)
        {
            List<Users> userLsit = await usersAccess.GetAllItems();
            var count = userLsit.Count;
            if (count > 0)
            {
                var lastUser = userLsit[count - 1];
                item.id = lastUser.id + 1;
            }
            else
            {
                item.id = 0;
            }
            await usersAccess.InsertItem(item);
            return item;
        }

        public async Task<Users> UpdateElement(Users item)
        {
            await usersAccess.UpdateItem(item);
            return item;
        }
    }
}
