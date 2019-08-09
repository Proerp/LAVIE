using System;
using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

namespace TotalCore.Repositories.Productions
{
    public interface IBatchMasterRepository : IGenericRepository<BatchMaster>
    {
        void AddLot(int batchMasterID, DateTime? entryDate);
        void RemoveLot(int lotID);
    }

    public interface IBatchMasterAPIRepository : IGenericAPIRepository
    {
        BatchMasterBase GetBatchMasterBase(string code);
        BatchMasterBase GetBatchMasterBase(int batchMasterID);
        IList<BatchMasterBase> GetBatchMasterBases();
        IList<BatchMasterTree> GetBatchMasterTrees(DateTime fromDate, DateTime toDate);
    }
}
