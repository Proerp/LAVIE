using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

namespace TotalCore.Repositories.Productions
{
    public interface IBatchRepository : IGenericRepository<Batch>
    {
        bool GetLocked(int batchID);

        void CommonUpdate(int batchID, string nextPackNo, string nextCartonNo, string nextPalletNo);
        void RepackDelete(int batchID);
        void RepackUpdate(int batchID, int repackID);
        bool RepackReprint(int repackID);

        void AddLot(int batchID);
    }

    public interface IBatchAPIRepository : IGenericAPIRepository
    {
        List<PendingLot> GetPendingLots(int? locationID, int? fillingLineID);
        List<BatchRepack> GetBatchRepacks(int? batchID, bool notPrintedOnly);

        List<BatchAvailable> GetBatchAvailables(int? locationID, int? deliveryAdviceID, int? transferOrderID, int? commodityID, bool withNullRow);
    }
}
