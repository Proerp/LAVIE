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
    public class PickupPrimitiveDTO : QuantityDTO<PickupDetailDTO>, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Pickup; } }

        public override bool Printable { get { return true; } }
        public override bool PrintVisible { get { return true; } }

        public override int GetID() { return this.PickupID; }
        public void SetID(int id) { this.PickupID = id; }

        private int pickupID;
        [DefaultValue(0)]
        public int PickupID
        {
            get { return this.pickupID; }
            set { ApplyPropertyChange<PickupPrimitiveDTO, int>(ref this.pickupID, o => o.PickupID, value); }
        }


        private Nullable<int> fillingLineID;
        [DefaultValue(null)]
        public Nullable<int> FillingLineID
        {
            get { return this.fillingLineID; }
            set { ApplyPropertyChange<PickupPrimitiveDTO, Nullable<int>>(ref this.fillingLineID, o => o.FillingLineID, value); }
        }
        private string fillingLineName;
        [DefaultValue(null)]
        public string FillingLineName
        {
            get { return this.fillingLineName; }
            set { ApplyPropertyChange<PickupDTO, string>(ref this.fillingLineName, o => o.FillingLineName, value, false); }
        }
        private string fillingLineNickName;
        [DefaultValue(null)]
        public string FillingLineNickName
        {
            get { return this.fillingLineNickName; }
            set { ApplyPropertyChange<PickupDTO, string>(ref this.fillingLineNickName, o => o.FillingLineNickName, value, false); }
        }


        private Nullable<int> warehouseID;
        [DefaultValue(null)]
        public Nullable<int> WarehouseID
        {
            get { return this.warehouseID; }
            set { ApplyPropertyChange<PickupPrimitiveDTO, Nullable<int>>(ref this.warehouseID, o => o.WarehouseID, value); }
        }
        private string warehouseName;
        [DefaultValue(null)]
        public string WarehouseName
        {
            get { return this.warehouseName; }
            set { ApplyPropertyChange<PickupDTO, string>(ref this.warehouseName, o => o.WarehouseName, value, false); }
        }



        private Nullable<int> forkliftDriverID;
        [DefaultValue(null)]
        public Nullable<int> ForkliftDriverID
        {
            get { return this.forkliftDriverID; }
            set { ApplyPropertyChange<PickupPrimitiveDTO, Nullable<int>>(ref this.forkliftDriverID, o => o.ForkliftDriverID, value); }
        }

        private Nullable<int> storekeeperID;
        [DefaultValue(null)]
        public Nullable<int> StorekeeperID
        {
            get { return this.storekeeperID; }
            set { ApplyPropertyChange<PickupPrimitiveDTO, Nullable<int>>(ref this.storekeeperID, o => o.StorekeeperID, value); }
        }




        public override string Caption
        {
            get { return this.fillingLineName + ", " + this.WarehouseName + "   Pallet: " + this.TotalPalletCounts.ToString() + ",   Quantity: " + this.TotalQuantity.ToString("N0") + ",   Volume: " + this.TotalLineVolume.ToString("N2"); }
        }

        public override void PerformPresaveRule()
        {
            if (this.DtoDetails().Count() > 0)
                this.EntryDate = this.DtoDetails().Max(d => d.BarcodeEntryDate).Value;

            base.PerformPresaveRule();

            this.DtoDetails().ToList().ForEach(e => { e.WarehouseID = (int)this.WarehouseID; });
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<PickupPrimitiveDTO>(p => p.FillingLineID), "Vui lòng chọn chuyền.", delegate { return (this.FillingLineID != null && this.FillingLineID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<PickupPrimitiveDTO>(p => p.WarehouseID), "Vui lòng chọn kho.", delegate { return (this.WarehouseID != null && this.WarehouseID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<PickupPrimitiveDTO>(p => p.ForkliftDriverID), "Vui lòng chọn tài xế.", delegate { return (this.ForkliftDriverID != null && this.ForkliftDriverID > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<PickupPrimitiveDTO>(p => p.StorekeeperID), "Vui lòng chọn nhân viên kho.", delegate { return (this.StorekeeperID != null && this.StorekeeperID > 0); }));

            return validationRules;

        }
    }

    public class PickupDTO : PickupPrimitiveDTO, IBaseDetailEntity<PickupDetailDTO>
    {
        public PickupDTO()
        {
            this.PickupViewDetails = new BindingList<PickupDetailDTO>();






            this.PackDetails = new BindingListView<PickupDetailDTO>(this.PickupViewDetails);
            this.CartonDetails = new BindingListView<PickupDetailDTO>(this.PickupViewDetails);
            this.PalletDetails = new BindingListView<PickupDetailDTO>(this.PickupViewDetails);

            this.PalletDetails.ApplyFilter(f => f.PackID != null);
            this.PalletDetails.ApplyFilter(f => f.CartonID != null);
            this.PalletDetails.ApplyFilter(f => f.PalletID != null);
        }


        public BindingList<PickupDetailDTO> PickupViewDetails { get; set; }
        public BindingList<PickupDetailDTO> ViewDetails { get { return this.PickupViewDetails; } set { this.PickupViewDetails = value; } }

        public ICollection<PickupDetailDTO> GetDetails() { return this.PickupViewDetails; }

        protected override IEnumerable<PickupDetailDTO> DtoDetails() { return this.PickupViewDetails; }






        public BindingListView<PickupDetailDTO> PackDetails { get; private set; }
        public BindingListView<PickupDetailDTO> CartonDetails { get; private set; }
        public BindingListView<PickupDetailDTO> PalletDetails { get; private set; }

    }

}
