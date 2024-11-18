using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nabeghe.Application.Convertor
{
    public static class DateConvartor
    {
        public static string Toshamsi(this DateTime value)
        {
            var pc = new PersianCalendar();
            var Toshamsi = pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                           pc.GetDayOfMonth(value).ToString("00");
            return Toshamsi;
        }
    }
}
