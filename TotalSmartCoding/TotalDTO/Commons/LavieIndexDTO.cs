using System;
using System.ComponentModel;

using TotalBase;

namespace TotalDTO.Commons
{
    public class LavieIndexDTO : BaseDTO
    {
        public int LavieID { get; set; }
        public int SerialID { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string ItemNumber { get; set; }
        public string ProductName { get; set; }
        public string GTIN { get; set; }
        public string BatchNumber { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public Nullable<decimal> Qty { get; set; }
        public Nullable<decimal> Layers { get; set; }
        public string GTINBarcode { get; set; }
        public string Barcode { get; set; }
        public string Remarks { get; set; }
        public string PalletID { get; set; }

        public int LineIndex { get; set; }

        private int printedTimes;
        [DefaultValue(0)]
        public int PrintedTimes
        {
            get { return this.printedTimes; }
            set { ApplyPropertyChange<LavieIndexDTO, int>(ref this.printedTimes, o => o.PrintedTimes, value); }
        }

    }

}
