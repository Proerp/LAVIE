using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

using TotalCore.Repositories.Productions;

namespace TotalSmartCoding.Controllers.APIs.Productions
{
    public class CartonAPIs
    {
        private readonly ICartonRepository cartonRepository;

        public CartonAPIs(ICartonRepository cartonRepository)
        {
            this.cartonRepository = cartonRepository;
        }

        public IList<Carton> GetCartons(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? palletID)
        {
            return this.cartonRepository.GetCartons(fillingLineID, entryStatusIDs, palletID);
        }
    }
}
