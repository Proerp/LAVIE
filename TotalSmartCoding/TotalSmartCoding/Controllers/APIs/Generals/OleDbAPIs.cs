using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;

using TotalCore.Repositories.Generals;
using TotalBase;
using System.Data;
using TotalDTO.Generals;
using AutoMapper;
using System.ComponentModel;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class OleDbAPIs
    {
        private readonly IOleDbAPIRepository oleDbAPIRepository;

        public OleDbAPIs(IOleDbAPIRepository oleDbAPIRepository, GlobalEnums.MappingTaskID mappingTaskID)
        {
            this.oleDbAPIRepository = oleDbAPIRepository;
            this.oleDbAPIRepository.MappingTaskID = mappingTaskID;
        }

        public BindingList<ColumnMappingDTO> GetColumnMappings()
        {
            return Mapper.Map<IList<ColumnMapping>, BindingList<ColumnMappingDTO>>(this.oleDbAPIRepository.GetColumnMappings().ToList());
        }

        public void SaveColumnMapping(int columnMappingID, string columnMappingName)
        {
            this.oleDbAPIRepository.SaveColumnMapping(columnMappingID, columnMappingName);
        }

        public List<string> GetExcelSheets(string excelFile)
        {
            return this.oleDbAPIRepository.GetExcelSheets(excelFile);
        }

        public DataTable OpenExcelSheet(string excelFile, string sheetName)
        {
            return this.oleDbAPIRepository.OpenExcelSheet(excelFile, sheetName);
        }

        public DataTable OpenExcelSheet(string excelFile, string sheetName, string querySelect)
        {
            return this.oleDbAPIRepository.OpenExcelSheet(excelFile, sheetName, querySelect);
        }

    }
}