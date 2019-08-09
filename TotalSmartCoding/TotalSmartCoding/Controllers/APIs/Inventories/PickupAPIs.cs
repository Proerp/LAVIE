using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalCore.Repositories.Inventories;

namespace TotalSmartCoding.Controllers.APIs.Inventories
{
    public class PickupAPIs
    {
        private readonly IPickupAPIRepository pickupAPIRepository;

        public PickupAPIs(IPickupAPIRepository pickupAPIRepository)
        {
            this.pickupAPIRepository = pickupAPIRepository;
        }


        public ICollection<PickupIndex> GetPickupIndexes()
        {
            return this.pickupAPIRepository.GetEntityIndexes<PickupIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
        }

        public List<PendingPallet> GetPendingPallets(int? locationID, int? fillingLineID, int? pickupID, string palletIDs, bool isReadonly)
        {
            return this.pickupAPIRepository.GetPendingPallets(locationID, fillingLineID, pickupID, palletIDs, isReadonly);
        }
    }
}
