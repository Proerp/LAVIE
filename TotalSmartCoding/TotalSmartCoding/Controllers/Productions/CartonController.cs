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
    public class CartonController : GenericSimpleController<Carton, CartonDTO, CartonPrimitiveDTO, CartonViewModel>
    {
        public ICartonService cartonService;

        public CartonController(ICartonService cartonService, CartonViewModel cartonViewModel)
            : base(cartonService, cartonViewModel)
        {
            this.cartonService = cartonService;
        }
    }
}

