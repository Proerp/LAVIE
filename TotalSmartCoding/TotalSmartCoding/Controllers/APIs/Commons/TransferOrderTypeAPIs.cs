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
    public class TransferOrderTypeAPIs
    {
        private readonly ITransferOrderTypeAPIRepository transferOrderTypeAPIRepository;

        public TransferOrderTypeAPIs(ITransferOrderTypeAPIRepository transferOrderTypeAPIRepository)
        {
            this.transferOrderTypeAPIRepository = transferOrderTypeAPIRepository;
        }


        public ICollection<TransferOrderTypeIndex> GetTransferOrderTypeIndexes()
        {
            return this.transferOrderTypeAPIRepository.GetEntityIndexes<TransferOrderTypeIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }

        public IList<TransferOrderTypeBase> GetTransferOrderTypeBases()
        {
            return this.transferOrderTypeAPIRepository.GetTransferOrderTypeBases();
        }

        public IList<TransferPackageTypeBase> GetTransferPackageTypeBases()
        {
            List<TransferPackageTypeBase> transferPackageTypes = new List<TransferPackageTypeBase>();
            transferPackageTypes.Add(new TransferPackageTypeBase() { TransferPackageTypeID = (int)GlobalEnums.TransferPackageTypeID.Pallets, Code = GlobalEnums.TransferPackageTypeID.Pallets.ToString(), Name = "Giữ nguyên pallet khi chuyển kho" });
            transferPackageTypes.Add(new TransferPackageTypeBase() { TransferPackageTypeID = (int)GlobalEnums.TransferPackageTypeID.Cartons, Code = GlobalEnums.TransferPackageTypeID.Cartons.ToString(), Name = "Xã pallet khi chuyển kho" });
            return transferPackageTypes;
        }
    }
}
