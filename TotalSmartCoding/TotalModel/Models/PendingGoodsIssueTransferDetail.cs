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
    
    public partial class PendingGoodsIssueTransferDetail
    {
        public int GoodsIssueID { get; set; }
        public int GoodsIssueTransferDetailID { get; set; }
        public string PrimaryReference { get; set; }
        public System.DateTime PrimaryEntryDate { get; set; }
        public int CommodityID { get; set; }
        public string CommodityCode { get; set; }
        public string CommodityName { get; set; }
        public int BatchID { get; set; }
        public System.DateTime BatchEntryDate { get; set; }
        public int BinLocationID { get; set; }
        public string BinLocationCode { get; set; }
        public Nullable<int> PackID { get; set; }
        public string PackCode { get; set; }
        public Nullable<int> CartonID { get; set; }
        public string CartonCode { get; set; }
        public Nullable<int> PalletID { get; set; }
        public string PalletCode { get; set; }
        public Nullable<decimal> QuantityRemains { get; set; }
        public Nullable<decimal> Quantity { get; set; }
        public Nullable<decimal> LineVolumeRemains { get; set; }
        public decimal LineVolume { get; set; }
        public int PackCounts { get; set; }
        public int CartonCounts { get; set; }
        public int PalletCounts { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsSelected { get; set; }
    }
}
