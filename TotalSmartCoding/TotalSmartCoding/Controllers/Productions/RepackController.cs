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
    public class RepackController : GenericSimpleController<Repack, RepackDTO, RepackPrimitiveDTO, RepackViewModel>
    {
        public IRepackService repackService;

        public RepackController(IRepackService repackService, RepackViewModel repackViewModel)
            : base(repackService, repackViewModel)
        {
            this.repackService = repackService;
        }
    }
}

