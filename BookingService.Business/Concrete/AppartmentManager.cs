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
    public class AppartmentManager : IAppartmentService
    {
        IAppartmentsDAL appartmentsAccess;
        IBookingsDAL bookingsAccess;
        public AppartmentManager(IAppartmentsDAL appartmentsAccess, IBookingsDAL bookingsAccess)
        {
            this.appartmentsAccess = appartmentsAccess;
            this.bookingsAccess = bookingsAccess;
        }
        public async Task DeleteItem(int id)
        {
            await appartmentsAccess.DeleteItem(id);
        }

        public async Task<bool> DeleteItemWithRecordCheck(int id)
        {
            var deleteItem = appartmentsAccess.GetItemById(id);
            var isUsed = bookingsAccess.GetAllItemsByFilter(x => x.apartment_id == deleteItem.Result.id);
            if (isUsed.Count == 0)
            {
                await appartmentsAccess.DeleteItem(deleteItem.Result.id);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<Appartments>> GetAllElement()
        {
            var appartmentsList = await appartmentsAccess.GetAllItems();
            return appartmentsList;
        }

        public List<Appartments> GetAllItemsByFilter(Expression<Func<Appartments, bool>> filter)
        {
            return appartmentsAccess.GetAllItemsByFilter(filter);
        }

        public async Task<Appartments> GetElementById(int id)
        {
            var appartment = await appartmentsAccess.GetItemById(id);
            return appartment;
        }

        public async Task<List<Appartments>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            var appartmentList = await appartmentsAccess.GetElementsByPaging(pagingParameters);
            return appartmentList;
        }

        public async Task<Appartments> InsertElement(Appartments item)
        {
            await appartmentsAccess.InsertItem(item);
            return item;
        }

        public async Task<Appartments> UpdateElement(Appartments item)
        {
            await appartmentsAccess.UpdateItem(item);
            return item;
        }
    }
}
