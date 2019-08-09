using System.Net;
using System.Linq;

using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Commons;

using TotalCore.Repositories.Productions;
using TotalCore.Services.Productions;

using TotalSmartCoding.Controllers;
using TotalDTO.Productions;
using TotalSmartCoding.ViewModels.Productions;


namespace TotalSmartCoding.Controllers.Productions
{
    public class PackController : GenericSimpleController<Pack, PackDTO, PackPrimitiveDTO, PackViewModel>
    {
        public IPackService packService;

        public PackController(IPackService packService, PackViewModel packViewModel)
            : base(packService, packViewModel)
        {
            this.packService = packService;
        }
    }
}

