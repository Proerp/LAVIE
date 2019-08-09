using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;
using TotalDTO.Productions;

namespace TotalCore.Services.Productions
{
    public interface IPalletService : IGenericService<Pallet, PalletDTO, PalletPrimitiveDTO>
    {
        bool GetLocked(int palletID);

        bool GetPalletChanged(GlobalVariables.FillingLine fillingLineID);
        IList<Pallet> GetPallets(GlobalVariables.FillingLine fillingLineID, int batchID, string entryStatusIDs);
    }
}
