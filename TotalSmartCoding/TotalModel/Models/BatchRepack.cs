//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TotalModel.Models
{
    using System;
    
    public partial class BatchRepack
    {
        public int PackID { get; set; }
        public int BatchID { get; set; }
        public string LotCode { get; set; }
        public int FillingLineID { get; set; }
        public string BatchCode { get; set; }
        public int PrintedTimes { get; set; }
        public string Code { get; set; }
        public System.DateTime EntryDate { get; set; }
        public string FillingLineCode { get; set; }
        public int RepackID { get; set; }
        public string APICode { get; set; }
        public string CommodityName { get; set; }
        public int CommodityID { get; set; }
        public int SerialID { get; set; }
    }
}
