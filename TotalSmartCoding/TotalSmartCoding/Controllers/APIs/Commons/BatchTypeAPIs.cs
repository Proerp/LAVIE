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
    public class BatchTypeAPIs
    {
        private readonly IBatchTypeAPIRepository batchTypeAPIRepository;

        public BatchTypeAPIs(IBatchTypeAPIRepository batchTypeAPIRepository)
        {
            this.batchTypeAPIRepository = batchTypeAPIRepository;
        }


        public ICollection<BatchTypeIndex> GetBatchTypeIndexes()
        {
            return this.batchTypeAPIRepository.GetEntityIndexes<BatchTypeIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<BatchTypeBase> GetBatchTypeBases()
        {
            return this.batchTypeAPIRepository.GetBatchTypeBases();
        }

        public IList<BatchTypeTree> GetBatchTypeTrees()
        {
            return this.batchTypeAPIRepository.GetBatchTypeTrees();
        }
    }
}
