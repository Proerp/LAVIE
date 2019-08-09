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

namespace TotalDTO.Inventories
{
    public class GoodsIssuePrimitiveDTO : QuantityDTO<GoodsIssueDetailDTO>, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.GoodsIssue; } }

        public override bool Printable { get { return true; } }
        public override bool PrintVisible { get { return true; } }

        public GoodsIssuePrimitiveDTO() { this.Initialize(); }

        public override void Init()
        {
            base.Init();
            this.Initialize();
        }

        private void Initialize() { this.LoadingStart = DateTime.Now; this.LoadingCompletion = DateTime.Now; }

        public override int GetID() { return this.GoodsIssueID; }
        public void SetID(int id) { this.GoodsIssueID = id; }

        private int goodsIssueID;
        [DefaultValue(0)]
        public int GoodsIssueID
        {
            get { return this.goodsIssueID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, int>(ref this.goodsIssueID, o => o.GoodsIssueID, value); }
        }

        private Nullable<int> goodsIssueTypeID;
        [DefaultValue(null)]
        public Nullable<int> GoodsIssueTypeID
        {
            get { return this.goodsIssueTypeID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<int>>(ref this.goodsIssueTypeID, o => o.GoodsIssueTypeID, value); }
        }
        public string GoodsIssueTypeName { get; set; }

        private Nullable<int> deliveryAdviceID;
        [DefaultValue(null)]
        public Nullable<int> DeliveryAdviceID
        {
            get { return this.deliveryAdviceID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<int>>(ref this.deliveryAdviceID, o => o.DeliveryAdviceID, value); }
        }
        [DefaultValue(null)]
        public Nullable<DateTime> DeliveryAdviceEntryDate { get; set; }
        [DefaultValue(null)]
        public string DeliveryAdviceReference { get; set; }



        private Nullable<int> transferOrderID;
        [DefaultValue(null)]
        public Nullable<int> TransferOrderID
        {
            get { return this.transferOrderID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<int>>(ref this.transferOrderID, o => o.TransferOrderID, value); }
        }
        [DefaultValue(null)]
        public Nullable<DateTime> TransferOrderEntryDate { get; set; }
        [DefaultValue(null)]
        public string TransferOrderReference { get; set; }


        [DefaultValue(null)]
        public string PrimaryReferences { get; set; }

        private string voucherCodes;
        [DefaultValue(null)]
        public string VoucherCodes
        {
            get { return this.voucherCodes; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, string>(ref this.voucherCodes, o => o.VoucherCodes, value); }
        }

        private Nullable<int> customerID;
        [DefaultValue(null)]
        public Nullable<int> CustomerID
        {
            get { return this.customerID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<int>>(ref this.customerID, o => o.CustomerID, value); }
        }
        private string customerName;
        [DefaultValue(null)]
        public string CustomerName
        {
            get { return this.customerName; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, string>(ref this.customerName, o => o.CustomerName, value); }
        }


        private Nullable<int> receiverID;
        [DefaultValue(null)]
        public Nullable<int> ReceiverID
        {
            get { return this.receiverID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<int>>(ref this.receiverID, o => o.ReceiverID, value); }
        }
        private string receiverName;
        [DefaultValue(null)]
        public string ReceiverName
        {
            get { return this.receiverName; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, string>(ref this.receiverName, o => o.ReceiverName, value); }
        }

        private Nullable<int> warehouseID;
        [DefaultValue(null)]
        public Nullable<int> WarehouseID
        {
            get { return this.warehouseID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<int>>(ref this.warehouseID, o => o.WarehouseID, value); }
        }
        private string warehouseName;
        [DefaultValue(null)]
        public string WarehouseName
        {
            get { return this.warehouseName; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, string>(ref this.warehouseName, o => o.WarehouseName, value); }
        }

        private Nullable<int> warehouseReceiptID;
        [DefaultValue(null)]
        public Nullable<int> WarehouseReceiptID
        {
            get { return this.warehouseReceiptID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<int>>(ref this.warehouseReceiptID, o => o.WarehouseReceiptID, value); }
        }
        private string warehouseReceiptName;
        [DefaultValue(null)]
        public string WarehouseReceiptName
        {
            get { return this.warehouseReceiptName; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, string>(ref this.warehouseReceiptName, o => o.WarehouseReceiptName, value); }
        }


        private int forkliftDriverID;
        [DefaultValue(null)]
        public int ForkliftDriverID
        {
            get { return this.forkliftDriverID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, int>(ref this.forkliftDriverID, o => o.ForkliftDriverID, value); }
        }

        private int storekeeperID;
        [DefaultValue(null)]
        public int StorekeeperID
        {
            get { return this.storekeeperID; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, int>(ref this.storekeeperID, o => o.StorekeeperID, value); }
        }

        private string vehicle;
        [DefaultValue(null)]
        public string Vehicle
        {
            get { return this.vehicle; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, string>(ref this.vehicle, o => o.Vehicle, value); }
        }

        private string vehicleDriver;
        [DefaultValue(null)]
        public string VehicleDriver
        {
            get { return this.vehicleDriver; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, string>(ref this.vehicleDriver, o => o.VehicleDriver, value); }
        }

        private Nullable<DateTime> loadingStart;
        public Nullable<DateTime> LoadingStart
        {
            get { return this.loadingStart; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<DateTime>>(ref this.loadingStart, o => o.LoadingStart, value); }
        }

        private Nullable<DateTime> loadingCompletion;
        public Nullable<DateTime> LoadingCompletion
        {
            get { return this.loadingCompletion; }
            set { ApplyPropertyChange<GoodsIssuePrimitiveDTO, Nullable<DateTime>>(ref this.loadingCompletion, o => o.LoadingCompletion, value); }
        }

        public override string Caption
        {
            get { return this.VoucherCodes + " " + (this.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice ? "D.A: " : "Order: ") + (this.DeliveryAdviceID != null ? this.DeliveryAdviceReference : (this.TransferOrderID != null ? this.TransferOrderReference : this.PrimaryReferences)) + ", " + (this.CustomerName != null ? "Customer: " + this.CustomerName.Substring(0, this.CustomerName.Length > 16 ? 15 : this.CustomerName.Length) : "") + (this.WarehouseName != null && this.WarehouseReceiptName != null ? "From: " + this.WarehouseName + " To: " + this.WarehouseReceiptName : "") + "             Total Quantity: " + this.TotalQuantity.ToString("N0") + ",    Total Volume: " + this.TotalLineVolume.ToString("N2"); }
        }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();

            string primaryReferences = ""; //string voucherCodes = "";
            this.DtoDetails().ToList().ForEach(e => { e.GoodsIssueTypeID = this.GoodsIssueTypeID; e.CustomerID = this.CustomerID; e.ReceiverID = this.ReceiverID; e.WarehouseReceiptID = this.WarehouseReceiptID; if (primaryReferences.IndexOf(e.PrimaryReference) < 0) primaryReferences = primaryReferences + (primaryReferences != "" ? ", " : "") + e.PrimaryReference; }); //if (voucherCodes.IndexOf(e.VoucherCode) < 0) voucherCodes = voucherCodes + (voucherCodes != "" ? ", " : "") + e.VoucherCode; 
            this.PrimaryReferences = primaryReferences; //this.VoucherCodes = voucherCodes;
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<GoodsIssuePrimitiveDTO>(p => p.CustomerID), "Vui lòng chọn khách hàng.", delegate { return (this.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.TransferOrder || (this.CustomerID != null && this.CustomerID > 0)); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<GoodsIssuePrimitiveDTO>(p => p.ReceiverID), "Vui lòng chọn đơn vị nhận hàng.", delegate { return (this.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.TransferOrder || (this.ReceiverID != null && this.ReceiverID > 0)); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<GoodsIssuePrimitiveDTO>(p => p.WarehouseID), "Vui lòng chọn kho xuất.", delegate { return (this.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice || (this.WarehouseID != null && this.WarehouseID > 0)); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<GoodsIssuePrimitiveDTO>(p => p.WarehouseReceiptID), "Vui lòng chọn kho nhận.", delegate { return (this.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice || (this.WarehouseReceiptID != null && this.WarehouseReceiptID > 0)); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<GoodsIssuePrimitiveDTO>(p => p.ForkliftDriverID), "Vui lòng chọn thủ kho.", delegate { return (this.ForkliftDriverID != null && this.ForkliftDriverID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<GoodsIssuePrimitiveDTO>(p => p.StorekeeperID), "Vui lòng chọn người lập.", delegate { return (this.StorekeeperID != null && this.StorekeeperID > 0); }));

            return validationRules;
        }
    }

    public class GoodsIssueDTO : GoodsIssuePrimitiveDTO, IBaseDetailEntity<GoodsIssueDetailDTO>
    {
        public GoodsIssueDTO()
        {
            this.GoodsIssueViewDetails = new BindingList<GoodsIssueDetailDTO>();






            this.PackDetails = new BindingListView<GoodsIssueDetailDTO>(this.GoodsIssueViewDetails);
            this.CartonDetails = new BindingListView<GoodsIssueDetailDTO>(this.GoodsIssueViewDetails);
            this.PalletDetails = new BindingListView<GoodsIssueDetailDTO>(this.GoodsIssueViewDetails);

            this.PackDetails.ApplyFilter(f => f.PackID != null);
            this.CartonDetails.ApplyFilter(f => f.CartonID != null);
            this.PalletDetails.ApplyFilter(f => f.PalletID != null);
        }


        public BindingList<GoodsIssueDetailDTO> GoodsIssueViewDetails { get; set; }
        public BindingList<GoodsIssueDetailDTO> ViewDetails { get { return this.GoodsIssueViewDetails; } set { this.GoodsIssueViewDetails = value; } }

        public ICollection<GoodsIssueDetailDTO> GetDetails() { return this.GoodsIssueViewDetails; }

        protected override IEnumerable<GoodsIssueDetailDTO> DtoDetails() { return this.GoodsIssueViewDetails; }






        public BindingListView<GoodsIssueDetailDTO> PackDetails { get; private set; }
        public BindingListView<GoodsIssueDetailDTO> CartonDetails { get; private set; }
        public BindingListView<GoodsIssueDetailDTO> PalletDetails { get; private set; }
    }

}
