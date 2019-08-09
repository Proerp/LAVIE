using System;

using TotalModel.Models;
using TotalDTO.Productions;
using TotalCore.Repositories.Productions;
using TotalCore.Services.Productions;

namespace TotalService.Productions
{
    public class BatchService : GenericService<Batch, BatchDTO, BatchPrimitiveDTO>, IBatchService
    {
        private IBatchRepository batchRepository;
        public BatchService(IBatchRepository batchRepository)
            : base(batchRepository, "BatchPostSaveValidate", "BatchSaveRelative", "BatchToggleApproved", "BatchToggleVoid", "BatchToggleLocked")
        {
            this.batchRepository = batchRepository;
        }


        public override bool Approvable(BatchDTO dto)
        {
            if (dto.NoApprovable || this.GlobalLocked(dto)) return false;
            if (dto.Approved || !this.GetApprovalPermitted(dto.OrganizationalUnitID)) return false;

            return !this.batchRepository.GetLocked(dto.GetID()); // SPECIAL OVERWRITE FOR THIS CLASS BatchService TO IGNORE EDITABLE WHEN CHECK Approvable
        }


        public bool CommonUpdate(int batchID, string nextPackNo, string nextCartonNo, string nextPalletNo)
        {
            try
            {
                this.batchRepository.CommonUpdate(batchID, nextPackNo, nextCartonNo, nextPalletNo);
                return true;
            }
            catch (Exception ex)
            {
                this.ServiceTag = ex.Message;
                return false;
            }
        }

        public bool RepackDelete(int batchID)
        {
            this.batchRepository.RepackDelete(batchID);
            return true;
        }

        public bool RepackUpdate(int batchID, int repackID)
        {
            try
            {
                this.batchRepository.RepackUpdate(batchID, repackID);
                return true;
            }
            catch (Exception ex)
            {
                this.ServiceTag = ex.Message;
                return false;
            }
        }

        public bool RepackReprint(int repackID)
        {
            try
            {
                return this.batchRepository.RepackReprint(repackID);
            }
            catch (Exception ex)
            {
                this.ServiceTag = ex.Message;
                return false;
            }
        }

        public bool AddLot(int batchID)
        {
            try
            {
                this.batchRepository.AddLot(batchID);
                return true;
            }
            catch (Exception ex)
            {
                this.ServiceTag = ex.Message;
                return false;
            }
        }
    }
}
