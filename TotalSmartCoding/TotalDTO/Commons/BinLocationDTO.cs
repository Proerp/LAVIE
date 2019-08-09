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
    public class BinLocationPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.BinLocation; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.BinLocationID; }
        public void SetID(int id) { this.BinLocationID = id; }


        private int binLocationID;
        [DefaultValue(0)]
        public int BinLocationID
        {
            get { return this.binLocationID; }
            set { ApplyPropertyChange<BinLocationPrimitiveDTO, int>(ref this.binLocationID, o => o.BinLocationID, value); }
        }


        private string code;
        [DefaultValue(null)]
        public string Code
        {
            get { return this.code; }
            set { ApplyPropertyChange<BinLocationPrimitiveDTO, string>(ref this.code, o => o.Code, value); }
        }

        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<BinLocationPrimitiveDTO, string>(ref this.name, o => o.Name, value); }
        }

        private Nullable<int> warehouseID;
        [DefaultValue(null)]
        public Nullable<int> WarehouseID
        {
            get { return this.warehouseID; }
            set { ApplyPropertyChange<BinLocationPrimitiveDTO, Nullable<int>>(ref this.warehouseID, o => o.WarehouseID, value); }
        }

        private string vatCode;
        [DefaultValue(null)]
        public string VATCode
        {
            get { return this.vatCode; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.vatCode, o => o.VATCode, value); }
        }

        private string contactInfo;
        [DefaultValue(null)]
        public string ContactInfo
        {
            get { return this.contactInfo; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.contactInfo, o => o.ContactInfo, value); }
        }


        private string attentionName;
        [DefaultValue(null)]
        public string AttentionName
        {
            get { return this.attentionName; }
            set { ApplyPropertyChange<CustomerPrimitiveDTO, string>(ref this.attentionName, o => o.AttentionName, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BinLocationPrimitiveDTO>(p => p.Code), "Vui lòng nhập mã khách hàng.", delegate { return (this.Code != null && this.Code.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BinLocationPrimitiveDTO>(p => p.Name), "Vui lòng nhập tên rút gọn thường gọi.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BinLocationPrimitiveDTO>(p => p.WarehouseID), "Vui lòng chọn loại khách hàng.", delegate { return (this.WarehouseID != null && this.WarehouseID > 0); }));
            return validationRules;
        }
    }

    public class BinLocationDTO : BinLocationPrimitiveDTO
    {
    }
}
