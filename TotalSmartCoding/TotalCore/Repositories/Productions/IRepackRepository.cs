using System.Collections.Generic;

using TotalBase;
using TotalModel.Models;

namespace TotalCore.Repositories.Productions
{
    public interface IRepackRepository : IGenericRepository<Repack>
    {
        IList<BatchRepack> LookupRepacks(string barcode);
        IList<BatchRepack> LookupRecartons(int cartonID);

        void RepackRollback(int batchID, int repackID);
    }
}
