using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Productions;
using System.Data.Entity.Core.Objects;
using TotalBase;

namespace TotalDAL.Repositories.Productions
{
    public class BatchMasterRepository : GenericRepository<BatchMaster>, IBatchMasterRepository
    {
        public BatchMasterRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "BatchMasterEditable", "BatchMasterApproved")
        {
        }

        public void AddLot(int batchMasterID, DateTime? entryDate)
        {
            this.TotalSmartCodingEntities.BatchMasterAddLot(batchMasterID, entryDate);
        }

        public void RemoveLot(int lotID)
        {
            this.TotalSmartCodingEntities.BatchMasterRemoveLot(lotID);
        }
    }








    public class BatchMasterAPIRepository : GenericAPIRepository, IBatchMasterAPIRepository
    {
        public BatchMasterAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetBatchMasterIndexes")
        {
        }

        protected override ObjectParameter[] GetEntityIndexParameters(int userID, DateTime fromDate, DateTime toDate)
        {
            ObjectParameter[] baseParameters = base.GetEntityIndexParameters(userID, fromDate, toDate);

            return new ObjectParameter[] { baseParameters[0], baseParameters[1], baseParameters[2], new ObjectParameter("ShowCummulativePacks", (int)(this.RepositoryBag["ShowCummulativePacks"] != null ? this.RepositoryBag["ShowCummulativePacks"] : 0)), new ObjectParameter("ActiveOption", (int)(this.RepositoryBag["ActiveOption"] != null ? this.RepositoryBag["ActiveOption"] : GlobalEnums.ActiveOption.Both)) };
        }

        public BatchMasterBase GetBatchMasterBase(string code)
        {
            return this.TotalSmartCodingEntities.GetBatchMasterBaseByCode(code).FirstOrDefault();
        }

        public BatchMasterBase GetBatchMasterBase(int batchMasterID)
        {
            return this.TotalSmartCodingEntities.GetBatchMasterBase(batchMasterID).FirstOrDefault();
        }

        public IList<BatchMasterBase> GetBatchMasterBases()
        {
            return this.TotalSmartCodingEntities.GetBatchMasterBases().ToList();
        }

        public IList<BatchMasterTree> GetBatchMasterTrees(DateTime fromDate, DateTime toDate)
        {
            return this.TotalSmartCodingEntities.GetBatchMasterTrees(fromDate, toDate).ToList();
        }
    }


}
