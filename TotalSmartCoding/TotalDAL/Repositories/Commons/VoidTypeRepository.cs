using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;

namespace TotalDAL.Repositories.Commons
{
    public class VoidTypeRepository : GenericRepository<VoidType>, IVoidTypeRepository
    {
        public VoidTypeRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities)
        {
        }

        public IList<VoidType> SearchVoidTypes(string searchText)
        {
            this.TotalSmartCodingEntities.Configuration.ProxyCreationEnabled = false;
            List<VoidType> voidTypes = this.TotalSmartCodingEntities.VoidTypes.Where(w => (w.Code.Contains(searchText) || w.Name.Contains(searchText))).OrderByDescending(or => or.Name).Take(3).ToList();
            this.TotalSmartCodingEntities.Configuration.ProxyCreationEnabled = true;

            return voidTypes;

        }

        public IList<string> GetVoidTypeNames()
        {
            this.TotalSmartCodingEntities.Configuration.ProxyCreationEnabled = false;
            List<string> voidTypeNames = this.TotalSmartCodingEntities.VoidTypes.OrderBy(o => o.Name).Select(s => s.Name).ToList();
            this.TotalSmartCodingEntities.Configuration.ProxyCreationEnabled = true;

            return voidTypeNames;
        }

    }
}
