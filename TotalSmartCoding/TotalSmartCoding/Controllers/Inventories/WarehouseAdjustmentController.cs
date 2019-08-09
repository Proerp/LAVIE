using TotalModel.Models;
using TotalDTO.Inventories;

using TotalCore.Services.Inventories;
using TotalSmartCoding.ViewModels.Inventories;

namespace TotalSmartCoding.Controllers.Inventories
{
    public class WarehouseAdjustmentController : GenericViewDetailController<WarehouseAdjustment, WarehouseAdjustmentDetail, WarehouseAdjustmentViewDetail, WarehouseAdjustmentDTO, WarehouseAdjustmentPrimitiveDTO, WarehouseAdjustmentDetailDTO, WarehouseAdjustmentViewModel>
    {
        public WarehouseAdjustmentController(IWarehouseAdjustmentService warehouseAdjustmentService, WarehouseAdjustmentViewModel warehouseAdjustmentViewModel)
            : base(warehouseAdjustmentService, warehouseAdjustmentViewModel)
        {
        }
    }
}
