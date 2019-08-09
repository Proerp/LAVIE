using System.Linq;
using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Productions;

namespace TotalDAL.Repositories.Productions
{
    public class PackRepository : GenericRepository<Pack>, IPackRepository
    {
        public PackRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities)
        {
        }

        public IList<Pack> GetPacks(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? cartonID)
        {
            return this.TotalSmartCodingEntities.GetPacks((int)fillingLineID, entryStatusIDs, cartonID).ToList();
        }

        public IList<Pack> SearchPacks(string barcode)
        {
            return this.TotalSmartCodingEntities.SearchPacks(barcode).ToList();
        }

        public int? GetRelatedPackID(int batchID, string barcode)
        {
            return this.TotalSmartCodingEntities.GetRelatedPackID(batchID, barcode).FirstOrDefault();
        }

        public void UpdateQueueID(string packIDs, int queueID)
        {
            this.TotalSmartCodingEntities.PackUpdateQueueID(packIDs, queueID);
        }

        public void UpdateEntryStatus(string packIDs, GlobalVariables.BarcodeStatus barcodeStatus)
        {
            this.TotalSmartCodingEntities.PackUpdateEntryStatus(packIDs, (int) barcodeStatus);
        }
    }
}
