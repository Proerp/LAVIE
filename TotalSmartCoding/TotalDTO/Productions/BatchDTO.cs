using System;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using System.ComponentModel;
using System.Collections.Generic;
using TotalModel.Helpers;

namespace TotalDTO.Productions
{
    public class BatchPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Batch; } }
        public override bool AllowDataInput { get { return GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail; } }
        public override bool NoVoidable { get { return false; } }

        public override int GetID() { return this.BatchID; }
        public virtual void SetID(int id) { this.BatchID = id; }

        private int batchID;
        [DefaultValue(0)]
        public int BatchID
        {
            get { return this.batchID; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, int>(ref this.batchID, o => o.BatchID, value); }
        }

        private int batchMasterID;
        [DefaultValue(null)]
        public int BatchMasterID
        {
            get { return this.batchMasterID; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, int>(ref this.batchMasterID, o => o.BatchMasterID, value); }
        }

        //public override string Reference { get { return this.Code + this.LotCode; } set { } }

        private int lotID;
        [DefaultValue(null)]
        public int LotID
        {
            get { return this.lotID; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, int>(ref this.lotID, o => o.LotID, value); }
        }

        private string code;
        [DefaultValue(null)]
        public string Code
        {
            get { return this.code; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, string>(ref this.code, o => o.Code, value); }
        }

        private string lotCode;
        [DefaultValue(null)]
        public string LotCode
        {
            get { return this.lotCode; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, string>(ref this.lotCode, o => o.LotCode, value); }
        }

        public int FillingLineID { get; set; }


        private int commodityID;
        [DefaultValue(null)]
        public int CommodityID
        {
            get { return this.commodityID; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, int>(ref this.commodityID, o => o.CommodityID, value); }
        }

        private int batchTypeID;
        [DefaultValue(null)]
        public int BatchTypeID
        {
            get { return this.batchTypeID; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, int>(ref this.batchTypeID, o => o.BatchTypeID, value); }
        }

        private string batchStatusCode;
        [DefaultValue(null)]
        public string BatchStatusCode
        {
            get { return this.batchStatusCode; }
            set { ApplyPropertyChange<BatchDTO, string>(ref this.batchStatusCode, o => o.BatchStatusCode, value, false); }
        }

        private string nextPackNo;
        [DefaultValue("000001")]
        public string NextPackNo
        {
            get { return this.nextPackNo; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, string>(ref this.nextPackNo, o => o.NextPackNo, value); }
        }

        private string nextCartonNo;
        [DefaultValue("000001")]
        public string NextCartonNo
        {
            get { return this.nextCartonNo; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, string>(ref this.nextCartonNo, o => o.NextCartonNo, value); }
        }

        private string nextPalletNo;
        [DefaultValue("000001")]
        public string NextPalletNo
        {
            get { return this.nextPalletNo; }
            set { ApplyPropertyChange<BatchPrimitiveDTO, string>(ref this.nextPalletNo, o => o.NextPalletNo, value); }
        }



        [DefaultValue(false)]
        public bool IsDefault { get; set; }
    }

    public class BatchDTO : BatchPrimitiveDTO
    {
        public BatchDTO()
        {
            this.FillingLineID = (int)GlobalVariables.FillingLineID;
            this.LocationID = GlobalVariables.LocationID;
        }

        private string commodityCode;
        [DefaultValue(null)]
        public string CommodityCode
        {
            get { return this.commodityCode; }
            set { ApplyPropertyChange<BatchDTO, string>(ref this.commodityCode, o => o.CommodityCode, value, false); }
        }

        private string commodityName;
        [DefaultValue(null)]
        public string CommodityName
        {
            get { return this.commodityName; }
            set { ApplyPropertyChange<BatchDTO, string>(ref this.commodityName, o => o.CommodityName, value, false); }
        }

        private string commodityAPICode;
        [DefaultValue(null)]
        public string CommodityAPICode
        {
            get { return this.commodityAPICode; }
            set { ApplyPropertyChange<BatchDTO, string>(ref this.commodityAPICode, o => o.CommodityAPICode, value, false); }
        }

        private string commodityCartonCode;
        [DefaultValue(null)]
        public string CommodityCartonCode
        {
            get { return this.commodityCartonCode; }
            set { ApplyPropertyChange<BatchDTO, string>(ref this.commodityCartonCode, o => o.CommodityCartonCode, value, false); }
        }


        private decimal volume;
        public virtual decimal Volume
        {
            get { return this.volume; }
            set { ApplyPropertyChange<BatchDTO, decimal>(ref this.volume, o => o.Volume, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }

        private int packPerCarton;
        public virtual int PackPerCarton
        {
            get { return this.packPerCarton; }
            set { ApplyPropertyChange<BatchDTO, int>(ref this.packPerCarton, o => o.PackPerCarton, value); }
        }

        private int cartonPerPallet;
        public virtual int CartonPerPallet
        {
            get { return this.cartonPerPallet; }
            set { ApplyPropertyChange<BatchDTO, int>(ref this.cartonPerPallet, o => o.CartonPerPallet, value); }
        }

        private int shelflife;
        public virtual int Shelflife
        {
            get { return this.shelflife; }
            set { ApplyPropertyChange<BatchDTO, int>(ref this.shelflife, o => o.Shelflife, value); }
        }

        private decimal plannedQuantity;
        public virtual decimal PlannedQuantity
        {
            get { return this.plannedQuantity; }
            set { ApplyPropertyChange<BatchDTO, decimal>(ref this.plannedQuantity, o => o.PlannedQuantity, value); }
        }


        private decimal packQuantity;
        public virtual decimal PackQuantity
        {
            get { return this.packQuantity; }
            set { ApplyPropertyChange<BatchDTO, decimal>(ref this.packQuantity, o => o.PackQuantity, value); }
        }

        private decimal packLineVolume;
        public virtual decimal PackLineVolume
        {
            get { return this.packLineVolume; }
            set { ApplyPropertyChange<BatchDTO, decimal>(ref this.packLineVolume, o => o.PackLineVolume, value); }
        }


        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules(); int value;
            validationRules.Add(new SimpleValidationRule("CommodityID", "Vui lòng chọn mã sản phẩm.", delegate { return this.CommodityID > 0; }));
            validationRules.Add(new SimpleValidationRule("BatchMasterID", "Vui lòng chọn Batch.", delegate { return this.BatchMasterID > 0; }));
            validationRules.Add(new SimpleValidationRule("LotID", "Vui lòng chọn Lot.", delegate { return this.LotID > 0; }));
            validationRules.Add(new SimpleValidationRule("BatchTypeID", "Vui lòng chọn [N-new], [R-Repack], [T-Trial].", delegate { return this.BatchTypeID > 0; }));
            validationRules.Add(new SimpleValidationRule("Code", "Số batch quy định là 5 ký tự.", delegate { return this.Code != null && this.Code.Length == 5; }));
            validationRules.Add(new SimpleValidationRule("LotCode", "Số Lot quy định là 1 ký tự.", delegate { return this.LotCode != null && this.LotCode.Length == 1 && TotalBase.CommonExpressions.AlphaNumericStringLOTNUMBER(this.LotCode).Length == 1; }));
            validationRules.Add(new SimpleValidationRule("NextPackNo", "Số thứ tự chai quy định là 6 chữ số.", delegate { return this.NextPackNo.Length == 6 && int.TryParse(this.NextPackNo, out value); }));
            validationRules.Add(new SimpleValidationRule("NextCartonNo", "Số thứ tự carton quy định là 6 chữ số.", delegate { return this.NextCartonNo.Length == 6 && int.TryParse(this.NextCartonNo, out value); }));
            validationRules.Add(new SimpleValidationRule("NextPalletNo", "Số thứ tự pallet quy định là 6 chữ số.", delegate { return this.NextPalletNo.Length == 6 && int.TryParse(this.NextPalletNo, out value); }));

            return validationRules;

        }
    }
}
