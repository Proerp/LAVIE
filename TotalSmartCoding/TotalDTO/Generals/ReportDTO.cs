using System;
using System.ComponentModel;

using TotalModel;
using TotalBase.Enums;

namespace TotalDTO.Generals
{
    public class ReportPrimitiveDTO : BaseDTO, IPrimitiveEntity, IPrimitiveDTO
    {
        public override GlobalEnums.NmvnTaskID NMVNTaskID { get { return GlobalEnums.NmvnTaskID.Report; } }
        public override bool NoApprovable { get { return true; } }

        public override int GetID() { return this.ReportID; }
        public void SetID(int id) { this.ReportID = id; }


        private int reportID;
        [DefaultValue(0)]
        public int ReportID
        {
            get { return this.reportID; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, int>(ref this.reportID, o => o.ReportID, value); }
        }

        private int reportUniqueID;
        public int ReportUniqueID
        {
            get { return this.reportUniqueID; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, int>(ref this.reportUniqueID, o => o.ReportUniqueID, value); }
        }

        private int reportTypeID;
        public int ReportTypeID
        {
            get { return this.reportTypeID; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, int>(ref this.reportTypeID, o => o.ReportTypeID, value); }
        }

        private int reportGroupID;
        public int ReportGroupID
        {
            get { return this.reportGroupID; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, int>(ref this.reportGroupID, o => o.ReportGroupID, value); }
        }

        private string reportName;
        [DefaultValue(null)]
        public string ReportName
        {
            get { return this.reportName; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, string>(ref this.reportName, o => o.ReportName, value); }
        }

        private string reportGroupName;
        [DefaultValue(null)]
        public string ReportGroupName
        {
            get { return this.reportGroupName; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, string>(ref this.reportGroupName, o => o.ReportGroupName, value); }
        }

        private string reportURL;
        [DefaultValue(null)]
        public string ReportURL
        {
            get { return this.reportURL; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, string>(ref this.reportURL, o => o.ReportURL, value); }
        }

        private string reportTabPageIDs;
        [DefaultValue(null)]
        public string ReportTabPageIDs
        {
            get { return this.reportTabPageIDs; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, string>(ref this.reportTabPageIDs, o => o.ReportTabPageIDs, value); }
        }

        private string optionBoxIDs;
        [DefaultValue(null)]
        public string OptionBoxIDs
        {
            get { return this.optionBoxIDs; }
            set { ApplyPropertyChange<ReportPrimitiveDTO, string>(ref this.optionBoxIDs, o => o.OptionBoxIDs, value); }
        }
    }

    public class ReportDTO : ReportPrimitiveDTO
    {
    }
}
