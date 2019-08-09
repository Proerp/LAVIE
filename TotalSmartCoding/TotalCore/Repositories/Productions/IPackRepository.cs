using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

namespace TotalCore.Repositories.Productions
{
    public interface IPackRepository : IGenericRepository<Pack>
    {
        IList<Pack> GetPacks(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? cartonID);
        IList<Pack> SearchPacks(string barcode);
        int? GetRelatedPackID(int batchID, string barcode);

        void UpdateQueueID(string packIDs, int queueID);
        void UpdateEntryStatus(string packIDs, GlobalVariables.BarcodeStatus barcodeStatus);
    }
}
