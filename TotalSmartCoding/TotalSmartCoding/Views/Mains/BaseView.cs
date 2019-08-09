using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;

using Ninject;

using CustomControls;
using BrightIdeasSoftware;

using TotalCore.Repositories.Commons;

using TotalDTO;
using TotalDTO.Helpers;
using TotalBase;
using TotalBase.Enums;

using TotalModel.Interfaces;
using TotalModel.Models;

using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalSmartCoding.ViewModels.Helpers;
using TotalSmartCoding.ViewModels.Productions;
using TotalDTO.Helpers.Interfaces;



namespace TotalSmartCoding.Views.Mains
{
    public partial class BaseView : Form, IToolstripMerge, IToolstripChild
    {
        #region CONTRUCTOR
        protected BaseDTO baseDTO { get; set; }
        protected bool EscapeAfterSave { get; set; }

        public BaseView()
        {
            InitializeComponent();

            this.baseDTO = new BaseDTO(); //JUST FOR CREATE AN EMPTY BaseDTO IN BaseView AT DESIGN TIME ONLY (NOT FUNCTIONAL AT RUN TIME) 
            this.EscapeAfterSave = true;

            this.fastListIndex = new FastObjectListView();
        }

        private void BaseView_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeTabControl();

                this.fastListIndex.CheckBoxes = false;
                this.fastListIndex.SelectedIndexChanged += new EventHandler(this.fastListIndex_SelectedIndexChanged);
                this.fastListIndex.MouseClick += new MouseEventHandler(fastListIndex_MouseClick);
                this.fastListIndex.KeyDown += new KeyEventHandler(fastListIndex_KeyDown);

                this.baseDTO.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);

                this.InitializeCommonControlBinding();
                this.InitializeDataGridBinding();
                this.InitializeReadOnlyModeBinding();

                this.Loading();
                this.DoAfterLoad();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected virtual void DoAfterLoad() { }

        Binding bindingIsDirty;

        protected virtual void InitializeTabControl() { }

        protected virtual void InitializeCommonControlBinding()
        {
            //IMPORTANT: SHOULD BINDING IsDirty TO CONTROL, BECAUSE: THE PropertyChanged EVENT NEED THE BINDING TARGET IN ORDER TO RAISE: SEE HERE: if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName))
            this.bindingIsDirty = this.checkBoxIsDirty.DataBindings.Add("Checked", this.baseDTO, "IsDirty", true);

            this.bindingIsDirty.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.errorProviderMaster.DataSource = this.baseDTO; //JUST SET this.errorProviderMaster.DataSource HERE, IT WILL PROVIDE ERROR BINDING TO EVERY VIEW
        }

        private List<DataGridColumnNames> readOnlyDataGridColumnNames { get; set; }
        protected virtual void InitializeDataGridBinding() { this.readOnlyDataGridColumnNames = new List<DataGridColumnNames>(); }

        protected virtual void InitializeDataGridReadonlyColumns(DataGridexView dataGridView)
        {
            DataGridColumnNames dataGridColumnNames = new DataGridColumnNames() { DataGridView = dataGridView };
            foreach (DataGridViewColumn dataGridViewColumn in dataGridView.Columns)
            {
                if (dataGridViewColumn.ReadOnly) dataGridColumnNames.ColumnNames.Add(dataGridViewColumn.Name);
            }
            this.readOnlyDataGridColumnNames.Add(dataGridColumnNames);
        }

        protected virtual void dataGrid_ReadOnlyChanged(object sender, EventArgs e)
        {
            foreach (DataGridColumnNames dataGridColumnNames in this.readOnlyDataGridColumnNames)
            {
                if (sender.Equals(dataGridColumnNames.DataGridView))
                {
                    DataGridexView dataGridexView = sender as DataGridexView;
                    foreach (string columnName in dataGridColumnNames.ColumnNames)
                    {
                        if (dataGridexView.Columns[columnName] != null)
                            dataGridexView.Columns[columnName].ReadOnly = true;
                    }
                }
            }
        }

        protected virtual void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception) { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
        }

        protected virtual void InitializeReadOnlyModeBinding()
        {
            List<Control> controlList = ViewHelpers.GetAllControls(this);

            foreach (Control control in controlList)
            {
                IControlExtension controlExtension = control as IControlExtension;
                if (controlExtension != null)
                {
                    if (controlExtension.Editable)
                        control.DataBindings.Add("ReadOnly", this, "ReadonlyMode");
                    else
                        controlExtension.ReadOnly = true;

                    if (control is DataGridexView)
                    {
                        DataGridexView dataGridexView = control as DataGridexView;
                        if (dataGridexView != null && dataGridexView.AllowAddRow)
                            control.DataBindings.Add("AllowUserToAddRows", this, "EditableMode");
                        else
                            ((DataGridView)control).AllowUserToAddRows = false;

                        if (dataGridexView != null && dataGridexView.AllowDeleteRow)
                            control.DataBindings.Add("AllowUserToDeleteRows", this, "EditableMode");
                        else
                            ((DataGridView)control).AllowUserToDeleteRows = false;
                    }
                }
            }

            //this.fastListIndex.DataBindings.Add("Enabled", this, "ReadonlyMode"); //HERE: WE DON'T LOCK fastListIndex.Enabled TO ReadonlyMode, INSTEAD: WE HANDLE fastListIndex.MouseClick AND fastListIndex.KeyDown TO KEEP THE CURRENT ROW OF fastListIndex WHEN EditableMode
        }
        #endregion CONTRUCTOR

        #region EventHandlers
        private void fastListIndex_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.ReadonlyMode)
                {
                    int? selectedIndexID = this.getSelectedIndexID();
                    if (selectedIndexID != null && selectedIndexID != this.baseDTO.GetID()) this.invokeEdit(selectedIndexID);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void fastListIndex_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.EditableMode)
            {
                int? selectedIndexID = this.getSelectedIndexID();
                if (selectedIndexID != null && selectedIndexID != this.baseDTO.LastID)
                    this.setSelectedIndexID(this.baseDTO.LastID);
            }
        }

        private void fastListIndex_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.EditableMode) e.Handled = true;
        }

        private int? getSelectedIndexID()
        {
            if (this.fastListIndex.SelectedObject != null)
            {
                IBaseIndex baseIndex = (IBaseIndex)this.fastListIndex.SelectedObject;
                if (baseIndex != null) return baseIndex.Id; else return null;
            }
            else return null;
        }

        private void setSelectedIndexID(int? baseIndexID)
        {
            //if (baseIndexID != null && baseIndexID > 0)
            //{
            IBaseIndex baseIndex = this.fastListIndex.Objects.Cast<IBaseIndex>().FirstOrDefault(w => w.Id == baseIndexID);
            if (baseIndex == null && (IBaseIndex)this.fastListIndex.SelectedObject != null)
                fastListIndex_SelectedIndexChanged(this.fastListIndex, new EventArgs());
            else
            {
                if (baseIndex == null) baseIndex = this.fastListIndex.Objects.Cast<IBaseIndex>().FirstOrDefault();
                if (baseIndex != null)
                {
                    this.fastListIndex.SelectObject(baseIndex);
                    this.fastListIndex.EnsureModelVisible(baseIndex);
                }
            }
            //}
            //else
            //    if (this.ReadonlyMode && this.fastListIndex.GetItemCount() > 0) this.fastListIndex.SelectedIndex = 0;
        }

        protected virtual bool checkSelectedIndexID()
        {
            if (this.baseDTO.GetID() > 0)
            {
                this.setSelectedIndexID(this.baseDTO.GetID());
                if (this.baseDTO.GetID() == this.getSelectedIndexID())
                    return true;
            }

            throw new System.ArgumentException("Lỗi", "Vui lòng chọn dữ liệu.");
        }




        public void dataGridViewDetails_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var comboBox = e.Control as DataGridViewComboBoxEditingControl;
            if (comboBox != null)
            {
                comboBox.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
                comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            }
        }

        #endregion EventHandlers

        #region <Implement Interface>

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ModelDTO_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.NotifyPropertyChanged(e.PropertyName);
        }


        public GlobalEnums.NmvnTaskID NMVNTaskID
        {
            get { return this.baseDTO.NMVNTaskID; }
        }

        public virtual ToolStrip toolstripChild { get; protected set; }
        protected virtual FastObjectListView fastListIndex { get; set; }

        #region Context toolbar

        public bool IsDirty
        {
            get { return this.baseDTO.IsDirty; }
        }

        public bool IsValid
        {
            get { return this.baseDTO.IsValid; }
        }

        public virtual bool Closable { get { return true; } }
        public virtual bool Loadable { get { return true; } }

        public virtual bool AllowDataInput { get { return this.baseDTO.AllowDataInput; } }
        public virtual bool Newable { get { return this.baseDTO.Newable; } }
        public virtual bool Editable { get { return this.baseDTO.Editable; } }
        public virtual bool Deletable { get { return this.baseDTO.Deletable; } }

        public virtual bool Importable { get { return this.baseDTO.Importable; } }
        public virtual bool Exportable { get { return this.baseDTO.Exportable; } }

        public virtual bool Approvable { get { return this.baseDTO.Approvable; } }
        public virtual bool Unapprovable { get { return this.baseDTO.UnApprovable; } }

        public virtual bool Voidable { get { return this.baseDTO.Voidable; } }
        public virtual bool Unvoidable { get { return this.baseDTO.UnVoidable; } }

        public virtual bool Printable { get { return this.baseDTO.Printable && this.ReadonlyMode; } }
        public virtual bool PrintVisible { get { return this.baseDTO.PrintVisible; } }

        public virtual bool Filterable { get { return true; } }


        private bool editableMode;
        public bool EditableMode
        {
            get { return this.editableMode; }
            set
            {
                if (this.editableMode != value)
                {
                    this.editableMode = value && this.Editable;
                    this.NotifyPropertyChanged("EditableMode");
                    this.NotifyPropertyChanged("ReadonlyMode");

                    this.EditableModeChanged(this.EditableMode);
                }
            }
        }
        public bool ReadonlyMode { get { return !this.editableMode; } }

        protected virtual void EditableModeChanged(bool editableMode) { }

        #endregion Context toolbar


        #region IToolstripChild

        protected virtual BaseController myController { get { return new BaseController(); } }

        public virtual void Loading()
        {
            this.setSelectedIndexID(this.baseDTO.GetID());

            if (this.ReadonlyMode) this.invokeEdit(this.baseDTO.GetID()); //THIS LINE IS FOR REFRESH THE STATE OF THE CURRENT ENTITY (Editable/ Deletable/ ...)=> THIS MAY BE NOT NECCESSARY IN SOME CASE => LATER: WE SHOULD TRY TO REFRESH BY A BETTER WAY: TO REFRESH WHEN NECCESSARY ONLY
        }

        public virtual void ApplyFilter(string filterTexts)
        {
            OLVHelpers.ApplyFilters(this.fastListIndex, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
        }

        public virtual void ApplyDetailFilter(string filterTexts)
        {
            OLVHelpers.ApplyFilters(this.fastListIndex, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void Escape()
        {
            if (this.EditableMode)
            {
                if (this.IsDirty)
                {
                    DialogResult dialogResult = CustomMsgBox.Show(this, "Dữ liệu chưa lưu. Bạn có muốn lưu lại không?", "Warning", MessageBoxButtons.YesNoCancel);
                    if (dialogResult == DialogResult.Yes)
                    {
                        this.Save();
                        if (!this.IsDirty) this.CancelDirty(false);
                    }
                    else if (dialogResult == DialogResult.No)
                        this.CancelDirty(true);
                    else
                        return;
                }
                else
                    this.CancelDirty(false);
            }
            else
                if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.None || this.NMVNTaskID == GlobalEnums.NmvnTaskID.Report)
                    this.Close(); //Unload this module
        }

        private void CancelDirty(bool withRestore)
        {
            this.myController.CancelDirty(withRestore);
            this.EditableMode = false;
        }

        public void New()
        {
            //this.ControlBox = false;

            //string plainText = "Lê Minh Hiệp";
            //////////// Convert the plain string pwd into bytes
            ////////////byte[] plainTextBytes = UnicodeEncoding.Unicode.GetBytes(plainText);
            ////////////System.Security.Cryptography.HashAlgorithm hashAlgo = new System.Security.Cryptography.SHA256Managed();
            ////////////byte[] hash = hashAlgo.ComputeHash(plainTextBytes);

            //byte[] data = UnicodeEncoding.Unicode.GetBytes(plainText);
            //data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            //String hash = UnicodeEncoding.Unicode.GetString(data);
            //CustomMsgBox.Show(this, hash);


            this.myController.Create();
            if (this.wizardMaster() == DialogResult.OK)
            {
                this.EditableMode = true;
                this.wizardDetail();
            }
            else
                this.CancelDirty(true);
        }

        protected virtual DialogResult wizardMaster()
        {
            return DialogResult.OK;
        }

        protected virtual void wizardDetail() { }

        public void Edit()
        {
            if (this.checkSelectedIndexID())
            {
                this.invokeEdit(this.baseDTO.GetID());
                this.EditableMode = true;
            }
        }

        protected virtual void invokeEdit(int? id)
        {
            this.myController.Edit(id);
        }

        public void Save()
        {
            try
            {
                if (this.myController.Save())
                {
                    if (this.EscapeAfterSave) this.Escape();
                    this.Loading();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        public virtual void Save(bool escapeAfterSave)
        {
            bool currentEscapeAfterSave = this.EscapeAfterSave;
            this.EscapeAfterSave = escapeAfterSave;
            this.Save();
            this.EscapeAfterSave = currentEscapeAfterSave;
        }

        public void Delete()
        {
            try
            {
                if (this.checkSelectedIndexID())
                {
                    if (CustomMsgBox.Show(this, "Are you sure you want to delete " + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                        if (this.myController.Delete(this.baseDTO.GetID()))
                            this.Loading();
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        public void Approve()
        {
            try
            {
                if (this.checkSelectedIndexID())
                {
                    this.myController.Approve(this.baseDTO.GetID());

                    if (this.ApproveCheck(this.baseDTO.GetID()) && CustomMsgBox.Show(this, (this.baseDTO is BatchViewModel ? "Cài đặt batch này cho sản xuất " : "Are you sure you want to " + (this.baseDTO.Approvable ? "verify" : "un-verify") + " this entry data") + "?", "Warning", MessageBoxButtons.YesNo, (this.baseDTO.Approvable ? MessageBoxIcon.Information : MessageBoxIcon.Warning)) == DialogResult.Yes)
                        if (this.myController.ApproveConfirmed())
                        {
                            this.ApproveMore(this.baseDTO.GetID());
                            this.Loading();
                        }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected virtual bool ApproveCheck(int id) { return true; }
        protected virtual void ApproveMore(int id) { }

        public void Void()
        {
            try
            {
                if (this.checkSelectedIndexID())
                {
                    this.myController.Void(this.baseDTO.GetID());

                    if (this.VoidCheck(this.baseDTO.GetID()) && CustomMsgBox.Show(this, (this.baseDTO is BatchViewModel || this.baseDTO is BatchMasterViewModel ? "Dừng sản xuất batch này " : "Are you sure you want to " + (this.baseDTO.Voidable ? "void" : "un-void") + " this entry data") + "?" + "\r\n\r\n" + "Important: this action cannot be undone." + "\r\n\r\n\r\n\r\n" + "Lưu ý: Sau khi nhấn Yes, dữ liệu sẽ được vô hiệu vĩnh viễn.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop) == DialogResult.Yes)
                        if (this.myController.VoidConfirmed())
                        {
                            this.VoidMore(this.baseDTO.GetID());
                            this.Loading();
                        }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected virtual bool VoidCheck(int id) { return true; }
        protected virtual void VoidMore(int id) { }


        public void Lock()
        {
            try
            {
                if (this.checkSelectedIndexID())
                {
                    this.myController.Lock(this.baseDTO.GetID());

                    if (this.LockCheck(this.baseDTO.GetID()) && CustomMsgBox.Show(this, "Are you sure you want to " + (this.baseDTO.Lockable ? "lock" : "un-lock") + " this entry data" + "?", "Warning", MessageBoxButtons.YesNo, (this.baseDTO.Lockable ? MessageBoxIcon.Information : MessageBoxIcon.Warning)) == DialogResult.Yes)
                        if (this.myController.LockConfirmed())
                        {
                            this.LockMore(this.baseDTO.GetID());
                            this.Loading();
                        }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected virtual bool LockCheck(int id) { return true; }
        protected virtual void LockMore(int id) { }


        public void Print(GlobalEnums.PrintDestination printDestination)
        {
            try
            {
                SsrsViewer ssrsViewer = new SsrsViewer(this.InitPrintViewModel());
                ssrsViewer.Show();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected virtual PrintViewModel InitPrintViewModel() { return new PrintViewModel(); }

        public virtual void Import() { }

        protected virtual void ImportExcel(GlobalEnums.MappingTaskID mappingTaskID)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Excel File (.xlsx)|*.xlsx";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fileName = openFileDialog.FileName;
                    string sheetName = this.GetExcelSheet(mappingTaskID, fileName);
                    if (sheetName != null)
                    {
                        ColumnMappings columnMappings = new ColumnMappings(mappingTaskID, fileName, sheetName);

                        if (columnMappings.ShowDialog() == DialogResult.OK)
                            this.DoImportExcel(fileName, sheetName);

                        columnMappings.Dispose();
                    }
                }
                openFileDialog.Dispose();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private string GetExcelSheet(GlobalEnums.MappingTaskID mappingTaskID, string excelFile)
        {
            string sheetName = null;
            ExcelSheets excelSheets = new ExcelSheets(mappingTaskID, excelFile);

            if (excelSheets.ShowDialog() == DialogResult.OK) sheetName = excelSheets.Tag.ToString();
            excelSheets.Dispose();

            return sheetName;
        }

        protected virtual void DoImportExcel(string fileName, string sheetName) { }

        public void Export()
        {
            //try
            //{
            //    if (this.ActiveControl.Equals(this.dataListViewMaster))
            //    {
            //        DataTable dataTableExport;
            //        dataTableExport = this.dataListViewMaster.DataSource as DataTable;
            //        if (dataTableExport != null) CommonFormAction.Export(dataTableExport);
            //    }
            //    else
            //    {
            //        List<MarketingProgramCustomerName> listExport;
            //        listExport = this.marketingProgramBLL.CustomerNameList.ToList();
            //        if (listExport != null) CommonFormAction.Export(listExport);
            //    }
            //}
            //catch (Exception exception)
            //{
            //    GlobalExceptionHandler.ShowExceptionMessageBox(this, exception);
            //}
        }



        #endregion


        #endregion <Implement Interface>

        #region Helper Method
        protected virtual void CalculateQuantityDetailDTO(IQuantityDetailDTO quantityDetailDTO, string propertyName, int? deliveryAdviceID, int? transferOrderID)
        {
            if (propertyName == CommonExpressions.PropertyName<IQuantityDetailDTO>(p => p.CommodityID))
            {
                CommodityAPIs commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>()); //WE MUST USE ContextAttributes.User.LocationID, INSTEAD OF quantityDetailDTO.LocationID, BECAUSE AT FIRST quantityDetailDTO.LocationID = 0. WHEN SAVE: GenericService.PreSaveRoutines WILL UPDATE DTO.LocationID = ContextAttributes.User.LocationID. SEE GenericService.PreSaveRoutines FOR MORE INFORMATION!!!
                IList<SearchCommodity> searchCommodities = commodityAPIs.SearchCommodities(quantityDetailDTO.CommodityID, ContextAttributes.User.LocationID, null, deliveryAdviceID, transferOrderID);
                if (searchCommodities.Count > 0)
                {
                    quantityDetailDTO.CommodityCode = searchCommodities[0].Code;
                    quantityDetailDTO.CommodityName = searchCommodities[0].Name;
                    quantityDetailDTO.Unit = searchCommodities[0].Unit;
                    quantityDetailDTO.PackageSize = searchCommodities[0].PackageSize;
                    quantityDetailDTO.Volume = searchCommodities[0].Volume;
                    quantityDetailDTO.PackageVolume = searchCommodities[0].PackageVolume;

                    IAvailableQuantityDetailDTO availableQuantityDetailDTO = quantityDetailDTO as IAvailableQuantityDetailDTO;
                    if (availableQuantityDetailDTO != null)
                    {
                        availableQuantityDetailDTO.QuantityAvailable = searchCommodities[0].QuantityAvailable;
                        availableQuantityDetailDTO.LineVolumeAvailable = searchCommodities[0].LineVolumeAvailable;
                    }

                    IBatchQuantityDetailDTO batchQuantityDetailDTO = quantityDetailDTO as IBatchQuantityDetailDTO;
                    if (batchQuantityDetailDTO != null)
                    {
                        batchQuantityDetailDTO.BatchID = null;
                        batchQuantityDetailDTO.BatchCode = null;
                        batchQuantityDetailDTO.BatchEntryDate = null;
                        batchQuantityDetailDTO.QuantityBatchAvailable = 0;
                        batchQuantityDetailDTO.LineVolumeBatchAvailable = 0;
                    }
                }
            }
            if (propertyName == CommonExpressions.PropertyName<IQuantityDetailDTO>(p => p.PackageVolume) || propertyName == CommonExpressions.PropertyName<IQuantityDetailDTO>(p => p.Quantity))
                quantityDetailDTO.LineVolume = quantityDetailDTO.Quantity * quantityDetailDTO.PackageVolume;

        }
        #endregion Helper Method
    }

    public class DataGridColumnNames
    {
        public Control DataGridView { get; set; }
        public List<string> ColumnNames { get; set; }

        public DataGridColumnNames() { this.ColumnNames = new List<string>(); }
    }
}
