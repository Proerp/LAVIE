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
    public interface ICustomerBaseDTO
    {
        int CustomerID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên khách hàng")]
        [Display(Name = "Khách hàng")]
        string CodeAndName { get; }
        string Code { get; set; }
        string Name { get; set; }
        string OfficialName { get; set; }
        string VATCode { get; set; }
        string ContactInfo { get; set; }
        string Telephone { get; set; }
        string BillingAddress { get; set; }
        string ShippingAddress { get; set; }
        Nullable<int> TerritoryID { get; set; }
        Nullable<int> SalespersonID { get; set; }
    }

    public class CustomerBaseDTO : BaseDTO, ICustomerBaseDTO
    {
        private int customerID;
        [DefaultValue(0)]
        public int CustomerID
        {
            get { return this.customerID; }
            set { ApplyPropertyChange<CustomerBaseDTO, int>(ref this.customerID, o => o.CustomerID, value); }
        }


        public string CodeAndName { get { return this.Code + (this.Code != null && this.Code != "" && this.Name != null && this.Name != "" ? "  -  " : "") + this.Name; } }

        private string code;
        [DefaultValue(null)]
        public string Code
        {
            get { return this.code; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.code, o => o.Code, value); }
        }

        private string name;
        [DefaultValue(null)]
        public string Name
        {
            get { return this.name; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.name, o => o.Name, value); }
        }

        private string officialName;
        [DefaultValue(null)]
        public string OfficialName
        {
            get { return this.officialName; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.officialName, o => o.OfficialName, value); }
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

        private string telephone;
        [DefaultValue(null)]
        public string Telephone
        {
            get { return this.telephone; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.telephone, o => o.Telephone, value); }
        }

        private string email;
        [DefaultValue(null)]
        public string Email
        {
            get { return this.email; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.email, o => o.Email, value); }
        }

        private string billingAddress;
        [DefaultValue(null)]
        public string BillingAddress
        {
            get { return this.billingAddress; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.billingAddress, o => o.BillingAddress, value); }
        }

        private string shippingAddress;
        [DefaultValue(null)]
        public string ShippingAddress
        {
            get { return this.shippingAddress; }
            set { ApplyPropertyChange<CustomerBaseDTO, string>(ref this.shippingAddress, o => o.ShippingAddress, value); }
        }



        private Nullable<int> territoryID;
        [DefaultValue(null)]
        public Nullable<int> TerritoryID
        {
            get { return this.territoryID; }
            set { ApplyPropertyChange<CustomerBaseDTO, Nullable<int>>(ref this.territoryID, o => o.TerritoryID, value); }
        }

        private Nullable<int> salespersonID;
        [DefaultValue(null)]
        public Nullable<int> SalespersonID
        {
            get { return this.salespersonID; }
            set { ApplyPropertyChange<CustomerBaseDTO, Nullable<int>>(ref this.salespersonID, o => o.SalespersonID, value); }
        }
    }


    public class CustomerPrimitiveDTO : CustomerBaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Customer; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.CustomerID; }
        public void SetID(int id) { this.CustomerID = id; }


        private Nullable<int> customerCategoryID;
        [DefaultValue(null)]
        public Nullable<int> CustomerCategoryID
        {
            get { return this.customerCategoryID; }
            set { ApplyPropertyChange<CustomerPrimitiveDTO, Nullable<int>>(ref this.customerCategoryID, o => o.CustomerCategoryID, value); }
        }

        private Nullable<int> customerTypeID;
        [DefaultValue(null)]
        public Nullable<int> CustomerTypeID
        {
            get { return this.customerTypeID; }
            set { ApplyPropertyChange<CustomerPrimitiveDTO, Nullable<int>>(ref this.customerTypeID, o => o.CustomerTypeID, value); }
        }

        private string facsimile;
        [DefaultValue(null)]
        public string Facsimile
        {
            get { return this.facsimile; }
            set { ApplyPropertyChange<CustomerPrimitiveDTO, string>(ref this.facsimile, o => o.Facsimile, value); }
        }

        private string attentionName;
        [DefaultValue(null)]
        public string AttentionName
        {
            get { return this.attentionName; }
            set { ApplyPropertyChange<CustomerPrimitiveDTO, string>(ref this.attentionName, o => o.AttentionName, value); }
        }


        public bool IsCustomer { get { return true; } }
        public bool IsSupplier { get { return false; } }

        

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.Code), "Vui lòng nhập mã khách hàng.", delegate { return (this.Code != null && this.Code.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.Name), "Vui lòng nhập tên rút gọn thường gọi.", delegate { return (this.Name != null && this.Name.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.OfficialName), "Vui lòng nhập tên xuất hóa đơn.", delegate { return (this.OfficialName != null && this.OfficialName.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.BillingAddress), "Vui lòng nhập địa chỉ xuất hóa đơn.", delegate { return (this.BillingAddress != null && this.BillingAddress.Length > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.CustomerTypeID), "Vui lòng chọn loại khách hàng.", delegate { return (this.CustomerTypeID != null && this.CustomerTypeID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.CustomerCategoryID), "Vui lòng chọn kênh khách hàng.", delegate { return (this.CustomerCategoryID != null && this.CustomerCategoryID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.TerritoryID), "Vui lòng chọn địa bàn.", delegate { return (this.TerritoryID != null && this.TerritoryID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<CustomerPrimitiveDTO>(p => p.SalespersonID), "Vui lòng chọn nhân viên phụ trách khách hàng.", delegate { return (this.SalespersonID != null && this.SalespersonID > 0); }));
            return validationRules;

        }
    }

    public class CustomerDTO : CustomerPrimitiveDTO
    {
    }
}
