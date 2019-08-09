using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalModel.Models;
using TotalCore.Repositories.Generals;

namespace TotalDAL.Repositories.Generals
{
    public class ReportRepository : GenericRepository<Report>, IReportRepository
    {
        public ReportRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities)
        {
        }
    }

    public class ReportAPIRepository : GenericAPIRepository, IReportAPIRepository
    {
        public ReportAPIRepository(TotalSmartCodingEntities totalSmartCodingEntities)
            : base(totalSmartCodingEntities, "GetReportIndexes")
        {
        }
    }
}
