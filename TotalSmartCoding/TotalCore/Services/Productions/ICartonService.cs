using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

using TotalDTO.Productions;

namespace TotalCore.Services.Productions
{
    public interface ICartonService : IGenericService<Carton, CartonDTO, CartonPrimitiveDTO>
    {
        IList<Carton> GetCartons(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? palletID);

        bool UpdateEntryStatus(string cartonIDs, GlobalVariables.BarcodeStatus barcodeStatus);
    }
}
