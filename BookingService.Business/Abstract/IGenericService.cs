using BookingService.Entity.Abstract;
using BookingService.Entity.Concrete.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Business.Abstract
{
    public interface IGenericService<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAllElement();
        Task<List<T>> GetElementsByPaging(PagingParameters pagingParameters);
        List<T> GetAllItemsByFilter(Expression<Func<T, bool>> filter);
        Task<T> GetElementById(int id);
        Task<T> InsertElement(T item);
        Task<T> UpdateElement(T item);
        Task DeleteItem(int id);

    }
}
