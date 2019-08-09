using System;
using System.ComponentModel.DataAnnotations;

using TotalModel;
using TotalDTO.Helpers;
using System.Collections.Generic;
using TotalModel.Helpers;
using TotalBase;

namespace TotalDTO.Inventories
{
    public class WarehouseAdjustmentDetailDTO : QuantityDetailDTO, IPrimitiveEntity
    {
        public int GetID() { return this.WarehouseAdjustmentDetailID; }

        //IMPORTANT: IMPLEMENT PropertyChanged!!!!
        //NOW: AFTER ADD WarehouseAdjustmentDetailDTO TO COLLECTION, WE DON'T CHANGE THESE PROPERTIES FROM BINDING DataGridView ALSO FROM BACKEND WarehouseAdjustmentDetailDTO OBJECT. SO: WE DON'T IMPLEMENT PropertyChanged FOR THESE PROPERTIES
        //LATER: IF WE RECEIPT FORM OTHER SOURCE THAN FROM PICKUP ONLY, WE SHOULD CONSIDER THIS => AND IMPLEMENT PropertyChanged FOR THESE PROPERTIES WHEN NECCESSARY


        public int WarehouseAdjustmentDetailID { get; set; }
        public int WarehouseAdjustmentID { get; set; }

        public int WarehouseAdjustmentTypeID { get; set; }

        public Nullable<int> GoodsReceiptID { get; set; }
        public Nullable<int> GoodsReceiptDetailID { get; set; }

        public string GoodsReceiptReference { get; set; }
        public Nullable<System.DateTime> GoodsReceiptEntryDate { get; set; }

        public int BatchID { get; set; }
        public System.DateTime BatchEntryDate { get; set; }

        public int WarehouseID { get; set; }
        public string WarehouseCode { get; set; }

        public Nullable<int> WarehouseReceiptID { get; set; }

        public int BinLocationID { get; set; }
        public string BinLocationCode { get; set; }

        public Nullable<int> PackID { get; set; }
        public string PackCode { get; set; }

        public Nullable<int> CartonID { get; set; }
        public string CartonCode { get; set; }

        public Nullable<int> PalletID { get; set; }
        public string PalletCode { get; set; }

        protected override List<ValidationRule> CreateRules()
        {
            List<ValidationRule> validationRules = base.CreateRules();
            validationRules.Add(new SimpleValidationRule(CommonExpressions.PropertyName<WarehouseAdjustmentDetailDTO>(p => p.WarehouseAdjustmentDetailID), "Số lượng xuất không được lớn hơn số lượng yêu cầu.", delegate { return (this.Quantity > 0 || (-this.Quantity <= this.QuantityAvailable && -this.LineVolume <= this.LineVolumeAvailable)); }));

            return validationRules;
        }
    }





}
