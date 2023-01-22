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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BookingService.Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        ICompanyDAL companyAccess;
        public CompanyManager(ICompanyDAL companyAccess)
        {
            this.companyAccess = companyAccess;
        }
        public async Task DeleteItem(int id)
        {
            await companyAccess.DeleteItem(id);
        }

        public async Task<List<Company>> GetAllElement()
        {
            var companyList = await companyAccess.GetAllItems();
            return companyList;
        }

        public List<Company> GetAllItemsByFilter(Expression<Func<Company, bool>> filter)
        {
            return companyAccess.GetAllItemsByFilter(filter);
        }

        public async Task<Company> GetElementById(int id)
        {
            var company = await companyAccess.GetItemById(id);
            return company;
        }

        public async Task<List<Company>> GetElementsByPaging(PagingParameters pagingParameters)
        {
            var companyList = await companyAccess.GetElementsByPaging(pagingParameters);
            return companyList;
        }

        public async Task<Company> InsertElement(Company item)
        {
            List<Company> companyList = await companyAccess.GetAllItems();
            var count = companyList.Count;
            if (count > 0 || companyList != null)
            {
                var lastCompany = companyList[count - 1];
                item.id = lastCompany.id + 1;
            }
            else
            {
                item.id = 0;
            }
            await companyAccess.InsertItem(item);
            return item;
        }

        public async Task<Company> UpdateElement(Company item)
        {
            await companyAccess.UpdateItem(item);
            return item;
        }
    }
}
