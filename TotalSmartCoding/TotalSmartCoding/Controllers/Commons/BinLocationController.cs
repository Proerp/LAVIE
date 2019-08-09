using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class BinLocationController : GenericSimpleController<BinLocation, BinLocationDTO, BinLocationPrimitiveDTO, BinLocationViewModel>
    {
        public BinLocationController(IBinLocationService binLocationService, BinLocationViewModel binLocationViewModel)
            : base(binLocationService, binLocationViewModel)
        {
        }
    }
}
