﻿using BookingService.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Abstract
{
    public interface ICompanyDAL : IGenericEntityDAL<Company>
    {
    }
}
