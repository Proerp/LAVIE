using System;

using TotalModel;
using TotalBase.Enums;

namespace TotalDTO.Productions
{
    public class CartonPrimitiveDTO : BarcodeDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public CartonPrimitiveDTO()
            : this(null)
        { }
        public CartonPrimitiveDTO(FillingData fillingData)
            : base(fillingData)
        { }


        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Carton; } }

        public override int GetID() { return this.CartonID; }
        public virtual void SetID(int id) { this.CartonID = id; }

        public int CartonID { get; set; }
        public Nullable<int> PalletID { get; set; }
    }

    public class CartonDTO : CartonPrimitiveDTO, IShallowClone<CartonDTO>
    {
        public CartonDTO()
            : this(null)
        { }
        public CartonDTO(FillingData fillingData)
            : base(fillingData)
        { }


        public string PackIDs { get; set; }

        public CartonDTO ShallowClone()
        {
            return (CartonDTO)this.MemberwiseClone();
        }
    }
}
