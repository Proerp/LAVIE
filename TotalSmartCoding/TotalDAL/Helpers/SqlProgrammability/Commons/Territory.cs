using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class Territory
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Territory(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetTerritoryIndexes();

            //this.TerritoryEditable(); 
            //this.TerritorySaveRelative();

            this.GetTerritoryBases();
        }


        private void GetTerritoryIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Territories.TerritoryID, Territories.Name " + "\r\n";
            queryString = queryString + "       FROM        Territories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTerritoryIndexes", queryString);
        }


        private void TerritorySaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO TerritoryTerritories (TerritoryID, TerritoryID, TerritoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      TerritoryID, 46 AS TerritoryID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS TerritoryTaskID, GETDATE(), '', 0 FROM Territories WHERE TerritoryID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO TerritoryTerritories (TerritoryID, TerritoryID, TerritoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      Territories.TerritoryID, Territories.TerritoryID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS TerritoryTaskID, GETDATE(), '', 0 FROM Territories INNER JOIN Territories ON Territories.TerritoryID = @EntityID AND Territories.TerritoryCategoryID NOT IN (4, 5, 7, 9, 10, 11, 12) AND Territories.TerritoryCategoryID = Territories.TerritoryCategoryID " + "\r\n";

            queryString = queryString + "               INSERT INTO TerritoryTerritories (TerritoryID, TerritoryID, TerritoryTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      TerritoryID, 82 AS TerritoryID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS TerritoryTaskID, GETDATE(), '', 0 FROM Territories WHERE TerritoryID = @EntityID AND TerritoryCategoryID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      TerritoryTerritories WHERE TerritoryID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("TerritorySaveRelative", queryString);
        }


        private void TerritoryEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = TerritoryID FROM Territories WHERE TerritoryID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = TerritoryID FROM GoodsIssueDetails WHERE TerritoryID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("TerritoryEditable", queryArray);
        }


        private void GetTerritoryBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      TerritoryID, Name, EntireName " + "\r\n";
            queryString = queryString + "       FROM        EntireTerritories " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetTerritoryBases", queryString);
        }

    }
}
