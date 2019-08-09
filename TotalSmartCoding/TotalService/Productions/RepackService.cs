using System.Text;

using TotalBase;
using TotalModel.Models;
using TotalDTO.Productions;
using TotalCore.Repositories.Productions;
using TotalCore.Services.Productions;
using System;
using System.Collections.Generic;

namespace TotalService.Productions
{
    public class RepackService : GenericService<Repack, RepackDTO, RepackPrimitiveDTO>, IRepackService
    {
        private IRepackRepository repackRepository;
        public RepackService(IRepackRepository repackRepository)
            : base(repackRepository, "RepackPostSaveValidate")
        {
            this.repackRepository = repackRepository;
        }

        public IList<BatchRepack> LookupRepacks(string barcode)
        {
            return this.repackRepository.LookupRepacks(barcode);
        }

        public IList<BatchRepack> LookupRecartons(int cartonID)
        {
            return this.repackRepository.LookupRecartons(cartonID);
        }

        public bool RepackRollback(int batchID, int repackID)
        {
            try
            {
                this.repackRepository.RepackRollback(batchID, repackID);
                return true;
            }
            catch (Exception ex)
            {
                this.ServiceTag = ex.Message;
                return false;
            }
        }

        protected override bool TryValidateModel(RepackDTO dto, ref StringBuilder invalidMessage)
        {
            if (!base.TryValidateModel(dto, ref invalidMessage)) return false;
            // cần phải ktra DTO here in order to save: có nên kết hợp IsValid của DTO để ktra ngay trong GenericService cho tất cả DTO object??? if (dto.EntryDate < new DateTime(2015, 7, 1) || dto.EntryDate > DateTime.Today.AddDays(2)) invalidMessage.Append(" Ngày không hợp lệ;");
            return true;
        }
    }
}
