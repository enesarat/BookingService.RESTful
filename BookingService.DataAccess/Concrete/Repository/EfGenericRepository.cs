using BookingService.DataAccess.Abstract;
using BookingService.DataAccess.Concrete.Helper.Exceptions;
using BookingService.Entity.Abstract;
using BookingService.Entity.Concrete.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Concrete.Repository
{
    public class EfGenericRepository<T> : IGenericEntityDAL<T> where T : class, IEntity, new()
    {
        public async Task DeleteItem(int id)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                var deleteItem = await GetItemById(id);
                dbContext.Set<T>().Remove(deleteItem);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<T>> GetAllItems()
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                var itemList = await dbContext.Set<T>().ToListAsync();
                return itemList;
            }
        }

        public async Task<List<T>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                var itemList = await dbContext.Set<T>()
                    .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                    .Take(pagingParameters.PageSize)
                    .ToListAsync();
                return itemList;
            }
        }

        public async Task<T> GetItemById(int id)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                var item = await dbContext.Set<T>().FindAsync(id);
                if (item == null)
                    throw new DataNotFoundException(nameof(item), id);
                return item;
            }
        }

        public async Task<T> InsertItem(T item)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                await dbContext.Set<T>().AddAsync(item);
                await dbContext.SaveChangesAsync();
                return item;
            }
        }

        public async Task<T> UpdateItem(T item)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                dbContext.Set<T>().Update(item);
                await dbContext.SaveChangesAsync();
                return item;
            }
        }

        public List<T> GetAllItemsByFilter(Expression<Func<T, bool>> filter)
        {
            using (BookingServiceDbContext dbContext = new BookingServiceDbContext())
            {
                if (filter == null)
                {
                    var entityList = dbContext.Set<T>().ToList();
                    return entityList;
                }
                else
                {
                    var entityList = dbContext.Set<T>().Where(filter).ToList();
                    return entityList;
                }
            }
        }
    }
}
