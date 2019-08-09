using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;



using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Productions;
using TotalBase;

namespace TotalSmartCoding.Controllers.APIs.Productions
{
    public class BatchMasterAPIs
    {
        private readonly IBatchMasterAPIRepository batchMasterAPIRepository;

        public BatchMasterAPIs(IBatchMasterAPIRepository batchMasterAPIRepository)
        {
            this.batchMasterAPIRepository = batchMasterAPIRepository;
        }


        public ICollection<BatchMasterIndex> GetBatchMasterIndexes(bool showCummulativePacks, GlobalEnums.ActiveOption activeOption)
        {
            this.batchMasterAPIRepository.RepositoryBag["ShowCummulativePacks"] = showCummulativePacks ? 1 : 0;
            this.batchMasterAPIRepository.RepositoryBag["ActiveOption"] = (int)activeOption;
            ICollection<BatchMasterIndex> goodsReceiptIndexes = this.batchMasterAPIRepository.GetEntityIndexes<BatchMasterIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);

            return goodsReceiptIndexes;
        }

        public BatchMasterIndex GetActiveBatchMasterIndex()
        {
            BatchMasterIndex goodsReceiptIndexes = this.GetBatchMasterIndexes(false, GlobalEnums.ActiveOption.Active).Where(w => w.IsDefault).FirstOrDefault();

            return goodsReceiptIndexes;
        }

        public BatchMasterBase GetBatchMasterBase(string code)
        {
            return this.batchMasterAPIRepository.GetBatchMasterBase(code);
        }

        public BatchMasterBase GetBatchMasterBase(int batchMasterID)
        {
            return this.batchMasterAPIRepository.GetBatchMasterBase(batchMasterID);
        }
       
        public IList<BatchMasterBase> GetBatchMasterBases()
        {
            return this.batchMasterAPIRepository.GetBatchMasterBases();
        }

        public IList<BatchMasterTree> GetBatchMasterTrees(DateTime fromDate, DateTime toDate)
        {
            return this.batchMasterAPIRepository.GetBatchMasterTrees(fromDate, toDate);
        }
    }
}
