using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DataAccess.Concrete.Helper.Calculation
{
    public class TimeCalculator
    {
        public string timeString { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public int day { get; set; }
        public string hourInfo { get; set; }

        public TimeCalculator(string timeString)
        {
            this.timeString = timeString;
            setHourInfo();
            setShortDate();
            setYear();
            setMonth();
            setDay();
        }

        public string setHourInfo()
        {
            int idxCrop = timeString.IndexOf("T");
            string hourInfo = timeString.Substring(10, timeString.Length - 10);
            this.hourInfo = hourInfo;
            return hourInfo;
        }
        public string getHourInfo()
        {
            return this.hourInfo;
        }

        public string setShortDate()
        {
            int idxCrop = timeString.IndexOf("T");
            string shortDate = timeString.Substring(0, 10);
            this.timeString = shortDate;
            return timeString;
        }
        public string getShortDate()
        {
            return this.timeString;
        }

        public int setYear()
        {
            var date = this.timeString;
            int idxCrop = date.IndexOf("-");
            var yearAsStr = date.Substring(0, idxCrop);
            int year = Convert.ToInt32(yearAsStr);
            this.year = year;
            return year;
        }
        public int getYear()
        {
            return this.year;
        }
        public int setMonth()
        {
            var date = this.timeString;
            int idxCrop1 = date.IndexOf("-");
            var monthAsStr = date.Substring(idxCrop1 + 1, 2);
            int month = Convert.ToInt32(monthAsStr);
            this.month = month;
            return month;
        }
        public int getMonth()
        {
            return this.month;
        }


        public int setDay()
        {
            var date = this.timeString;
            var dayAsStr = date.Substring(8, 2);
            int day = Convert.ToInt32(dayAsStr);
            this.day = day;
            return day;
        }
        public int getDay()
        {
            return this.day;
        }


    }
}
