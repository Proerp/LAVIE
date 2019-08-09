using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TotalBase;
using TotalBase.Enums;
using TotalModel;
using TotalModel.Helpers;

namespace TotalDTO.Helpers
{
    public interface IQuantityDetailDTO : IBaseModel
    {
        int CommodityID { get; set; }
        
        string CommodityCode { get; set; }
        string CommodityName { get; set; }

        string Unit { get; set; }
        string PackageSize { get; set; }        

        int CommodityTypeID { get; set; }

        decimal Volume { get; set; }
        decimal PackageVolume { get; set; }

        decimal Quantity { get; set; }
        decimal LineVolume { get; set; }
        

        int PackCounts { get; set; }
        int CartonCounts { get; set; }
        int PalletCounts { get; set; }
    }

    public abstract class QuantityDetailDTO : BaseModel, IQuantityDetailDTO, IBaseModel
    {
        public virtual int CommodityTypeID { get { return 1; } set { } } //NOT USED NOW. KEEP HERE FOR USE LATER

        private int commodityID;
        [DefaultValue(null)]
        public int CommodityID
        {
            get { return this.commodityID; }
            set { ApplyPropertyChange<QuantityDetailDTO, int>(ref this.commodityID, o => o.CommodityID, value); }
        }

        private string commodityCode;
        [DefaultValue(null)]
        public virtual string CommodityCode
        {
            get { return this.commodityCode; }
            set { ApplyPropertyChange<QuantityDetailDTO, string>(ref this.commodityCode, o => o.CommodityCode, value); }
        }

        private string commodityName;
        [DefaultValue(null)]
        public virtual string CommodityName
        {
            get { return this.commodityName; }
            set { ApplyPropertyChange<QuantityDetailDTO, string>(ref this.commodityName, o => o.CommodityName, value); }
        }

        public string CommodityCodeAndName { get { return this.CommodityCode + "-" + this.CommodityName; } }

        private string unit;
        [DefaultValue(null)]
        public virtual string Unit
        {
            get { return this.unit; }
            set { ApplyPropertyChange<QuantityDetailDTO, string>(ref this.unit, o => o.Unit, value); }
        }

        private string packageSize;
        [DefaultValue(null)]
        public virtual string PackageSize
        {
            get { return this.packageSize; }
            set { ApplyPropertyChange<QuantityDetailDTO, string>(ref this.packageSize, o => o.PackageSize, value); }
        }


        private decimal volume;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Volume không hợp lệ")]
        public virtual decimal Volume
        {
            get { return this.volume; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.volume, o => o.Volume, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }

        private decimal packageVolume;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "PackageVolume không hợp lệ")]
        public virtual decimal PackageVolume
        {
            get { return this.packageVolume; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.packageVolume, o => o.PackageVolume, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }








        private decimal quantityAvailable;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal QuantityAvailable
        {
            get { return this.quantityAvailable; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.quantityAvailable, o => o.QuantityAvailable, Math.Round(value, (int)GlobalEnums.rndQuantity)); }
        }

        private decimal lineVolumeAvailable;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "LineVolumeAvailable không hợp lệ")]
        public virtual decimal LineVolumeAvailable
        {
            get { return this.lineVolumeAvailable; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.lineVolumeAvailable, o => o.LineVolumeAvailable, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }



        private decimal quantityRemains;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal QuantityRemains
        {
            get { return this.quantityRemains; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.quantityRemains, o => o.QuantityRemains, Math.Round(value, (int)GlobalEnums.rndQuantity)); }
        }

        private decimal lineVolumeRemains;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "LineVolumeRemains không hợp lệ")]
        public virtual decimal LineVolumeRemains
        {
            get { return this.lineVolumeRemains; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.lineVolumeRemains, o => o.LineVolumeRemains, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }



        private decimal quantity;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal Quantity
        {
            get { return this.quantity; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.quantity, o => o.Quantity, Math.Round(value, (int)GlobalEnums.rndQuantity)); }
        }

        private decimal lineVolume;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "LineVolume không hợp lệ")]
        public virtual decimal LineVolume
        {
            get { return this.lineVolume; }
            set { ApplyPropertyChange<QuantityDetailDTO, decimal>(ref this.lineVolume, o => o.LineVolume, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }





        private int packCounts;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "PackCounts không hợp lệ")]
        public virtual int PackCounts
        {
            get { return this.packCounts; }
            set { ApplyPropertyChange<QuantityDetailDTO, int>(ref this.packCounts, o => o.PackCounts, value); }
        }

        private int cartonCounts;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "CartonCounts không hợp lệ")]
        public virtual int CartonCounts
        {
            get { return this.cartonCounts; }
            set { ApplyPropertyChange<QuantityDetailDTO, int>(ref this.cartonCounts, o => o.CartonCounts, value); }
        }

        private int palletCounts;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "PalletCounts không hợp lệ")]
        public virtual int PalletCounts
        {
            get { return this.palletCounts; }
            set { ApplyPropertyChange<QuantityDetailDTO, int>(ref this.palletCounts, o => o.PalletCounts, value); }
        }





        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<QuantityDetailDTO>(p => p.CommodityID), "Vui lòng chọn mặt hàng.", delegate { return (this.CommodityID > 0); }));

            return validationRules;

        }
    }
}
