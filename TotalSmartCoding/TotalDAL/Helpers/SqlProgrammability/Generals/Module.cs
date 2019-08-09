using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{
    public class Module
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public Module(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetModuleIndexes();
            this.GetModuleDetailIndexes();

            this.GetModuleViewDetails();

            //this.ModuleEditable(); 
            //this.ModuleSaveRelative();

            
        }


        private void GetModuleIndexes()
        {
            string queryString;

            queryString = " @UserID Int, @FromDate DateTime, @ToDate DateTime " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Modules.ModuleID, Modules.Code, Modules.Name " + "\r\n";
            queryString = queryString + "       FROM        Modules " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetModuleIndexes", queryString);
        }

        private void GetModuleDetailIndexes()
        {
            string queryString;

            queryString = " " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      Modules.ModuleID, Modules.Name AS ModuleName, ModuleDetails.ModuleDetailID, ModuleDetails.Name AS ModuleDetailName " + "\r\n";
            queryString = queryString + "       FROM        Modules INNER JOIN ModuleDetails ON Modules.ModuleID = ModuleDetails.ModuleID " + "\r\n";
            queryString = queryString + "       WHERE      (Modules.InActive = 0) AND (ModuleDetails.InActive = 0) " + "\r\n";
            queryString = queryString + "       ORDER BY    Modules.SerialID, ModuleDetails.SerialID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetModuleDetailIndexes", queryString);
        }

        private void ModuleSaveRelative()
        {
            string queryString = " @EntityID int, @SaveRelativeOption int " + "\r\n"; //SaveRelativeOption: 1: Update, -1:Undo
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       IF (@SaveRelativeOption = 1) " + "\r\n";
            queryString = queryString + "           BEGIN " + "\r\n";

            //queryString = queryString + "               INSERT INTO ModuleModules (ModuleID, ModuleID, ModuleTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            //queryString = queryString + "               SELECT      ModuleID, 46 AS ModuleID, " + (int)GlobalEnums.NmvnTaskID.Module + " AS ModuleTaskID, GETDATE(), '', 0 FROM Modules WHERE ModuleID = @EntityID " + "\r\n";

            //queryString = queryString + "               INSERT INTO ModuleModules (ModuleID, ModuleID, ModuleTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            //queryString = queryString + "               SELECT      Modules.ModuleID, Modules.ModuleID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS ModuleTaskID, GETDATE(), '', 0 FROM Modules INNER JOIN Modules ON Modules.ModuleID = @EntityID AND Modules.ModuleCategoryID NOT IN (4, 5, 7, 9, 10, 11, 12) AND Modules.ModuleCategoryID = Modules.ModuleCategoryID " + "\r\n";

            //queryString = queryString + "               INSERT INTO ModuleModules (ModuleID, ModuleID, ModuleTaskID, EntryDate, Remarks, InActive) " + "\r\n";
            //queryString = queryString + "               SELECT      ModuleID, 82 AS ModuleID, " + (int)GlobalEnums.NmvnTaskID.DeliveryAdvice + " AS ModuleTaskID, GETDATE(), '', 0 FROM Modules WHERE ModuleID = @EntityID AND ModuleCategoryID IN (4, 5, 7, 9, 10, 11, 12) " + "\r\n";

            queryString = queryString + "           END " + "\r\n";

            queryString = queryString + "       ELSE " + "\r\n"; //(@SaveRelativeOption = -1) 
            queryString = queryString + "           DELETE      ModuleModules WHERE ModuleID = @EntityID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("ModuleSaveRelative", queryString);
        }


        private void ModuleEditable()
        {
            string[] queryArray = new string[0];

            //queryArray[0] = " SELECT TOP 1 @FoundEntity = ModuleID FROM Modules WHERE ModuleID = @EntityID AND (InActive = 1 OR InActivePartial = 1)"; //Don't allow approve after void
            //queryArray[1] = " SELECT TOP 1 @FoundEntity = ModuleID FROM GoodsIssueDetails WHERE ModuleID = @EntityID ";

            this.totalSmartCodingEntities.CreateProcedureToCheckExisting("ModuleEditable", queryArray);
        }


        private void GetModuleViewDetails()
        {
            string queryString;

            queryString = " @ModuleID Int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT ModuleDetailID, ModuleID, Code, Name, FullName, Actions, Controller, LastOpen, SerialID, ImageIndex, InActive FROM ModuleDetails WHERE ModuleID = @ModuleID ORDER BY SerialID" + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetModuleViewDetails", queryString);
        }

    }
}
