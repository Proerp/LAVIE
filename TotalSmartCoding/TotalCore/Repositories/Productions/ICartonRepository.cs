using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

namespace TotalCore.Repositories.Productions
{
    public interface ICartonRepository : IGenericRepository<Carton>
    {
        IList<Carton> GetCartons(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? palletID);
        IList<Carton> SearchCartons(string barcode);

        void UpdateEntryStatus(string cartonIDs, GlobalVariables.BarcodeStatus barcodeStatus);
    }
}
