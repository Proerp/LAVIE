using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using TotalModel.Helpers;

namespace TotalDTO.Helpers
{
    public interface IDiscountVATAmountDTO : IVATAmountDTO
    {
        decimal AverageDiscountPercent { get; set; }
    }

    public abstract class DiscountVATAmountDTO<TDiscountVATAmountDetailDTO> : VATAmountDTO<TDiscountVATAmountDetailDTO>, IDiscountVATAmountDTO
        where TDiscountVATAmountDetailDTO : NotifyValidationRule, IDiscountVATAmountDetailDTO
    {
        [Display(Name = "Bình quân CK")]
        public decimal AverageDiscountPercent { get; set; }
    }
}
