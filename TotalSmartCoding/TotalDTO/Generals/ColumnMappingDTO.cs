using System.ComponentModel;

using TotalModel.Helpers;

namespace TotalDTO.Generals
{
    public class ColumnAvailableDTO: NotifyPropertyChangeObject
    {
        private string columnAvailableName;
        [DefaultValue(null)]
        public string ColumnAvailableName
        {
            get { return this.columnAvailableName; }
            set { ApplyPropertyChange<ColumnAvailableDTO, string>(ref this.columnAvailableName, o => o.ColumnAvailableName, value); }
        }

        private string columnMappingName;
        [DefaultValue(null)]
        public string ColumnMappingName
        {
            get { return this.columnMappingName; }
            set { ApplyPropertyChange<ColumnAvailableDTO, string>(ref this.columnMappingName, o => o.ColumnMappingName, value); }
        }
    }

    public class ColumnMappingDTO : NotifyPropertyChangeObject
    {
        private int columnMappingID;
        [DefaultValue(0)]
        public int ColumnMappingID
        {
            get { return this.columnMappingID; }
            set { ApplyPropertyChange<ColumnMappingDTO, int>(ref this.columnMappingID, o => o.ColumnMappingID, value); }
        }

        private int columnID;
        [DefaultValue(0)]
        public int ColumnID
        {
            get { return this.columnID; }
            set { ApplyPropertyChange<ColumnMappingDTO, int>(ref this.columnID, o => o.ColumnID, value); }
        }

        private string columnName;
        [DefaultValue(null)]
        public string ColumnName
        {
            get { return this.columnName; }
            set { ApplyPropertyChange<ColumnMappingDTO, string>(ref this.columnName, o => o.ColumnName, value); }
        }

        private string columnDisplayName;
        [DefaultValue(null)]
        public string ColumnDisplayName
        {
            get { return this.columnDisplayName; }
            set { ApplyPropertyChange<ColumnMappingDTO, string>(ref this.columnDisplayName, o => o.ColumnDisplayName, value); }
        }

        private string columnMappingName;
        [DefaultValue(null)]
        public string ColumnMappingName
        {
            get { return this.columnMappingName; }
            set { ApplyPropertyChange<ColumnMappingDTO, string>(ref this.columnMappingName, o => o.ColumnMappingName, value); }
        }

    }
}
