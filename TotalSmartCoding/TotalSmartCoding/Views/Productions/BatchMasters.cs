using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ninject;



using TotalSmartCoding.Views.Mains;


using TotalBase;
using TotalBase.Enums;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalCore.Repositories.Productions;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalCore.Services.Productions;
using TotalSmartCoding.ViewModels.Productions;
using TotalSmartCoding.Controllers.Productions;
using TotalDTO.Productions;
using AutoMapper;
using TotalModel.Models;

using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using TotalModel.Interfaces;
using BrightIdeasSoftware;
using TotalSmartCoding.Properties;
using TotalSmartCoding.Views.Commons.Commodities;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalSmartCoding.Controllers.Commons;
using TotalCore.Services.Commons;
using TotalCore.Repositories.Generals;
using TotalSmartCoding.ViewModels.Commons;
using TotalModel.Helpers;



namespace TotalSmartCoding.Views.Productions
{
    public partial class BatchMasters : BaseView
    {
        private BatchMasterAPIs batchMasterAPIs;
        private BatchMasterViewModel batchMasterViewModel { get; set; }

        private CommodityAPIs commodityAPIs { get; set; }
        private BatchStatusAPIs batchStatusAPIs { get; set; }

        public BatchMasters()
            : base()
        {
            InitializeComponent();


            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastBatchMasterIndex;

            this.comboDiscontinued.SelectedIndex = 0;
            this.comboShowCummulativePacks.SelectedIndex = GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.BatchMaster ? 0 : 0;

            this.batchMasterAPIs = new BatchMasterAPIs(CommonNinject.Kernel.Get<IBatchMasterAPIRepository>());

            this.batchMasterViewModel = CommonNinject.Kernel.Get<BatchMasterViewModel>();
            this.batchMasterViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.batchMasterViewModel;
        }

        protected override void NotifyPropertyChanged(string propertyName)
        {
            base.NotifyPropertyChanged(propertyName);

            if (propertyName == "ReadonlyMode")
            {
                this.buttonNewLOT.Enabled = this.Newable && this.ReadonlyMode && !this.batchMasterViewModel.InActive;
                this.buttonDiscontinued.Enabled = this.Newable && this.ReadonlyMode;
            }
        }

        protected override void InitializeTabControl()
        {
            try
            {
                CustomTabControl customTabBatchMaster = new CustomTabControl();
                //customTabControlCustomerChannel.ImageList = this.imageListTabControl;

                customTabBatchMaster.Font = this.textexCode.Font;
                customTabBatchMaster.DisplayStyle = TabStyle.VisualStudio;
                customTabBatchMaster.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

                customTabBatchMaster.TabPages.Add("BatchMaster", "Batch Information    ");
                customTabBatchMaster.TabPages[0].Controls.Add(this.layoutMaster);

                this.naviBarMaster.Bands[0].ClientArea.Controls.Add(customTabBatchMaster);

                customTabBatchMaster.Dock = DockStyle.Fill;
                this.layoutMaster.Dock = DockStyle.Fill;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingEntryDate;
        Binding bindingCode;

        Binding bindingPlannedQuantity;

        Binding bindingRemarks;

        Binding bindingCommodityID;
        Binding bindingBatchStatusID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.batchMasterViewModel, "EntryDate", true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCode = this.textexCode.DataBindings.Add("Text", this.batchMasterViewModel, "Code", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingPlannedQuantity = this.numericPlannedQuantity.DataBindings.Add("Value", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.PlannedQuantity), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.batchMasterViewModel, "Remarks", true, DataSourceUpdateMode.OnPropertyChanged);

            this.textexCommodityAPICode.DataBindings.Add("Text", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.CommodityAPICode), true);
            this.textexCommodityName.DataBindings.Add("Text", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.CommodityName), true);

            this.commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>());
            this.combexCommodityID.DataSource = this.commodityAPIs.GetCommodityBases();
            this.combexCommodityID.DisplayMember = CommonExpressions.PropertyName<CommodityBase>(p => p.Code);
            this.combexCommodityID.ValueMember = CommonExpressions.PropertyName<CommodityBase>(p => p.CommodityID);
            this.bindingCommodityID = this.combexCommodityID.DataBindings.Add("SelectedValue", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.CommodityID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.batchStatusAPIs = new BatchStatusAPIs(CommonNinject.Kernel.Get<IBatchStatusAPIRepository>());
            this.combexBatchStatusID.DataSource = this.batchStatusAPIs.GetBatchStatusBases();
            this.combexBatchStatusID.DisplayMember = CommonExpressions.PropertyName<BatchStatusBase>(p => p.Code);
            this.combexBatchStatusID.ValueMember = CommonExpressions.PropertyName<BatchStatusBase>(p => p.BatchStatusID);
            this.bindingBatchStatusID = this.combexBatchStatusID.DataBindings.Add("SelectedValue", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.BatchStatusID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingPlannedQuantity.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingCommodityID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingBatchStatusID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastBatchMasterIndex.AboutToCreateGroups += fastBatchMasterIndex_AboutToCreateGroups;

            this.fastBatchMasterIndex.ShowGroups = true;

            this.buttonNewLOT.Visible = GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail;
            this.buttonRemoveLOT.Visible = GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Smallpack || GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pail; this.separatorRemoveLOT.Visible = this.buttonRemoveLOT.Visible;
            this.buttonBatches.Visible = !this.buttonNewLOT.Visible; 
        }

        private void fastBatchMasterIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Storage32";
                    olvGroup.Subtitle = olvGroup.Contents.Count.ToString() + " Batch" + (olvGroup.Contents.Count > 1 ? "es" : "");
                }
            }
        }

        protected override void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            base.CommonControl_BindingComplete(sender, e);
            if (sender.Equals(this.bindingCommodityID))
            {
                if (this.combexCommodityID.SelectedItem != null)
                {
                    CommodityBase commodityBase = (CommodityBase)this.combexCommodityID.SelectedItem;
                    this.batchMasterViewModel.CommodityCode = commodityBase.Code;
                    this.batchMasterViewModel.CommodityName = commodityBase.Name;
                    this.batchMasterViewModel.CommodityAPICode = commodityBase.APICode;
                }
            }
        }

        private BatchMasterController batchMasterController
        {
            get { return new BatchMasterController(CommonNinject.Kernel.Get<IBatchMasterService>(), this.batchMasterViewModel); }
        }

        protected override Controllers.BaseController myController
        {
            get { return this.batchMasterController; }
        }

        public override void Loading()
        {
            this.fastBatchMasterIndex.SetObjects(this.batchMasterAPIs.GetBatchMasterIndexes(this.comboShowCummulativePacks.SelectedIndex == 0 ? false : true, this.comboDiscontinued.SelectedIndex == 0 ? GlobalEnums.ActiveOption.Active : GlobalEnums.ActiveOption.Both));
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastBatchMasterIndex.Sort(this.olvEntryDate, SortOrder.Descending);
        }

        public override void Import()
        {
            this.ImportExcel(GlobalEnums.MappingTaskID.BatchMaster);
        }

        protected override void DoImportExcel(string fileName, string sheetName)
        {
            base.DoImportExcel(fileName, sheetName);
            this.ImportExcel(fileName, sheetName);
            this.Loading();
        }

        private void comboDiscontinued_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.batchMasterAPIs != null) this.Loading();
        }

        private void comboShowCummulativePacks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.batchMasterAPIs != null) this.Loading();
        }

        private void fastBatchIndex_FormatRow(object sender, FormatRowEventArgs e)
        {
            BatchMasterIndex batchMasterIndex = (BatchMasterIndex)e.Model;
            if (batchMasterIndex.InActive) e.Item.ForeColor = Color.Gray;
        }

        protected override bool ApproveCheck(int id)
        {
            return !this.batchMasterViewModel.IsDefault && !this.batchMasterViewModel.InActive;
        }


        private void buttonBatches_Click(object sender, EventArgs e)
        {
            try
            {
                MasterMDI masterMDI = new MasterMDI(GlobalEnums.NmvnTaskID.Batch, new Batches(null, false));

                masterMDI.ShowDialog();
                masterMDI.Dispose();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonDiscontinued_Click(object sender, EventArgs e)
        {
            this.Void();
        }

        protected override bool VoidCheck(int id)
        {
            this.batchMasterViewModel.VoidTypeID = 1;
            return !this.batchMasterViewModel.IsDefault;
        }

        private void buttonItems_Click(object sender, EventArgs e)
        {
            try
            {
                MasterMDI masterMDI = new MasterMDI(GlobalEnums.NmvnTaskID.Commodity, new Commodities());

                masterMDI.ShowDialog();
                masterMDI.Dispose();

                this.combexCommodityID.DataSource = this.commodityAPIs.GetCommodityBases();
                this.invokeEdit(this.batchMasterViewModel.BatchMasterID);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonNewLOT_Click(object sender, EventArgs e)
        {
            try
            {
                this.batchMasterViewModel.LastID = this.batchMasterViewModel.GetID();
                BatchMasterWizard wizardMaster = new BatchMasterWizard(this.batchMasterController, this.batchMasterViewModel);
                DialogResult dialogResult = wizardMaster.ShowDialog(); wizardMaster.Dispose();

                this.batchMasterController.CancelDirty(true);
                if (dialogResult == DialogResult.OK) this.Loading();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonRemoveLOT_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fastBatchMasterIndex.SelectedObject != null)
                {
                    BatchMasterIndex batchMasterIndex = (BatchMasterIndex)this.fastBatchMasterIndex.SelectedObject;
                    if (batchMasterIndex != null && batchMasterIndex.LotID != null)
                    {
                        DialogResult dialogResult = CustomMsgBox.Show(this, "Vui lòng xác nhận xóa LOT đang chọn?" + "\r\n" + "\r\n" + "Batch: " + batchMasterIndex.BatchMasterCode + ", Lot: " + batchMasterIndex.LotCode, "Warning", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.Yes)
                            if (this.batchMasterController.RemoveLot((int)batchMasterIndex.LotID)) this.Loading();

                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }











        #region Import Excel

        public bool ImportExcel(string fileName, string sheetName)
        {
            try
            {
                OleDbAPIs oleDbAPIs = new OleDbAPIs(CommonNinject.Kernel.Get<IOleDbAPIRepository>(), GlobalEnums.MappingTaskID.BatchMaster);

                CommodityViewModel commodityViewModel = CommonNinject.Kernel.Get<CommodityViewModel>();
                CommodityController commodityController = new CommodityController(CommonNinject.Kernel.Get<ICommodityService>(), commodityViewModel);


                int intValue; decimal decimalValue; DateTime dateTimeValue;
                ExceptionTable exceptionTable = new ExceptionTable(new string[2, 2] { { "ExceptionCategory", "System.String" }, { "Description", "System.String" } });

                //////////TimeSpan timeout = TimeSpan.FromMinutes(90);
                //////////using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, timeout))
                //////////{
                //////////if (!this.Editable(this.)) throw new System.ArgumentException("Import", "Permission conflict");


                DataTable excelDataTable = oleDbAPIs.OpenExcelSheet(fileName, sheetName);
                if (excelDataTable != null && excelDataTable.Rows.Count > 0)
                {
                    foreach (DataRow excelDataRow in excelDataTable.Rows)
                    {
                        exceptionTable.ClearDirty();

                        string code = excelDataRow["Code"].ToString().Trim();
                        if (code.Length < 5) code = new String('0', 5 - code.Length) + code;

                        BatchMasterBase batchMasterBase = this.batchMasterAPIs.GetBatchMasterBase(code);

                        if (batchMasterBase == null)
                        {
                            this.batchMasterController.Create();

                            this.batchMasterViewModel.EntryDate = new DateTime(2000, 1, 1);
                            this.batchMasterViewModel.Code = code;


                            if (DateTime.TryParse(excelDataRow["PlannedDate"].ToString(), out dateTimeValue)) this.batchMasterViewModel.PlannedDate = dateTimeValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu Plan start date", "Batch: " + code + ": " + excelDataRow["PlannedDate"].ToString() });
                            if (decimal.TryParse(excelDataRow["PlannedQuantity"].ToString(), out decimalValue)) this.batchMasterViewModel.PlannedQuantity = decimalValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu SL Kế hoạch", "Batch: " + code + ": " + excelDataRow["PlannedQuantity"].ToString() });

                            CommodityBase commodityBase = this.commodityAPIs.GetCommodityBase(excelDataRow["CommodityCode"].ToString());
                            if (commodityBase != null) this.batchMasterViewModel.CommodityID = commodityBase.CommodityID;
                            else
                            {
                                commodityController.Create();

                                commodityViewModel.Code = excelDataRow["CommodityCode"].ToString();
                                commodityViewModel.APICode = excelDataRow["CommodityAPICode"].ToString().Replace("'", "");
                                commodityViewModel.CartonCode = excelDataRow["CommodityCartonCode"].ToString().Replace("'", "");
                                commodityViewModel.Name = excelDataRow["CommodityName"].ToString();
                                commodityViewModel.OfficialName = excelDataRow["CommodityName"].ToString();

                                commodityViewModel.CommodityCategoryID = 2;
                                commodityViewModel.FillingLineIDs = "1,2";

                                if (decimal.TryParse(excelDataRow["Volume"].ToString(), out decimalValue)) commodityViewModel.Volume = decimalValue / 1000; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu Trọng lượng", "Batch: " + code + ": " + excelDataRow["Volume"].ToString() });
                                if (int.TryParse(excelDataRow["PackPerCarton"].ToString(), out intValue)) commodityViewModel.PackPerCarton = intValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu QC thùng", "Batch: " + code + ": " + excelDataRow["PackPerCarton"].ToString() });
                                if (int.TryParse(excelDataRow["CartonPerPallet"].ToString(), out intValue)) commodityViewModel.CartonPerPallet = intValue / commodityViewModel.PackPerCarton; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu QC pallet", "Batch: " + code + ": " + excelDataRow["CartonPerPallet"].ToString() });

                                commodityViewModel.PackageSize = commodityViewModel.PackPerCarton.ToString("N0") + "x" + commodityViewModel.Volume.ToString("N2") + "kg";

                                if (int.TryParse(excelDataRow["Shelflife"].ToString(), out intValue)) commodityViewModel.Shelflife = intValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu Expiration months", "Batch: " + code + ": " + excelDataRow["Shelflife"].ToString() });

                                if (!commodityViewModel.IsValid) exceptionTable.AddException(new string[] { "Lỗi dữ liệu sản phẩm không hợp lệ", excelDataRow["CommodityCode"].ToString() + ": " + commodityViewModel.Error });
                                else
                                    if (commodityViewModel.IsDirty)
                                        if (commodityController.Save())
                                            this.batchMasterViewModel.CommodityID = commodityViewModel.CommodityID;
                                        else
                                            exceptionTable.AddException(new string[] { "Lỗi lưu dữ liệu [thêm sản phẩm mới]", excelDataRow["CommodityCode"].ToString() + commodityController.BaseService.ServiceTag });
                            }


                            BatchStatusBase batchStatusBase = this.batchStatusAPIs.GetBatchStatusBase(excelDataRow["BatchStatusCode"].ToString());
                            if (batchStatusBase != null) this.batchMasterViewModel.BatchStatusID = batchStatusBase.BatchStatusID; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu Trạng thái", "Batch: " + code + ": " + excelDataRow["BatchStatusCode"].ToString() });


                            if (!this.batchMasterViewModel.IsValid) exceptionTable.AddException(new string[] { "Lỗi dữ liệu Batch không hợp lệ", "Batch: " + code + ": " + this.batchMasterViewModel.Error }); ;
                            if (!exceptionTable.IsDirty)
                                if (this.batchMasterViewModel.IsDirty && !this.batchMasterController.Save())
                                    exceptionTable.AddException(new string[] { "Lỗi lưu dữ liệu [thêm batch mới]", code + this.batchMasterController.BaseService.ServiceTag });
                        }
                        else
                            exceptionTable.AddException(new string[] { "Batch không được import, do đã tồn tại trên hệ thống", "Batch: " + code });
                    }
                }

                if (exceptionTable.Table.Rows.Count <= 0)
                    return true;
                else
                    throw new CustomException("Lỗi import file excel. Vui lòng xem danh sách đính kèm. Click vào từng nội dung để xem chi tiết.", exceptionTable.Table);

            }
            catch (System.Exception exception)
            {
                throw exception;
            }
        }


        #endregion Import Excel




    }
}
