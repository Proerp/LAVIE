using System;
using System.Collections.Generic;
using System.ComponentModel;
using TotalBase;
using TotalDTO.Inventories;
using TotalModel.Helpers;

namespace TotalDTO.Helpers.Interfaces
{
    public interface ILineDetailBinlLocation
    {
        event PropertyChangedEventHandler PropertyChanged;

        string BarcodeCode { get; }
        string CommodityCodeAndName { get; }

        int WarehouseID { get; set; }
        Nullable<int> BinLocationID { get; set; }
        string BinLocationCode { get; set; }
        string Caption { get; }

        decimal Quantity { get; set; }
        decimal LineVolume { get; set; }

        bool IsValid { get; }
    }


    public class LineDetailBinlLocation : QuantityDetailDTO, ILineDetailBinlLocation
    {
        public int WarehouseID { get; set; }

        private Nullable<int> binLocationID;
        [DefaultValue(null)]
        public Nullable<int> BinLocationID
        {
            get { return this.binLocationID; }
            set { ApplyPropertyChange<LineDetailBinlLocation, Nullable<int>>(ref this.binLocationID, o => o.BinLocationID, value); }
        }

        private string binLocationCode;
        [DefaultValue(null)]
        public virtual string BinLocationCode
        {
            get { return this.binLocationCode; }
            set { ApplyPropertyChange<LineDetailBinlLocation, string>(ref this.binLocationCode, o => o.BinLocationCode, value, false); }
        }

        public Nullable<int> PackID { get; set; }
        public string PackCode { get; set; }

        public Nullable<int> CartonID { get; set; }
        public string CartonCode { get; set; }

        public Nullable<int> PalletID { get; set; }
        public string PalletCode { get; set; }

        public string BarcodeCode { get { return this.PalletID != null ? this.PalletCode : (this.CartonID != null ? this.CartonCode : PackCode); } }

        public override string Caption
        {
            get { return this.CommodityCodeAndName + ",   Quantity: " + this.Quantity.ToString("N0") + ",   Volume: " + this.Volume.ToString("N2"); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LineDetailBinlLocation>(p => p.BinLocationID), "Vui lòng chọn bin location.", delegate { return (this.BinLocationID != null && this.BinLocationID > 0); }));

            return validationRules;
        }
    }

}
