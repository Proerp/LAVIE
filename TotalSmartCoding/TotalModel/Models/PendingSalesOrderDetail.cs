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
    
    public partial class PendingSalesOrderDetail
    {
        public int SalesOrderID { get; set; }
        public int SalesOrderDetailID { get; set; }
        public string SalesOrderReference { get; set; }
        public System.DateTime SalesOrderEntryDate { get; set; }
        public int CommodityID { get; set; }
        public string CommodityCode { get; set; }
        public string CommodityName { get; set; }
        public decimal QuantityRemains { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public decimal LineVolumeRemains { get; set; }
        public Nullable<decimal> LineVolume { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsSelected { get; set; }
        public string PackageSize { get; set; }
        public decimal Volume { get; set; }
        public decimal PackageVolume { get; set; }
        public decimal QuantityAvailable { get; set; }
        public decimal LineVolumeAvailable { get; set; }
        public string SalesOrderVoucherCode { get; set; }
    }
}
