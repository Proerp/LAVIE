using System.Linq;
using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Productions;

namespace TotalDAL.Repositories.Productions
{
    public class CartonRepository : GenericRepository<Carton>, ICartonRepository
    {
        public CartonRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities)
        {
        }


        public IList<Carton> GetCartons(GlobalVariables.FillingLine fillingLineID, string entryStatusIDs, int? palletID)
        {
            return this.TotalSmartCodingEntities.GetCartons((int)fillingLineID, entryStatusIDs, palletID).ToList();
        }

        public IList<Carton> SearchCartons(string barcode)
        {
            return this.TotalSmartCodingEntities.SearchCartons(barcode).ToList();
        }

        public void UpdateEntryStatus(string cartonIDs, GlobalVariables.BarcodeStatus barcodeStatus)
        {
            this.TotalSmartCodingEntities.CartonUpdateEntryStatus(cartonIDs, (int)barcodeStatus);
        }
    }
}
