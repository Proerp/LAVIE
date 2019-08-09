using System.Linq;
using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Productions;

namespace TotalDAL.Repositories.Productions
{
    public class PalletRepository : GenericRepository<Pallet>, IPalletRepository
    {
        public PalletRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities)
        {
        }

        public bool GetLocked(int palletID)
        {
            return this.CheckExisting(palletID, "PalletLocked");
        }

        public bool GetPalletChanged(GlobalVariables.FillingLine fillingLineID)
        {
            bool? palletChanged = this.TotalSmartCodingEntities.GetPalletChanged((int)fillingLineID).Single();
            return palletChanged == null ? false : (bool)palletChanged;
        }

        public IList<Pallet> GetPallets(GlobalVariables.FillingLine fillingLineID, int batchID, string entryStatusIDs)
        {
            return this.TotalSmartCodingEntities.GetPallets((int)fillingLineID, batchID, entryStatusIDs).ToList();
        }

        public IList<Pallet> SearchPallets(string barcode)
        {
            return this.TotalSmartCodingEntities.SearchPallets(barcode).ToList();
        }

        public void UpdateEntryStatus(string palletIDs, GlobalVariables.BarcodeStatus barcodeStatus)
        {
            this.TotalSmartCodingEntities.PalletUpdateEntryStatus(palletIDs, (int)barcodeStatus);
        }
    }
}
