using System.Text;
using System.Data.Entity.Core.Objects;

using TotalBase;
using TotalModel.Models;
using TotalDTO.Productions;
using TotalCore.Repositories.Productions;
using TotalCore.Services.Productions;
using System;
using System.Collections.Generic;

namespace TotalService.Productions
{
    public class PackService : GenericService<Pack, PackDTO, PackPrimitiveDTO>, IPackService
    {
        private IPackRepository packRepository;
        public PackService(IPackRepository packRepository)
            : base(packRepository, "PackPostSaveValidate", "PackSaveRelative")
        {
            this.packRepository = packRepository;
        }

        protected override System.Data.Entity.Core.Objects.ObjectParameter[] SaveRelativeParameters(Pack entity, SaveRelativeOption saveRelativeOption)
        {
            ObjectParameter[] baseParameters = base.SaveRelativeParameters(entity, saveRelativeOption);
            ObjectParameter[] objectParameters = new ObjectParameter[] { baseParameters[0], baseParameters[1], new ObjectParameter("Remarks", this.ServiceBag.ContainsKey("Remarks") && this.ServiceBag["Remarks"] != null ? this.ServiceBag["Remarks"] : "") };

            this.ServiceBag.Remove("Remarks");

            return objectParameters;
        }

        public IList<Pack> GetPacks(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? cartonID)
        {
            return this.packRepository.GetPacks(fillingLineID, entryStatusIDs, cartonID);
        }

        public int? GetRelatedPackID(int batchID, string barcode)
        {
            return this.packRepository.GetRelatedPackID(batchID, barcode);
        }

        public bool UpdateEntryStatus(string packIDs, GlobalVariables.BarcodeStatus barcodeStatus)
        {
            try
            {
                this.packRepository.UpdateEntryStatus(packIDs, barcodeStatus);
                return true;
            }
            catch (Exception ex)
            {
                this.ServiceTag = ex.Message;
                return false;
            }
        }
        public bool UpdateQueueID(string packIDs, int queueID)
        {
            try
            {
                this.packRepository.UpdateQueueID(packIDs, queueID);
                return true;
            }
            catch (Exception ex)
            {
                this.ServiceTag = ex.Message;
                return false;
            }
        }


        protected override bool TryValidateModel(PackDTO dto, ref StringBuilder invalidMessage)
        {
            if (!base.TryValidateModel(dto, ref invalidMessage)) return false;
            // cần phải ktra DTO here in order to save: có nên kết hợp IsValid của DTO để ktra ngay trong GenericService cho tất cả DTO object??? if (dto.EntryDate < new DateTime(2015, 7, 1) || dto.EntryDate > DateTime.Today.AddDays(2)) invalidMessage.Append(" Ngày không hợp lệ;");
            return true;
        }
    }
}
