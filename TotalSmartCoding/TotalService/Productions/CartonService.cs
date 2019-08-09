using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;

using TotalBase;
using TotalModel.Models;
using TotalDTO.Productions;
using TotalCore.Repositories.Productions;
using TotalCore.Services.Productions;

namespace TotalService.Productions
{
    public class CartonService : GenericService<Carton, CartonDTO, CartonPrimitiveDTO>, ICartonService
    {
        ICartonRepository cartonRepository;
        public CartonService(ICartonRepository cartonRepository)
            : base(cartonRepository, "CartonPostSaveValidate", "CartonSaveRelative")
        {
            this.cartonRepository = cartonRepository;
        }

        protected override ObjectParameter[] SaveRelativeParameters(Carton entity, SaveRelativeOption saveRelativeOption)
        {
            ObjectParameter[] baseParameters = base.SaveRelativeParameters(entity, saveRelativeOption); //IMPORTANT: WE SHOULD SET PackIDs WHEN SaveRelativeOption.Update. WE DON'T CARE PackIDs WHEN SaveRelativeOption.Undo [SEE STORE PROCEDURE CartonSaveRelative FOR MORE INFORMATION] 
            ObjectParameter[] objectParameters = new ObjectParameter[] { baseParameters[0], baseParameters[1], new ObjectParameter("PackIDs", this.ServiceBag.ContainsKey("PackIDs") && this.ServiceBag["PackIDs"] != null ? this.ServiceBag["PackIDs"] : ""), new ObjectParameter("DeletePack", this.ServiceBag.ContainsKey("DeletePack") && this.ServiceBag["DeletePack"] != null ? true : false), new ObjectParameter("Remarks", this.ServiceBag.ContainsKey("Remarks") && this.ServiceBag["Remarks"] != null ? this.ServiceBag["Remarks"] : "") };

            this.ServiceBag.Remove("PackIDs");
            this.ServiceBag.Remove("DeletePack");
            this.ServiceBag.Remove("Remarks");
            
            return objectParameters;
        }

        protected override bool TryValidateModel(CartonDTO dto, ref System.Text.StringBuilder invalidMessage)
        {
            if (!base.TryValidateModel(dto, ref invalidMessage)) return false;

            if (this.ServiceBag.ContainsKey("EntryStatusIDs") && this.ServiceBag["EntryStatusIDs"] != null && (this.ServiceBag["EntryStatusIDs"]).ToString().IndexOf(dto.EntryStatusID.ToString()) < 0) { invalidMessage.Append("Trạng thái carton không phù hợp [" + dto.EntryStatusID + "]"); return false; }

            return true;
        }

        public IList<Carton> GetCartons(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? palletID)
        {
            return this.cartonRepository.GetCartons(fillingLineID, entryStatusIDs, palletID);
        }

        public bool UpdateEntryStatus(string cartonIDs, GlobalVariables.BarcodeStatus barcodeStatus)
        {
            try
            {
                this.cartonRepository.UpdateEntryStatus(cartonIDs, barcodeStatus);
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
