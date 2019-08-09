using TotalModel.Models;
using TotalDTO.Commons;

using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;

namespace TotalSmartCoding.Controllers.Commons
{
    public class LavieController : GenericSimpleController<Lavie, LavieDTO, LaviePrimitiveDTO, LavieViewModel>
    {
        public LavieController(ILavieService lavieService, LavieViewModel lavieViewModel)
            : base(lavieService, lavieViewModel)
        {
        }
    }
}
