using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Commons;
using TotalBase;

namespace TotalSmartCoding.Controllers.APIs.Commons
{
    public class BatchStatusAPIs
    {
        private readonly IBatchStatusAPIRepository batchStatusAPIRepository;

        public BatchStatusAPIs(IBatchStatusAPIRepository batchStatusAPIRepository)
        {
            this.batchStatusAPIRepository = batchStatusAPIRepository;
        }


        public ICollection<BatchStatusIndex> GetBatchStatusIndexes()
        {
            return this.batchStatusAPIRepository.GetEntityIndexes<BatchStatusIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public BatchStatusBase GetBatchStatusBase(int batchStatusID)
        {
            return this.batchStatusAPIRepository.GetBatchStatusBase(batchStatusID);
        }

        public BatchStatusBase GetBatchStatusBase(string code)
        {
            return this.batchStatusAPIRepository.GetBatchStatusBase(code);
        }

        public IList<BatchStatusBase> GetBatchStatusBases()
        {
            return this.batchStatusAPIRepository.GetBatchStatusBases();
        }

    }
}
