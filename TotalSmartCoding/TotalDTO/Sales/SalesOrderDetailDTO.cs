using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TotalBase;
using TotalModel;
using TotalDTO.Helpers;
using TotalModel.Helpers;
using TotalDTO.Helpers.Interfaces;


namespace TotalDTO.Sales
{
    public class SalesOrderDetailDTO : QuantityDetailDTO, IPrimitiveEntity, IAvailableQuantityDetailDTO
    {
        public int GetID() { return this.SalesOrderDetailID; }

        public int SalesOrderDetailID { get; set; }
        public int SalesOrderID { get; set; }
        public string VoucherCode { get; set; }

        public Nullable<int> CustomerID { get; set; }
        public Nullable<int> ReceiverID { get; set; }
    }
}
