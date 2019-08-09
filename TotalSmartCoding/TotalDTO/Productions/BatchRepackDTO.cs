using System;
using System.ComponentModel;

using TotalBase;

namespace TotalDTO.Productions
{
    public class BatchRepackDTO : BaseDTO
    {
        public int RepackID { get; set; }

        public int LineIndex { get; set; }

        public int SerialID { get; set; }

        public int PackID { get; set; }
        public int BatchID { get; set; }
        public string BatchCode { get; set; }
        public string LotCode { get; set; }

        public string Code { get; set; } //2B26N 026L 260218 12 30 09305 0 000019//2B26N026L2602181230093050000019
        public string dd { get { return Code.Substring(2, 2); } } //Production date
        public string HH { get { return Code.Substring(15, 2); } } //Production Hour
        public string mm { get { return Code.Substring(17, 2); } } //Production minute
        public string SerialNumber { get { return Code.Substring(Code.Length - 6, 6); } } //SerialNumber: LAST 6 DIGIT

        public int FillingLineID { get; set; }
        public string FillingLineCode { get; set; }

        public int CommodityID { get; set; }
        public string APICode { get; set; }
        public string CommodityName { get; set; }

        private int printedTimes;
        [DefaultValue(0)]
        public int PrintedTimes
        {
            get { return this.printedTimes; }
            set { ApplyPropertyChange<BatchRepackDTO, int>(ref this.printedTimes, o => o.PrintedTimes, value); }
        }

    }
}
