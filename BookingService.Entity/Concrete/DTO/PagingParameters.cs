using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Entity.Concrete.DTO
{
    public class PagingParameters
    {
        public int PageNumber { get; set; } = 1;

        const int maxPageSize = 25;

        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > maxPageSize)
                {
                    _pageSize = maxPageSize;
                }
                else
                {
                    _pageSize = value;
                }
            }
        }
    }
}
