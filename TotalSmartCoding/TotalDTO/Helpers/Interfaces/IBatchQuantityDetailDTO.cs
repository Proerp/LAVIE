using System;

namespace TotalDTO.Helpers.Interfaces
{
    public interface IBatchQuantityDetailDTO : IAvailableQuantityDetailDTO
    {
        //THESE PROPERTY (DeliveryAdviceID, TransferOrderID) ARE USED TO GetBatchAvailables IN VIEW: OptionBatches
        int DeliveryAdviceID { get; }
        int TransferOrderID { get; }

        Nullable<int> BatchID { get; set; }

        string BatchCode { get; set; }
        DateTime? BatchEntryDate { get; set; }

        decimal QuantityBatchAvailable { get; set; }
        decimal LineVolumeBatchAvailable { get; set; }
    }
}
