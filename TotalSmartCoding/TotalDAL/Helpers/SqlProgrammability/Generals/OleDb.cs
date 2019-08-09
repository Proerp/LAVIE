using System;
using System.Text;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalDAL.Helpers.SqlProgrammability.Generals
{    
    public class OleDb
    {
        private readonly TotalSmartCodingEntities totalSmartCodingEntities;

        public OleDb(TotalSmartCodingEntities totalSmartCodingEntities)
        {
            this.totalSmartCodingEntities = totalSmartCodingEntities;
        }

        public void RestoreProcedure()
        {
            this.GetColumnMappings();
            this.SaveColumnMapping();
        }

        private void GetColumnMappings()
        {
            string queryString;

            queryString = " @MappingTaskID int " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       SELECT      ColumnMappingID, MappingTaskID, ColumnID, ColumnName, ColumnDisplayName, ColumnMappingName, SerialID, OrderBy, ImportedDate " + "\r\n";
            queryString = queryString + "       FROM        ColumnMappings " + "\r\n";
            queryString = queryString + "       WHERE       MappingTaskID = @MappingTaskID" + "\r\n";
            queryString = queryString + "       ORDER BY    SerialID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("GetColumnMappings", queryString);
        }

        private void SaveColumnMapping()
        {
            string queryString;

            queryString = " @ColumnMappingID int, @ColumnMappingName nvarchar(50) " + "\r\n";
            queryString = queryString + " WITH ENCRYPTION " + "\r\n";
            queryString = queryString + " AS " + "\r\n";
            queryString = queryString + "    BEGIN " + "\r\n";

            queryString = queryString + "       UPDATE      ColumnMappings " + "\r\n";
            queryString = queryString + "       SET         ColumnMappingName = @ColumnMappingName, ImportedDate = Getdate() " + "\r\n";
            queryString = queryString + "       WHERE       ColumnMappingID = @ColumnMappingID " + "\r\n";

            queryString = queryString + "    END " + "\r\n";

            this.totalSmartCodingEntities.CreateStoredProcedure("SaveColumnMapping", queryString);
        }

    }
}
