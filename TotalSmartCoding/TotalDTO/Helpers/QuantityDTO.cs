using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TotalModel.Helpers;


namespace TotalDTO.Helpers
{
    public interface IQuantityDTO
    {
        decimal TotalQuantity { get; }
    }

    public abstract class QuantityDTO<TQuantityDetailDTO> : BaseWithDetailDTO<TQuantityDetailDTO>, IQuantityDTO
        where TQuantityDetailDTO : NotifyValidationRule, IQuantityDetailDTO
    {
        public virtual decimal TotalQuantity { get { return this.DtoDetails().Select(o => o.Quantity).Sum(); } }
        public virtual decimal TotalLineVolume { get { return this.DtoDetails().Select(o => o.LineVolume).Sum(); } }
        public virtual decimal TotalVolume { get { return this.DtoDetails().Select(o => o.Volume).Sum(); } }




        public virtual int TotalPackCounts { get { return this.DtoDetails().Select(o => o.PackCounts).Sum(); } }
        public virtual int TotalCartonCounts { get { return this.DtoDetails().Select(o => o.CartonCounts).Sum(); } }
        public virtual int TotalPalletCounts { get { return this.DtoDetails().Select(o => o.PalletCounts).Sum(); } }

        //public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    foreach (var result in base.Validate(validationContext)) { yield return result; }

        //    if (this.TotalQuantity != this.GetTotalQuantity()) yield return new ValidationResult("Lỗi tổng số lượng", new[] { "TotalQuantity" });
        //    if (this.TotalVolume != this.GetTotalVolume()) yield return new ValidationResult("Lỗi tổng số lượng", new[] { "TotalVolume" });
        //}
    }
}
