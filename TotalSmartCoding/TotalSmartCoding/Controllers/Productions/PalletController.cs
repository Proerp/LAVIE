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
    public class PalletController : GenericSimpleController<Pallet, PalletDTO, PalletPrimitiveDTO, PalletViewModel>
    {
        public IPalletService palletService;

        public PalletController(IPalletService palletService, PalletViewModel palletViewModel)
            : base(palletService, palletViewModel)
        {
            this.palletService = palletService;
        }
    }
}

