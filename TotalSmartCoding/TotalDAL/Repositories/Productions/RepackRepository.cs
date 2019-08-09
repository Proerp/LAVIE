using System.Linq;
using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Productions;

namespace TotalDAL.Repositories.Productions
{
    public class RepackRepository : GenericRepository<Repack>, IRepackRepository
    {
        public RepackRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities)
        {
        }

        public IList<BatchRepack> LookupRepacks(string barcode)
        {
            return this.TotalSmartCodingEntities.LookupRepacks(barcode).ToList();
        }

        public IList<BatchRepack> LookupRecartons(int cartonID)
        {
            return this.TotalSmartCodingEntities.LookupRecartons(cartonID).ToList();
        }

        public void RepackRollback(int batchID, int repackID)
        {
            this.TotalSmartCodingEntities.RepackRollback(batchID, repackID);
        }
        
    }
}
