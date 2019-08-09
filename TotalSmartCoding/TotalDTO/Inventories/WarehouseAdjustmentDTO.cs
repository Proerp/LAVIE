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
    public class WarehouseAdjustmentPrimitiveDTO : QuantityDTO<WarehouseAdjustmentDetailDTO>, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.WarehouseAdjustment; } }

        public override int GetID() { return this.WarehouseAdjustmentID; }
        public void SetID(int id) { this.WarehouseAdjustmentID = id; }

        private int warehouseAdjustmentID;
        [DefaultValue(0)]
        public int WarehouseAdjustmentID
        {
            get { return this.warehouseAdjustmentID; }
            set { ApplyPropertyChange<WarehouseAdjustmentPrimitiveDTO, int>(ref this.warehouseAdjustmentID, o => o.WarehouseAdjustmentID, value); }
        }




        private int warehouseID;
        [DefaultValue(null)]
        public int WarehouseID
        {
            get { return this.warehouseID; }
            set
            {
                ApplyPropertyChange<WarehouseAdjustmentPrimitiveDTO, int>(ref this.warehouseID, o => o.WarehouseID, value);
                if (!this.WarehouseReceiptEnabled) this.WarehouseReceiptID = this.WarehouseID;
            }
        }
        private string warehouseName;
        [DefaultValue(null)]
        public string WarehouseName
        {
            get { return this.warehouseName; }
            set { ApplyPropertyChange<WarehouseAdjustmentPrimitiveDTO, string>(ref this.warehouseName, o => o.WarehouseName, value); }
        }

        private Nullable<int> warehouseReceiptID;
        [DefaultValue(null)]
        public Nullable<int> WarehouseReceiptID
        {
            get { return this.warehouseReceiptID; }
            set { ApplyPropertyChange<WarehouseAdjustmentPrimitiveDTO, Nullable<int>>(ref this.warehouseReceiptID, o => o.WarehouseReceiptID, value); }
        }
        private string warehouseReceiptName;
        [DefaultValue(null)]
        public string WarehouseReceiptName
        {
            get { return this.warehouseReceiptName; }
            set { ApplyPropertyChange<WarehouseAdjustmentPrimitiveDTO, string>(ref this.warehouseReceiptName, o => o.WarehouseReceiptName, value, false); }
        }


        private int warehouseAdjustmentTypeID;
        [DefaultValue(null)]
        public int WarehouseAdjustmentTypeID
        {
            get { return this.warehouseAdjustmentTypeID; }
            set
            {
                ApplyPropertyChange<WarehouseAdjustmentPrimitiveDTO, int>(ref this.warehouseAdjustmentTypeID, o => o.WarehouseAdjustmentTypeID, value);
                if (!this.WarehouseReceiptEnabled) this.WarehouseReceiptID = this.WarehouseID;
            }
        }
        private string warehouseAdjustmentTypeName;
        [DefaultValue(null)]
        public string WarehouseAdjustmentTypeName
        {
            get { return this.warehouseAdjustmentTypeName; }
            set { ApplyPropertyChange<WarehouseAdjustmentDTO, string>(ref this.warehouseAdjustmentTypeName, o => o.WarehouseAdjustmentTypeName, value); }
        }

        public bool WarehouseReceiptEnabled { get { return this.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.HoldUnHold; } }

        private string adjustmentJobs;
        [DefaultValue(null)]
        public string AdjustmentJobs
        {
            get { return this.adjustmentJobs; }
            set { ApplyPropertyChange<WarehouseAdjustmentDTO, string>(ref this.adjustmentJobs, o => o.AdjustmentJobs, value); }
        }

        private int storekeeperID;
        [DefaultValue(1)]
        public int StorekeeperID
        {
            get { return this.storekeeperID; }
            set { ApplyPropertyChange<WarehouseAdjustmentPrimitiveDTO, int>(ref this.storekeeperID, o => o.StorekeeperID, value); }
        }




        public bool HasPositiveLine { get { return this.DtoDetails().Where(w => w.Quantity > 0).Count() > 0; } }

        public override string Caption
        {
            get { return this.WarehouseAdjustmentTypeName + "             Issue at: " + this.WarehouseName + (this.WarehouseReceiptEnabled ? ", Receipt at: " + this.WarehouseReceiptName : "") + "             Total Quantity: " + this.TotalQuantity.ToString("N0") + ",    Total Volume: " + this.TotalLineVolume.ToString("N2"); }
        }

        public override void PerformPresaveRule()
        {
            base.PerformPresaveRule();

            this.DtoDetails().ToList().ForEach(e => { e.WarehouseID = this.WarehouseID; e.WarehouseReceiptID = this.WarehouseReceiptID; e.WarehouseAdjustmentTypeID = this.WarehouseAdjustmentTypeID; });
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehouseAdjustmentPrimitiveDTO>(p => p.WarehouseID), "Vui lòng chọn kho.", delegate { return (this.WarehouseID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehouseAdjustmentPrimitiveDTO>(p => p.WarehouseReceiptID), "Vui lòng chọn kho nhập.", delegate { return (this.WarehouseReceiptID != null && this.WarehouseReceiptID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehouseAdjustmentPrimitiveDTO>(p => p.WarehouseAdjustmentTypeID), "Vui lòng chọn tài xế.", delegate { return (this.WarehouseAdjustmentTypeID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehouseAdjustmentPrimitiveDTO>(p => p.StorekeeperID), "Vui lòng chọn nhân viên kho.", delegate { return (this.StorekeeperID > 0); }));

            return validationRules;

        }

    }

    public class WarehouseAdjustmentDTO : WarehouseAdjustmentPrimitiveDTO, IBaseDetailEntity<WarehouseAdjustmentDetailDTO>
    {
        public WarehouseAdjustmentDTO()
        {
            this.WarehouseAdjustmentViewDetails = new BindingList<WarehouseAdjustmentDetailDTO>();






            this.PositivePackDetails = new BindingListView<WarehouseAdjustmentDetailDTO>(this.WarehouseAdjustmentViewDetails);
            this.PositiveCartonDetails = new BindingListView<WarehouseAdjustmentDetailDTO>(this.WarehouseAdjustmentViewDetails);
            this.PositivePalletDetails = new BindingListView<WarehouseAdjustmentDetailDTO>(this.WarehouseAdjustmentViewDetails);

            this.NegativePackDetails = new BindingListView<WarehouseAdjustmentDetailDTO>(this.WarehouseAdjustmentViewDetails);
            this.NegativeCartonDetails = new BindingListView<WarehouseAdjustmentDetailDTO>(this.WarehouseAdjustmentViewDetails);
            this.NegativePalletDetails = new BindingListView<WarehouseAdjustmentDetailDTO>(this.WarehouseAdjustmentViewDetails);

            this.PositivePackDetails.ApplyFilter(f => f.PackID != null && f.Quantity >= 0);
            this.PositiveCartonDetails.ApplyFilter(f => f.CartonID != null && f.Quantity >= 0);
            this.PositivePalletDetails.ApplyFilter(f => f.PalletID != null && f.Quantity >= 0);

            this.NegativePackDetails.ApplyFilter(f => f.PackID != null && f.Quantity < 0);
            this.NegativeCartonDetails.ApplyFilter(f => f.CartonID != null && f.Quantity < 0);
            this.NegativePalletDetails.ApplyFilter(f => f.PalletID != null && f.Quantity < 0);
        }


        public BindingList<WarehouseAdjustmentDetailDTO> WarehouseAdjustmentViewDetails { get; set; }
        public BindingList<WarehouseAdjustmentDetailDTO> ViewDetails { get { return this.WarehouseAdjustmentViewDetails; } set { this.WarehouseAdjustmentViewDetails = value; } }

        public ICollection<WarehouseAdjustmentDetailDTO> GetDetails() { return this.WarehouseAdjustmentViewDetails; }

        protected override IEnumerable<WarehouseAdjustmentDetailDTO> DtoDetails() { return this.WarehouseAdjustmentViewDetails; }






        public BindingListView<WarehouseAdjustmentDetailDTO> PositivePackDetails { get; private set; }
        public BindingListView<WarehouseAdjustmentDetailDTO> PositiveCartonDetails { get; private set; }
        public BindingListView<WarehouseAdjustmentDetailDTO> PositivePalletDetails { get; private set; }

        public BindingListView<WarehouseAdjustmentDetailDTO> NegativePackDetails { get; private set; }
        public BindingListView<WarehouseAdjustmentDetailDTO> NegativeCartonDetails { get; private set; }
        public BindingListView<WarehouseAdjustmentDetailDTO> NegativePalletDetails { get; private set; }
    }

}
