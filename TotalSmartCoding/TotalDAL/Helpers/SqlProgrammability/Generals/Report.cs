using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{
    public class Report
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Report(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetReportIndexes();
        }


        private void GetReportIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      ReportID, ReportUniqueID, ReportGroupID, UPPER(ReportGroupName) AS ReportGroupName, ReportTabPageIDs, ReportName, ReportTypeID " + "\r\n";
            queryString = queryString + "       FROM        Reports " + "\r\n";
            queryString = queryString + "       ORDER BY    ReportGroupName, SerialID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetReportIndexes", queryString);
        }
    }
}
