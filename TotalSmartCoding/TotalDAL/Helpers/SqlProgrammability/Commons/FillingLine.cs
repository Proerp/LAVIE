using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Commons
{
    public class FillingLine
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public FillingLine(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetFillingLineIndexes();

            //this.FillingLineEditable(); 
            //this.FillingLineSaveRelative();

            this.GetFillingLineBases();
            this.GetFillingLineTrees();
        }


        private void GetFillingLineIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      FillingLines.FillingLineID, FillingLines.Code, FillingLines.Name, FillingLines.NickName " + "\r\n";
            queryString = queryString + "       FROM        FillingLines " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetFillingLineIndexes", queryString);
        }


        private void FillingLineSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            queryString = queryString + "               INSERT INTO FillingLineFillingLines (FillingLineID, FillingLineID, FillingLineTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      FillingLineID, 46 AS FillingLineID, " + (int)GlobalEnums.NmvnTaskID.SalesOrder + " AS FillingLineTaskID, GETDATE(), '', 0 FROM FillingLines WHERE FillingLineID = @EntityID " + "\r\n";

            queryString = queryString + "               INSERT INTO FillingLineFillingLines (FillingLineID, FillingLineID, FillingLineTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      FillingLines.FillingLineID, FillingLines.FillingLineID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS FillingLineTaskID, GETDATE(), '', 0 FROM FillingLines INNER JOIN FillingLines ON FillingLines.FillingLineID = @EntityID AND FillingLines.FillingLineCategoryID NOT IN (4, 5, 7, 9, 10, 11, 12) AND FillingLines.FillingLineCategoryID = FillingLines.FillingLineCategoryID " + "\r\n";

            queryString = queryString + "               INSERT INTO FillingLineFillingLines (FillingLineID, FillingLineID, FillingLineTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            queryString = queryString + "               SELECT      FillingLineID, 82 AS FillingLineID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS FillingLineTaskID, GETDATE(), '', 0 FROM FillingLines WHERE FillingLineID = @EntityID AND FillingLineCategoryID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      FillingLineFillingLines WHERE FillingLineID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("FillingLineSaveRelative", queryString);
        }


        private void FillingLineEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = FillingLineID FROM FillingLines WHERE FillingLineID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = FillingLineID FROM GoodsIssueDetails WHERE FillingLineID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("FillingLineEditable", queryArray);
        }


        private void GetFillingLineBases()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      FillingLineID, Code, Name, NickName, FactoryCode " + "\r\n";
            queryString = queryString + "       FROM        FillingLines " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetFillingLineBases", queryString);
        }

        private void GetFillingLineTrees()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      " + GlobalEnums.RootNode + " AS NodeID, 0 AS ParentNodeID, NULL AS PrimaryID, NULL AS AncestorID, '[All]' AS Code, NULL AS Name, NULL AS ParameterName, CAST(1 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       UNION ALL " + "\r\n";
            queryString = queryString + "       SELECT      " + GlobalEnums.AncestorNode + " + FillingLineID AS NodeID, " + GlobalEnums.RootNode + " + 0 AS ParentNodeID, FillingLineID AS PrimaryID, NULL AS AncestorID, Name AS Code, N'' AS Name, 'FillingLineID' AS ParameterName, CAST(0 AS bit) AS Selected " + "\r\n";
            queryString = queryString + "       FROM        FillingLines " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetFillingLineTrees", queryString);

        }

    }
}
