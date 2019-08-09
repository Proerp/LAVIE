using System;
using System.ComponentModel.DataAnnotations;

namespace TotalDTO.Commons
{
    public interface IWarehouseAdjustmentTypeBaseDTO
    {
        Nullable<int> WarehouseAdjustmentTypeID { get; set; }
        string Code { get; set; }
        [Display(Name = "Kho hàng")]
        [UIHint("AutoCompletes/WarehouseAdjustmentTypeBase")]
        [Required(ErrorMessage = "Vui lòng nhập kho hàng")]
        string Name { get; set; }
    }

    public class WarehouseAdjustmentTypeBaseDTO : BaseDTO, IWarehouseAdjustmentTypeBaseDTO
    {
        public Nullable<int> WarehouseAdjustmentTypeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class WarehouseAdjustmentTypeDTO
    {
    }
}
