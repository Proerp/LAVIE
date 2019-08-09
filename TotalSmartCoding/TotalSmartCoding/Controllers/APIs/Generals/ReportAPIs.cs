using System;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;

using TotalDTO.Inventories;
using TotalCore.Repositories.Generals;

namespace TotalSmartCoding.Controllers.APIs.Generals
{
    public class ReportAPIs
    {
        private readonly IReportAPIRepository reportAPIRepository;

        public ReportAPIs(IReportAPIRepository reportAPIRepository)
        {
            this.reportAPIRepository = reportAPIRepository;
        }


        public ICollection<ReportIndex> GetReportIndexes()
        {
            return this.reportAPIRepository.GetEntityIndexes<ReportIndex>(ContextAttributes.User.UserID, GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate).ToList();
        }
    }
}
