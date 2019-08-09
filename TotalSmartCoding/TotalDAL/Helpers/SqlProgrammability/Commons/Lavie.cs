using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDAL.Helpers.SqlProgrammability.Sales;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class Lavie
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Lavie(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetLavieIndexes();

            this.LavieEditable();
            this.LavieSaveRelative();

            this.LavieUpdate();
            this.LavieDoEmpty();
        }


        private void GetLavieIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      LavieID, SerialID, EntryDate, ItemNumber, ProductName, GTIN, PalletID, BatchNumber, ExpirationDate, Qty, Layers, GTINBarcode, Barcode, Remarks, PrintedTimes " + "\r\n";
            queryString = queryString + "       FROM        Lavies " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetLavieIndexes", queryString);
        }


        private void LavieSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("LavieSaveRelative", queryString);
        }


        private void LavieEditable()
        {
            string[] queryArray = new string[10];

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("LavieEditable", queryArray);
        }

        private void LavieUpdate()
        {
            string queryString = " @LavieID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "       UPDATE      Lavies " + "\r\n";
            queryString = queryString + "       SET         PrintedTimes = 1 " + "\r\n";
            queryString = queryString + "       WHERE       LavieID = @LavieID " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("LavieUpdate", queryString);
        }

        private void LavieDoEmpty()
        {
            string queryString = " " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "   DELETE FROM Lavies " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("LavieDoEmpty", queryString);
        }
    }
}
