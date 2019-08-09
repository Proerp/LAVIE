using System;
using System.ComponentModel.DataAnnotations;

using TotalModel;
using TotalDTO.Helpers;
using System.Collections.Generic;
using TotalModel.Helpers;
using TotalBase;
using System.ComponentModel;
using TotalBase.Enums;
using TotalDTO.Helpers.Interfaces;

namespace TotalDTO.Sales
{
    public class DeliveryAdviceDetailDTO : QuantityDetailDTO, IPrimitiveEntity, IBatchQuantityDetailDTO
    {
        public int GetID() { return this.DeliveryAdviceDetailID; }

        //IMPORTANT: IMPLEMENT PropertyChanged!!!!
        //NOW: AFTER ADD DeliveryAdviceDetailDTO TO COLLECTION, WE DON'T CHANGE THESE PROPERTIES FROM BINDING DataGridView ALSO FROM BACKEND DeliveryAdviceDetailDTO OBJECT. SO: WE DON'T IMPLEMENT PropertyChanged FOR THESE PROPERTIES
        //LATER: IF WE RECEIPT FORM OTHER SOURCE THAN FROM PICKUP ONLY, WE SHOULD CONSIDER THIS => AND IMPLEMENT PropertyChanged FOR THESE PROPERTIES WHEN NECCESSARY

        public int TransferOrderID { get { return 0; } } //this property is an implemented the interface IBatchQuantityDetailDTO to use in calling GetBatchAvailables

        public int DeliveryAdviceDetailID { get; set; }
        public int DeliveryAdviceID { get; set; }

        public Nullable<int> SalesOrderID { get; set; }
        public Nullable<int> SalesOrderDetailID { get; set; }

        public string SalesOrderReference { get; set; }
        public Nullable<System.DateTime> SalesOrderEntryDate { get; set; }


        public Nullable<int> BatchID { get; set; }
        private DateTime? batchEntryDate;
        [UIHint("DateTimeReadonly")]
        [Display(Name = "Ngày lập")]
        [Required(ErrorMessage = "Vui lòng nhập ngày lập")]
        public DateTime? BatchEntryDate
        {
            get { return this.batchEntryDate; }
            set { ApplyPropertyChange<DeliveryAdviceDetailDTO, DateTime?>(ref this.batchEntryDate, o => o.BatchEntryDate, value); }
        }
        private string batchCode;
        [DefaultValue(null)]
        public string BatchCode
        {
            get { return this.batchCode; }
            set { ApplyPropertyChange<DeliveryAdviceDetailDTO, string>(ref this.batchCode, o => o.BatchCode, value); }
        }


        private decimal quantityBatchAvailable;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Số lượng không hợp lệ")]
        public virtual decimal QuantityBatchAvailable
        {
            get { return this.quantityBatchAvailable; }
            set { ApplyPropertyChange<DeliveryAdviceDetailDTO, decimal>(ref this.quantityBatchAvailable, o => o.QuantityBatchAvailable, Math.Round(value, (int)GlobalEnums.rndQuantity)); }
        }

        private decimal lineVolumeBatchAvailable;
        [DefaultValue(0)]
        [Range(0, 99999999999, ErrorMessage = "Volume không hợp lệ")]
        public virtual decimal LineVolumeBatchAvailable
        {
            get { return this.lineVolumeBatchAvailable; }
            set { ApplyPropertyChange<DeliveryAdviceDetailDTO, decimal>(ref this.lineVolumeBatchAvailable, o => o.LineVolumeBatchAvailable, Math.Round(value, (int)GlobalEnums.rndVolume)); }
        }



        public string VoucherCode { get; set; }
        public int CustomerID { get; set; }
        public int ReceiverID { get; set; }

        public decimal QuantityIssue { get; set; }
        public decimal LineVolumeIssue { get; set; }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.DeliveryAdviceDetailID), "Số lượng xuất không được lớn hơn số lượng tồn.", delegate { return (this.Quantity <= this.QuantityAvailable && this.Quantity <= this.QuantityRemains && this.LineVolume <= this.LineVolumeAvailable && this.LineVolume <= this.LineVolumeRemains); }));

            return validationRules;
        }
    }





}
