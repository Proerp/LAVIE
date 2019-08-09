using System;

using TotalModel;
using TotalBase.Enums;

namespace TotalDTO.Productions
{
    public class PackPrimitiveDTO : BarcodeDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public PackPrimitiveDTO()
            : this(null)
        { }
        public PackPrimitiveDTO(FillingData fillingData)
            : base(fillingData)
        {
            if (fillingData != null)
                this.LineVolume = fillingData.Volume;
        }



        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Pack; } }

        public override int GetID() { return this.PackID; }
        public virtual void SetID(int id) { this.PackID = id; }

        public int PackID { get; set; }
        public Nullable<int> CartonID { get; set; }
        public Nullable<int> RelatedPackID { get; set; }

        public override int PackCounts { get { return 1; } set { } }
    }

    public class PackDTO : PackPrimitiveDTO, IShallowClone<PackDTO>
    {
        public PackDTO()
            : this(null)
        { }
        public PackDTO(FillingData fillingData)
            : base(fillingData)
        { }



        public PackDTO ShallowClone()
        {
            return (PackDTO)this.MemberwiseClone();
        }
    }
}
