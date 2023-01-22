using BookingService.Entity.Abstract;
using BookingService.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Abstract
{
    public interface IGenericEntityDAL<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAllItems();
        Task<List<T>> GetElementsByPaging(PagingParameters pagingParameters);
        List<T> GetAllItemsByFilter(Expression<Func<T, bool>> filter);
        Task<T> GetItemById(int id);
        Task<T> InsertItem(T item);
        Task<T> UpdateItem(T item);
        Task DeleteItem(int id);
    }
}
