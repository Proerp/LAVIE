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
    public class LaviePrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Lavie; } }
        public override bool AllowDataInput { get { return false; } }
        public override bool Importable { get { return true; } }
        public override bool NoApprovable { get { return true; } }
        public override bool NoVoidable { get { return true; } }

        public LaviePrimitiveDTO() { this.Initialize(); }

        public override void Init()
        {
            base.Init();
            this.Initialize();
        }

        private void Initialize() { }


        public override int GetID() { return this.LavieID; }
        public void SetID(int id) { this.LavieID = id; }

        private int lavieID;
        [DefaultValue(0)]
        public int LavieID
        {
            get { return this.lavieID; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, int>(ref this.lavieID, o => o.LavieID, value); }
        }

        private int serialID;
        [DefaultValue(0)]
        public int SerialID
        {
            get { return this.serialID; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, int>(ref this.serialID, o => o.SerialID, value); }
        }

        private string itemNumber;
        [DefaultValue(null)]
        public string ItemNumber
        {
            get { return this.itemNumber; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, string>(ref this.itemNumber, o => o.ItemNumber, value); }
        }

        private string productName;
        [DefaultValue(null)]
        public string ProductName
        {
            get { return this.productName; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, string>(ref this.productName, o => o.ProductName, value); }
        }

        private string gtIN;
        [DefaultValue(null)]
        public string GTIN
        {
            get { return this.gtIN; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, string>(ref this.gtIN, o => o.GTIN, value); }
        }

        private string palletID;
        [DefaultValue(null)]
        public string PalletID
        {
            get { return this.palletID; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, string>(ref this.palletID, o => o.PalletID, value); }
        }

        private string batchNumber;
        [DefaultValue(null)]
        public string BatchNumber
        {
            get { return this.batchNumber; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, string>(ref this.batchNumber, o => o.BatchNumber, value); }
        }

        private Nullable<DateTime> expirationDate;
        public Nullable<DateTime> ExpirationDate
        {
            get { return this.expirationDate; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, Nullable<DateTime>>(ref this.expirationDate, o => o.ExpirationDate, value); }
        }

        private decimal qty;
        [Range(1, 99999999999, ErrorMessage = "Qty không hợp lệ")]
        public virtual decimal Qty
        {
            get { return this.qty; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, decimal>(ref this.qty, o => o.Qty, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }

        private decimal layers;
        [Range(1, 99999999999, ErrorMessage = "Layers không hợp lệ")]
        public virtual decimal Layers
        {
            get { return this.layers; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, decimal>(ref this.layers, o => o.Layers, Math.Round(value, (int)GlobalEnums.rndWeight)); }
        }


        private string gtINBarcode;
        [DefaultValue(null)]
        public string GTINBarcode
        {
            get { return this.gtINBarcode; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, string>(ref this.gtINBarcode, o => o.GTINBarcode, value); }
        }

        private string barcode;
        [DefaultValue(null)]
        public string Barcode
        {
            get { return this.barcode; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, string>(ref this.barcode, o => o.Barcode, value); }
        }

        private int printedTimes;
        [DefaultValue(0)]
        public int PrintedTimes
        {
            get { return this.printedTimes; }
            set { ApplyPropertyChange<LaviePrimitiveDTO, int>(ref this.printedTimes, o => o.PrintedTimes, value); }
        }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.ItemNumber), "Vui lòng nhập mã mặt hàng.", delegate { return (this.ItemNumber != null && this.ItemNumber.Trim() != ""); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.ProductName), "Vui lòng nhập tên mặt hàng.", delegate { return (this.ProductName != null && this.ProductName.Trim() != ""); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.GTIN), "Vui lòng nhập tên đầy đủ.", delegate { return (this.GTIN != null && this.GTIN.Trim() != ""); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.BatchNumber), "Vui lòng nhập quy cách đóng gói.", delegate { return (this.BatchNumber != null && this.BatchNumber.Trim() != ""); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.GTINBarcode), "Vui lòng nhập quy cách đóng gói.", delegate { return (this.GTINBarcode != null && this.GTINBarcode.Trim() != ""); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.Barcode), "Vui lòng nhập quy cách đóng gói.", delegate { return (this.Barcode != null && this.Barcode.Trim() != ""); }));

            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.Qty), "Vui lòng nhập qty.", delegate { return (this.Qty > 0); }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<LaviePrimitiveDTO>(p => p.Layers), "Vui lòng nhập qty.", delegate { return (this.Layers > 0); }));

            return validationRules;
        }
    }

    public class LavieDTO : LaviePrimitiveDTO
    {
    }
}
