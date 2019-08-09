using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

namespace TotalCore.Repositories.Productions
{
    public interface IPalletRepository : IGenericRepository<Pallet>
    {
        bool GetLocked(int palletID);

        bool GetPalletChanged(GlobalVariables.FillingLine fillingLineID);
        IList<Pallet> GetPallets(GlobalVariables.FillingLine fillingLineID, int batchID, string entryStatusIDs);
        IList<Pallet> SearchPallets(string barcode);

        void UpdateEntryStatus(string cartonIDs, GlobalVariables.BarcodeStatus barcodeStatus);
    }
}
