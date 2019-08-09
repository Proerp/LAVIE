using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Equin.ApplicationFramework;

using TotalModel;
using TotalBase.Enums;
using TotalDTO.Helpers;
using TotalDTO.Commons;
using TotalModel.Helpers;
using TotalBase;

namespace TotalDTO.Sales
{
    public class DeliveryAdvicePrimitiveDTO : QuantityDTO<DeliveryAdviceDetailDTO>, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.DeliveryAdvice; } }
        public override bool NoVoidable { get { return false; } }

        public override int GetID() { return this.DeliveryAdviceID; }
        public void SetID(int id) { this.DeliveryAdviceID = id; }

        private int deliveryAdviceID;
        [DefaultValue(0)]
        public int DeliveryAdviceID
        {
            get { return this.deliveryAdviceID; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, int>(ref this.deliveryAdviceID, o => o.DeliveryAdviceID, value); }
        }



        public bool HasSalesOrder { get; set; }

        private Nullable<int> salesOrderID;
        [DefaultValue(null)]
        public Nullable<int> SalesOrderID
        {
            get { return this.salesOrderID; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, Nullable<int>>(ref this.salesOrderID, o => o.SalesOrderID, value); }
        }
        [DefaultValue(null)]
        public Nullable<DateTime> SalesOrderEntryDate { get; set; }
        [DefaultValue(null)]
        public string SalesOrderReference { get; set; }
        [DefaultValue(null)]
        public string SalesOrderReferences { get; set; }

        private string voucherCode;
        [DefaultValue(null)]
        public string VoucherCode
        {
            get { return this.voucherCode; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, string>(ref this.voucherCode, o => o.VoucherCode, value); }
        }

        private int customerID;
        [DefaultValue(null)]
        public int CustomerID
        {
            get { return this.customerID; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, int>(ref this.customerID, o => o.CustomerID, value); }
        }
        private string customerName;
        [DefaultValue(null)]
        public string CustomerName
        {
            get { return this.customerName; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, string>(ref this.customerName, o => o.CustomerName, value, false); }
        }

        private int receiverID;
        [DefaultValue(null)]
        public int ReceiverID
        {
            get { return this.receiverID; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, int>(ref this.receiverID, o => o.ReceiverID, value); }
        }
        private string receiverName;
        [DefaultValue(null)]
        public string ReceiverName
        {
            get { return this.receiverName; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, string>(ref this.receiverName, o => o.ReceiverName, value, false); }
        }

        private string contactInfo;
        [DefaultValue(null)]
        public string ContactInfo
        {
            get { return this.contactInfo; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, string>(ref this.contactInfo, o => o.ContactInfo, value); }
        }

        private string shippingAddress;
        [DefaultValue(null)]
        public string ShippingAddress
        {
            get { return this.shippingAddress; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, string>(ref this.shippingAddress, o => o.ShippingAddress, value); }
        }

        private Nullable<int> salespersonID;
        [DefaultValue(null)]
        public Nullable<int> SalespersonID
        {
            get { return this.salespersonID; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, Nullable<int>>(ref this.salespersonID, o => o.SalespersonID, value); }
        }

        private int forkliftDriverID;
        [DefaultValue(null)]
        public int ForkliftDriverID
        {
            get { return this.forkliftDriverID; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, int>(ref this.forkliftDriverID, o => o.ForkliftDriverID, value); }
        }

        private int storekeeperID;
        //[DefaultValue(null)]
        public int StorekeeperID
        {
            get { return this.storekeeperID; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, int>(ref this.storekeeperID, o => o.StorekeeperID, value); }
        }

        private string vehicle;
        [DefaultValue(null)]
        public string Vehicle
        {
            get { return this.vehicle; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, string>(ref this.vehicle, o => o.Vehicle, value); }
        }

        private string vehicleDriver;
        [DefaultValue(null)]
        public string VehicleDriver
        {
            get { return this.vehicleDriver; }
            set { ApplyPropertyChange<DeliveryAdvicePrimitiveDTO, string>(ref this.vehicleDriver, o => o.VehicleDriver, value); }
        }


        public override string Caption
        {
            get { return (this.HasSalesOrder ? "Sales Order " + (this.SalesOrderID != null ? this.SalesOrderReference + ", on " + this.SalesOrderEntryDate.ToString() : this.SalesOrderReferences) + ", " : "") + "Customer: " + this.CustomerName + (this.CustomerName != "" ? ", " : "") + "DA Date: " + this.EntryDate.ToString() + "             Total Quantity: " + this.TotalQuantity.ToString("N0") + ",    Total Volume: " + this.TotalLineVolume.ToString("N2"); }
        }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();

            string salesOrderReferences = ""; string voucherCode = "";
            this.DtoDetails().ToList().ForEach(e => { e.CustomerID = this.CustomerID; e.ReceiverID = this.ReceiverID; if (this.HasSalesOrder && salesOrderReferences.IndexOf(e.SalesOrderReference) < 0) salesOrderReferences = salesOrderReferences + (salesOrderReferences != "" ? ", " : "") + e.SalesOrderReference; if (this.HasSalesOrder && e.VoucherCode != null && voucherCode.IndexOf(e.VoucherCode) < 0) voucherCode = voucherCode + (voucherCode != "" ? ", " : "") + e.VoucherCode; });
            this.SalesOrderReferences = salesOrderReferences;
            //if (this.HasSalesOrder) this.VoucherCode = voucherCode; REMOVE THIS, BECAUSE: SalesOrders.VoucherCode IS THE ORDER NUMBER, WHILE DeliveryAdvices.VoucherCode IS THE INVOICE NUMBER
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<DeliveryAdvicePrimitiveDTO>(p => p.CustomerID), "Vui lòng chọn khách hàng.", delegate { return (this.CustomerID != null && this.CustomerID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<DeliveryAdvicePrimitiveDTO>(p => p.ReceiverID), "Vui lòng chọn đơn vị nhận hàng.", delegate { return (this.ReceiverID != null && this.ReceiverID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<DeliveryAdvicePrimitiveDTO>(p => p.SalespersonID), "Vui lòng chọn nhân viên bán hàng.", delegate { return (this.SalespersonID != null && this.SalespersonID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<DeliveryAdvicePrimitiveDTO>(p => p.ForkliftDriverID), "Vui lòng chọn thủ kho.", delegate { return (this.ForkliftDriverID != null && this.ForkliftDriverID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<DeliveryAdvicePrimitiveDTO>(p => p.StorekeeperID), "Vui lòng chọn người lập.", delegate { return (this.StorekeeperID != null && this.StorekeeperID > 0); }));

            return validationRules;
        }
    }

    public class DeliveryAdviceDTO : DeliveryAdvicePrimitiveDTO, IBaseDetailEntity<DeliveryAdviceDetailDTO>
    {
        public DeliveryAdviceDTO()
        {
            this.DeliveryAdviceViewDetails = new BindingList<DeliveryAdviceDetailDTO>();
        }


        public BindingList<DeliveryAdviceDetailDTO> DeliveryAdviceViewDetails { get; set; }
        public BindingList<DeliveryAdviceDetailDTO> ViewDetails { get { return this.DeliveryAdviceViewDetails; } set { this.DeliveryAdviceViewDetails = value; } }

        public ICollection<DeliveryAdviceDetailDTO> GetDetails() { return this.DeliveryAdviceViewDetails; }

        protected override IEnumerable<DeliveryAdviceDetailDTO> DtoDetails() { return this.DeliveryAdviceViewDetails; }


        public bool HasOptionBatches
        {
            get { return this.DtoDetails().Where(w => w.BatchID != null).Count() > 0; }
        }
    }

}
