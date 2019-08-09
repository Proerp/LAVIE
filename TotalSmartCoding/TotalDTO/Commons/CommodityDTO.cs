using System;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using TotalModel;
using TotalBase.Enums;
using TotalModel.Helpers;
using TotalBase;

namespace TotalDTO.Commons
{
    public class CommodityPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Commodity; } }
        public override bool NoApprovable { get { return true; } }

        public CommodityPrimitiveDTO() { this.Initialize(); }

        public override void Init()
        {
            base.Init();
            this.Initialize();
        }

        private void Initialize() { this.Unit = "Lon"; this.Volume = 0; this.PackPerCarton = 0; this.CartonPerPallet = 0; this.Shelflife = 0; }


        public override int GetID() { return this.CommodityID; }
        public void SetID(int id) { this.CommodityID = id; }

        private int commodityID;
        [DefaultValue(0)]
        public int CommodityID
        {
            get { return this.commodityID; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, int>(ref this.commodityID, o => o.CommodityID, value); }
        }

        private string code;
        [DefaultValue(null)]
        public string Code
        {
            get { return this.code; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.code, o => o.Code, value); }
        }

        public string OfficialCode { get { return TotalBase.CommonExpressions.AlphaNumericString(this.Code); } }

        //private string officialCode;
        //[DefaultValue(null)]
        //public string OfficialCode
        //{
        //    get { return this.officialCode; }
        //    set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.officialCode, o => o.OfficialCode, value); }
        //}

        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.name, o => o.Name, value);
            ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.officialName, o => o.OfficialName, value); 
            }
        }

        private string officialName;
        [DefaultValue(null)]
        public string OfficialName
        {
            get { return this.officialName; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.officialName, o => o.OfficialName, value); }
        }

        private Nullable<int> commodityTypeID;
        [DefaultValue(1)] //DEFAULT 1, NOT IMPLEMET HERE
        public Nullable<int> CommodityTypeID
        {
            get { return this.commodityTypeID; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, Nullable<int>>(ref this.commodityTypeID, o => o.CommodityTypeID, value); }
        }

        private Nullable<int> commodityCategoryID;
        [DefaultValue(2)]
        public Nullable<int> CommodityCategoryID
        {
            get { return this.commodityCategoryID; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, Nullable<int>>(ref this.commodityCategoryID, o => o.CommodityCategoryID, value); }
        }


        private string unit;
        [DefaultValue(null)]
        public string Unit
        {
            get { return this.unit; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.unit, o => o.Unit, value); }
        }

        private string packageSize;
        [DefaultValue(null)]
        public string PackageSize
        {
            get { return this.packageSize; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.packageSize, o => o.PackageSize, value); }
        }

        private string origin;
        [DefaultValue(null)]
        public string Origin
        {
            get { return this.origin; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.origin, o => o.Origin, value); }
        }

        private string apiCode;
        [DefaultValue(null)]
        public string APICode
        {
            get { return this.apiCode; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.apiCode, o => o.APICode, value); }
        }

        private string cartonCode;
        [DefaultValue(null)]
        public string CartonCode
        {
            get { return this.cartonCode; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.cartonCode, o => o.CartonCode, value); }
        }

        private string fillingLineIDs;
        [DefaultValue("0")]
        public string FillingLineIDs
        {
            get { return this.fillingLineIDs; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, string>(ref this.fillingLineIDs, o => o.FillingLineIDs, value); }
        }

        private decimal volume;
        //[DefaultValue(0.0)]
        [Range(1, 99999999999, ErrorMessage = "Volume không hợp lệ")]
        public virtual decimal Volume
        {
            get { return this.volume; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, decimal>(ref this.volume, o => o.Volume, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }

        public decimal PackageVolume { get { return Math.Round(this.Volume * this.PackPerCarton, (int)GlobalEnums.rndVolume); } }

        //private decimal packageVolume;
        ////[DefaultValue(0.0)]
        //[Range(1, 99999999999, ErrorMessage = "PackageVolume không hợp lệ")]
        //public virtual decimal PackageVolume
        //{
        //    get { return this.packageVolume; }
        //    set { ApplyPropertyChange<CommodityPrimitiveDTO, decimal>(ref this.packageVolume, o => o.PackageVolume, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        //}

        private decimal weight;
        //[DefaultValue(0.0)]
        [Range(1, 99999999999, ErrorMessage = "Weight không hợp lệ")]
        public virtual decimal Weight
        {
            get { return this.weight; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, decimal>(ref this.weight, o => o.Weight, Math.Round(value, (int)GlobalEnums.rndWeight)); }
        }


        private int packPerCarton;
        [DefaultValue(0)]
        [Range(1, 99999999999, ErrorMessage = "Pack per carton không hợp lệ")]
        public virtual int PackPerCarton
        {
            get { return this.packPerCarton; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, int>(ref this.packPerCarton, o => o.PackPerCarton, value); }
        }

        private int cartonPerPallet;
        [DefaultValue(0)]
        [Range(1, 99999999999, ErrorMessage = "Carton per pallet không hợp lệ")]
        public virtual int CartonPerPallet
        {
            get { return this.cartonPerPallet; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, int>(ref this.cartonPerPallet, o => o.CartonPerPallet, value); }
        }


        private int shelflife;
        [DefaultValue(0)]
        [Range(1, 99999999999, ErrorMessage = "Shelflife không hợp lệ")]
        public virtual int Shelflife
        {
            get { return this.shelflife; }
            set { ApplyPropertyChange<CommodityPrimitiveDTO, int>(ref this.shelflife, o => o.Shelflife, value); }
        }

        public Nullable<bool> Discontinue { get; set; }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.Code), "Vui lòng nhập mã mặt hàng.", delegate { return (this.Code != null && this.Code.Trim().Length == 10); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.APICode), "Vui lòng nhập mã chuyễn đổi.", delegate { return (this.APICode != null && this.APICode.Trim().Length == 3); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.CartonCode), "Vui lòng nhập mã carton.", delegate { return (this.CartonCode != null && this.CartonCode.Trim().Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.Name), "Vui lòng nhập tên mặt hàng.", delegate { return (this.Name != null && this.Name.Trim() != ""); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.OfficialName), "Vui lòng nhập tên đầy đủ.", delegate { return (this.OfficialName != null && this.OfficialName.Trim() != ""); }));
            
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.CommodityCategoryID), "Vui lòng chọn phân loại mặt hàng.", delegate { return (this.CommodityCategoryID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.PackageSize), "Vui lòng nhập quy cách đóng gói.", delegate { return (this.PackageSize != null && this.PackageSize.Trim() != ""); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.FillingLineIDs), "Vui lòng nhập mã dây chuyền đóng gói.", delegate { return (this.FillingLineIDs != null && this.FillingLineIDs.Trim() != ""); }));

            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.Volume), "Vui lòng nhập volume.", delegate { return (this.Volume > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.PackageVolume), "Vui lòng nhập volume.", delegate { return (this.PackageVolume > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.PackPerCarton), "Vui lòng nhập volume.", delegate { return (this.PackPerCarton > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CommodityPrimitiveDTO>(p => p.CartonPerPallet), "Vui lòng nhập volume.", delegate { return (this.CartonPerPallet > 0); }));

            return validationRules;
        }
    }

    public class CommodityDTO : CommodityPrimitiveDTO
    {
    }
}
