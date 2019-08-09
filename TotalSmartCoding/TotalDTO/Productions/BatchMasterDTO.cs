using System;

using TotalBase;
using TotalModel;
using TotalBase.Enums;
using System.ComponentModel;
using System.Collections.Generic;
using TotalModel.Helpers;
using System.ComponentModel.DataAnnotations;

namespace TotalDTO.Productions
{
    public class BatchMasterPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.BatchMaster; } }
        public override bool AllowDataInput { get { return GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.BatchMaster; } }
        public override bool Importable { get { return GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.BatchMaster; } }
        public override bool NoApprovable { get { return true; } }
        public override bool NoVoidable { get { return false; } }

        public override int GetID() { return this.BatchMasterID; }
        public virtual void SetID(int id) { this.BatchMasterID = id; }

        public BatchMasterPrimitiveDTO() { this.Initialize(); }

        public override void Init()
        {
            base.Init();
            this.Initialize();
        }

        private void Initialize() { this.PlannedQuantity = 0; }


        private int batchMasterID;
        [DefaultValue(0)]
        public int BatchMasterID
        {
            get { return this.batchMasterID; }
            set { ApplyPropertyChange<BatchMasterPrimitiveDTO, int>(ref this.batchMasterID, o => o.BatchMasterID, value); }
        }

        private DateTime plannedDate;
        public DateTime PlannedDate
        {
            get { return this.plannedDate; }
            set { ApplyPropertyChange<BatchMasterPrimitiveDTO, DateTime>(ref this.plannedDate, o => o.PlannedDate, value); }
        }

        private string code;
        [DefaultValue(null)]
        public string Code
        {
            get { return this.code; }
            set { ApplyPropertyChange<BatchMasterPrimitiveDTO, string>(ref this.code, o => o.Code, value); }
        }

        private string statusCode;
        [DefaultValue(null)]
        public string StatusCode
        {
            get { return this.statusCode; }
            set { ApplyPropertyChange<BatchMasterPrimitiveDTO, string>(ref this.statusCode, o => o.StatusCode, value); }
        }


        private int commodityID;
        [DefaultValue(null)]
        public int CommodityID
        {
            get { return this.commodityID; }
            set { ApplyPropertyChange<BatchMasterPrimitiveDTO, int>(ref this.commodityID, o => o.CommodityID, value); }
        }

        private int batchStatusID;
        [DefaultValue(null)]
        public int BatchStatusID
        {
            get { return this.batchStatusID; }
            set { ApplyPropertyChange<BatchMasterPrimitiveDTO, int>(ref this.batchStatusID, o => o.BatchStatusID, value); }
        }

        private decimal plannedQuantity;
        //[DefaultValue(0.0)]
        [Range(1, 99999999999, ErrorMessage = "Volume không hợp lệ")]
        public virtual decimal PlannedQuantity
        {
            get { return this.plannedQuantity; }
            set { ApplyPropertyChange<BatchMasterPrimitiveDTO, decimal>(ref this.plannedQuantity, o => o.PlannedQuantity, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }

        [DefaultValue(false)]
        public bool IsDefault { get; set; }
    }

    public class BatchMasterDTO : BatchMasterPrimitiveDTO
    {
        public BatchMasterDTO()
        {
            this.LocationID = GlobalVariables.LocationID;
        }

        private string commodityName;
        [DefaultValue(null)]
        public string CommodityName
        {
            get { return this.commodityName; }
            set { ApplyPropertyChange<BatchMasterDTO, string>(ref this.commodityName, o => o.CommodityName, value, false); }
        }

        private string commodityCode;
        [DefaultValue(null)]
        public string CommodityCode
        {
            get { return this.commodityCode; }
            set { ApplyPropertyChange<BatchMasterDTO, string>(ref this.commodityCode, o => o.CommodityCode, value, false); }
        }

        private string commodityAPICode;
        [DefaultValue(null)]
        public string CommodityAPICode
        {
            get { return this.commodityAPICode; }
            set { ApplyPropertyChange<BatchMasterDTO, string>(ref this.commodityAPICode, o => o.CommodityAPICode, value, false); }
        }

        public bool IsFV { get { return CommonExpressions.IsFV(this.CommodityCode); } }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BatchMasterPrimitiveDTO>(p => p.EntryDate), "Vui lòng nhập ngày sản xuất.", delegate { return this.EntryDate > new DateTime(2000, 1, 1) || this.BatchMasterID == 0; }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BatchMasterPrimitiveDTO>(p => p.CommodityID), "Vui lòng chọn mã sản phẩm.", delegate { return this.CommodityID > 0; }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BatchMasterPrimitiveDTO>(p => p.BatchStatusID), "Vui lòng chọn trạng thái.", delegate { return this.BatchStatusID > 0; }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BatchMasterPrimitiveDTO>(p => p.Code), "Số batch quy định là 5 ký tự.", delegate { return this.Code != null && this.Code.Length == 5; }));
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<BatchMasterPrimitiveDTO>(p => p.PlannedQuantity), "Vui lòng nhập số lượng kế hoạch sản xuất.", delegate { return (this.PlannedQuantity >= 0); }));

            return validationRules;

        }
    }
}
