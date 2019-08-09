using TotalBase;

namespace TotalDTO.Productions
{
    public class BarcodeDTO : BaseDTO
    {
        public BarcodeDTO()
        { }
        public BarcodeDTO(FillingData fillingData)
        {
            if (fillingData != null)
            {
                this.FillingLineID = (int)fillingData.FillingLineID;
                this.BatchID = fillingData.BatchID;
                this.CommodityID = fillingData.CommodityID;

                this.EntryStatusID = (int)GlobalVariables.BarcodeStatus.Freshnew;
                this.Locked = true;
            }
        }

        public int FillingLineID { get; set; }
        public int BatchID { get; set; }
        public int CommodityID { get; set; }
        public string Code { get; set; }

        public decimal Quantity { get; set; }
        public virtual int PackCounts { get; set; }

        public decimal LineVolume { get; set; }

        public int EntryStatusID { get; set; }

        public int QueueID { get; set; } //JUST FOR PackDTO ONLY        
    }
}
