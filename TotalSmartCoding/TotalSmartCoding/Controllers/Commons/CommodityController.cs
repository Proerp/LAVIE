using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class CommodityController : GenericSimpleController<Commodity, CommodityDTO, CommodityPrimitiveDTO, CommodityViewModel>
    {
        public CommodityController(ICommodityService commodityService, CommodityViewModel commodityViewModel)
            : base(commodityService, commodityViewModel)
        {
        }
    }
}
