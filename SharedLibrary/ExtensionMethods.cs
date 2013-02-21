using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace SharedLibrary
{
    public static class ExtensionMethods
    {
        public static string ToFarsi(this string str)
        {
            string result = str;
            result = result.Replace("1", "۱");
            result = result.Replace("2", "۲");
            result = result.Replace("3", "۳");
            result = result.Replace("4", "۴");
            result = result.Replace("5", "۵");
            result = result.Replace("6", "۶");
            result = result.Replace("7", "۷");
            result = result.Replace("8", "۸");
            result = result.Replace("9", "۹");
            result = result.Replace("0", "۰");

            return result;
        }

        public static string ToPersianString(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            string time = string.Format("ساعت {0}:{1:d2}", dt.Hour, dt.Minute);
            string persianDate = string.Format("{0} {1} {2} {3} {4}"
                                               , TarikhFA.DayOfWeek.GetDayofWeekName((int)pc.GetDayOfWeek(dt.Date)),
                                               pc.GetDayOfMonth(dt.Date)
                                               , TarikhFA.PersianMonth.GetMonthName(pc.GetMonth(dt.Date)),
                                               pc.GetYear(dt.Date), time).ToFarsi();

            return persianDate;
        }
    }
}
