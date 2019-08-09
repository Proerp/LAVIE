using System;

using TotalBase;
using TotalModel.Models;

using TotalDTO.Productions;

namespace TotalCore.Services.Productions
{
    public interface IBatchMasterService : IGenericService<BatchMaster, BatchMasterDTO, BatchMasterPrimitiveDTO>
    {
        bool AddLot(int batchMasterID, DateTime? entryDate);
        bool RemoveLot(int lotID);        
    }
}
