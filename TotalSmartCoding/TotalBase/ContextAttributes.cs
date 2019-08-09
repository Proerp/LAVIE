using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalBase
{
    public static class ContextAttributes
    {
        public static UserInformation User;

        public static DateTime FromDate { get { return DateTime.Today.AddDays(-60); } }
        public static DateTime ToDate { get {return DateTime.Today.AddDays(2).AddHours(23).AddMinutes(59).AddSeconds(59);}}
    }
}
