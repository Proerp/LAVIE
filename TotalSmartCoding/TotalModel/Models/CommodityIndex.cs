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
    
    public partial class CommodityIndex
    {
        public int CommodityID { get; set; }
        public string CommodityCode { get; set; }
        public string CommodityName { get; set; }
        public int CommodityCategoryID { get; set; }
        public string CommodityCategoryName { get; set; }
        public string PackageSize { get; set; }
        public decimal PackageVolume { get; set; }
        public int PackPerCarton { get; set; }
        public int CartonPerPallet { get; set; }
        public int Shelflife { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> InActive { get; set; }
        public string CommodityAPICode { get; set; }
    }
}