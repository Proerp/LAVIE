using System.Collections.Generic;
using System.Data;
using TotalBase.Enums;
using TotalModel.Models;

namespace TotalCore.Repositories.Generals
{
    public interface IOleDbAPIRepository : IGenericAPIRepository
    {
        GlobalEnums.MappingTaskID MappingTaskID { get; set; }

        List<string> GetExcelSheets(string excelFile);

        DataTable OpenExcelSheet(string excelFile, string sheetName);
        DataTable OpenExcelSheet(string excelFile, string sheetName, string querySelect);
        DataTable OpenExcelSheet(string excelFile, string sheetName, string querySelect, string queryWhere, string queryOrderBy);

        IList<ColumnMapping> GetColumnMappings();
        void SaveColumnMapping(int columnMappingID, string columnMappingName);
    }
}
