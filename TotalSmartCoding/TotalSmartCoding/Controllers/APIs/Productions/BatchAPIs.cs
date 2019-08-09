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
    public class BatchAPIs
    {
        private readonly IBatchAPIRepository batchAPIRepository;

        public BatchAPIs(IBatchAPIRepository batchAPIRepository)
        {
            this.batchAPIRepository = batchAPIRepository;
        }


        public ICollection<BatchIndex> GetBatchIndexes(int batchID, bool showCummulativePacks, GlobalEnums.ActiveOption activeOption, bool defaultOnly)
        {
            this.batchAPIRepository.RepositoryBag["BatchID"] = batchID;
            this.batchAPIRepository.RepositoryBag["ShowCummulativePacks"] = showCummulativePacks ? 1 : 0;
            this.batchAPIRepository.RepositoryBag["ActiveOption"] = (int)activeOption;
            this.batchAPIRepository.RepositoryBag["DefaultOnly"] = defaultOnly ? 1 : 0;
            ICollection<BatchIndex> goodsReceiptIndexes = this.batchAPIRepository.GetEntityIndexes<BatchIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);

            return goodsReceiptIndexes;
        }

        public BatchIndex GetActiveBatchIndex()
        {
            BatchIndex goodsReceiptIndexes = this.GetBatchIndexes(0, false, GlobalEnums.ActiveOption.Active, true).Where(w => w.IsDefault).FirstOrDefault();

            return goodsReceiptIndexes;
        }

        public BatchIndex GetBatchByID(int batchID)
        {
            BatchIndex goodsReceiptIndexes = this.GetBatchIndexes(batchID, false, GlobalEnums.ActiveOption.Both, false).FirstOrDefault();

            return goodsReceiptIndexes;
        }

        public List<PendingLot> GetPendingLots(int? locationID, int? fillingLineID)
        {
            return this.batchAPIRepository.GetPendingLots(locationID, fillingLineID);
        }

        public List<BatchRepack> GetBatchRepacks(int? batchID, bool notPrintedOnly)
        {
            return this.batchAPIRepository.GetBatchRepacks(batchID, notPrintedOnly);
        }

        public IList<BatchAvailable> GetBatchAvailables(int? locationID, int? deliveryAdviceID, int? transferOrderID, int? commodityID, bool withNullRow)
        {
            return this.batchAPIRepository.GetBatchAvailables(locationID, deliveryAdviceID, transferOrderID, commodityID, withNullRow);
        }
    }
}
