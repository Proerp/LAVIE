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
    public class PalletService : GenericService<Pallet, PalletDTO, PalletPrimitiveDTO>, IPalletService
    {
        IPalletRepository palletRepository;
        public PalletService(IPalletRepository palletRepository)
            : base(palletRepository, "PalletPostSaveValidate", "PalletSaveRelative", null, null, "PalletToggleLocked")
        {
            this.palletRepository = palletRepository;
        }

        protected override ObjectParameter[] SaveRelativeParameters(Pallet entity, SaveRelativeOption saveRelativeOption)
        {
            ObjectParameter[] baseParameters = base.SaveRelativeParameters(entity, saveRelativeOption); //IMPORTANT: WE SHOULD SET CartonIDs WHEN SaveRelativeOption.Update. WE DON'T CARE CartonIDs WHEN SaveRelativeOption.Undo [SEE STORE PROCEDURE PalletSaveRelative FOR MORE INFORMATION] 
            ObjectParameter[] objectParameters = new ObjectParameter[] { baseParameters[0], baseParameters[1], new ObjectParameter("CartonIDs", this.ServiceBag["CartonIDs"] != null ? this.ServiceBag["CartonIDs"] : ""), new ObjectParameter("Remarks", this.ServiceBag.ContainsKey("Remarks") && this.ServiceBag["Remarks"] != null ? this.ServiceBag["Remarks"] : "") };

            this.ServiceBag.Remove("CartonIDs");
            this.ServiceBag.Remove("Remarks");

            return objectParameters;
        }


        public bool GetLocked(int palletID)
        {
            return this.palletRepository.GetLocked(palletID);
        }

        public bool GetPalletChanged(GlobalVariables.FillingLine fillingLineID)
        {
            return this.palletRepository.GetPalletChanged(fillingLineID);
        }

        public IList<Pallet> GetPallets(GlobalVariables.FillingLine fillingLineID, int batchID, string entryStatusIDs)
        {
            return this.palletRepository.GetPallets(fillingLineID, batchID, entryStatusIDs);
        }

        public bool UpdateEntryStatus(string palletIDs, GlobalVariables.BarcodeStatus barcodeStatus)
        {
            try
            {
                this.palletRepository.UpdateEntryStatus(palletIDs, barcodeStatus);
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
