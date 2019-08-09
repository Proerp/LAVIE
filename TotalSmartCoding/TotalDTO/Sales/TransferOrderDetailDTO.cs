using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TotalBase;
using TotalModel;
using TotalDTO.Helpers;
using TotalModel.Helpers;
using TotalBase.Enums;
using TotalDTO.Helpers.Interfaces;


namespace TotalDTO.Sales
{
    public class TransferOrderDetailDTO : QuantityDetailDTO, IPrimitiveEntity, IBatchQuantityDetailDTO
    {
        public int GetID() { return this.TransferOrderDetailID; }

        public int DeliveryAdviceID { get { return 0; } }//this property is an implemented the interface IBatchQuantityDetailDTO to use in calling GetBatchAvailables

        public int TransferOrderDetailID { get; set; }
        public int TransferOrderID { get; set; }

        public Nullable<int> WarehouseID { get; set; }
        public Nullable<int> WarehouseReceiptID { get; set; }


        public Nullable<int> BatchID { get; set; }
        private DateTime? batchEntryDate;
        [UIHint("DateTimeReadonly")]
        [Display(Name = "Ngày lập")]
        [Required(ErrorMessage = "Vui lòng nhập ngày lập")]
        public DateTime? BatchEntryDate
        {
            get { return this.batchEntryDate; }
            set { ApplyPropertyChange<TransferOrderDetailDTO, DateTime?>(ref this.batchEntryDate, o => o.BatchEntryDate, value); }
        }
        private string batchCode;
        [DefaultValue(null)]
        public string BatchCode
        {
            get { return this.batchCode; }
            set { ApplyPropertyChange<TransferOrderDetailDTO, string>(ref this.batchCode, o => o.BatchCode, value); }
        }


        private decimal quantityBatchAvailable;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal QuantityBatchAvailable
        {
            get { return this.quantityBatchAvailable; }
            set { ApplyPropertyChange<TransferOrderDetailDTO, decimal>(ref this.quantityBatchAvailable, o => o.QuantityBatchAvailable, Math.Round(value, (int)GlobalEnums.rndQuantity)); }
        }

        private decimal lineVolumeBatchAvailable;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Volume không hợp lệ")]
        public virtual decimal LineVolumeBatchAvailable
        {
            get { return this.lineVolumeBatchAvailable; }
            set { ApplyPropertyChange<TransferOrderDetailDTO, decimal>(ref this.lineVolumeBatchAvailable, o => o.LineVolumeBatchAvailable, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }

        public decimal QuantityIssue { get; set; }
        public decimal LineVolumeIssue { get; set; }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<TransferOrderDetailDTO>(p => p.TransferOrderDetailID), "Số lượng xuất không được lớn hơn số lượng tồn.", delegate { return (this.Quantity <= this.QuantityAvailable && this.LineVolume <= this.LineVolumeAvailable); }));

            return validationRules;
        }

    }
}
