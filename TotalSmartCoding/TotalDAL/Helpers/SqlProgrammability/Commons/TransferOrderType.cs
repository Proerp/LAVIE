using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class TransferOrderType
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public TransferOrderType(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetTransferOrderTypeIndexes();

            this.GetTransferOrderTypeBases();
        }


        private void GetTransferOrderTypeIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TransferOrderTypes.TransferOrderTypeID, TransferOrderTypes.Code, TransferOrderTypes.Name " + "\r\n";
            queryString = queryString + "       FROM        TransferOrderTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTransferOrderTypeIndexes", queryString);
        }


        private void GetTransferOrderTypeBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TransferOrderTypeID, Code, Name " + "\r\n";
            queryString = queryString + "       FROM        TransferOrderTypes " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTransferOrderTypeBases", queryString);
        }

    }
}
