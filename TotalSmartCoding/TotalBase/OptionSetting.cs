using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TotalBase
{
    public class OptionSetting
    {
        private DateTime lowerFillterDate;
        private DateTime upperFillterDate;

        public OptionSetting() : this(DateTime.Today.AddDays(-10), DateTime.Today.AddDays(30)) { }

        public OptionSetting(DateTime lowerFillterDate, DateTime upperFillterDate)
        {
            this.LowerFillterDate = lowerFillterDate;
            this.UpperFillterDate = upperFillterDate;

            this.FromDate = DateTime.Today;
            this.FromDate = new DateTime(this.FromDate.Year, this.FromDate.Month, 1);
            this.ToDate = this.FromDate.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59).AddMilliseconds(999);
            //this.FromDate = this.FromDate.AddMonths(-1);
        }

        public DateTime LowerFillterDate
        {
            get { return this.lowerFillterDate; }
            set { this.lowerFillterDate = value; }
        }

        public DateTime UpperFillterDate
        {
            get { return this.upperFillterDate; }
            set { this.upperFillterDate = value; }
        }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
