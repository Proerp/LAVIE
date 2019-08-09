using System.Linq;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Commons;


namespace TotalDAL.Repositories.Commons
{
    public class LavieRepository : GenericRepository<Lavie>, ILavieRepository
    {
        public LavieRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "LavieEditable")
        {
        }
    }





    public class LavieAPIRepository : GenericAPIRepository, ILavieAPIRepository
    {
        public LavieAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetLavieIndexes")
        {
        }

        public void LavieUpdate(int lavieID)
        {
            this.TotalSmartCodingEntities.LavieUpdate(lavieID);
        }

        public void LavieDoEmpty()
        {
            this.TotalSmartCodingEntities.LavieDoEmpty();
        }
    }
}
