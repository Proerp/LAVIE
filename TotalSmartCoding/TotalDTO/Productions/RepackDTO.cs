using System;

using TotalModel;
using TotalBase.Enums;

namespace TotalDTO.Productions
{
    public class RepackPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Repack; } }

        public override int GetID() { return this.RepackID; }
        public virtual void SetID(int id) { this.RepackID = id; }

        public int RepackID { get; set; }

        public int SerialID { get; set; }

        public int PackID { get; set; }
        public string Code { get; set; } 
        public int BatchID { get; set; }
        public DateTime? BatchEntryDate { get; set; }
        public string BatchCode { get; set; }
        public string LotCode { get; set; }
    }

    public class RepackDTO : RepackPrimitiveDTO
    {
    }
}
