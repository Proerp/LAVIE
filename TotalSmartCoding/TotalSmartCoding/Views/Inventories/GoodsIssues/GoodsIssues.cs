using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;

using Ninject;

using TotalSmartCoding.Views.Mains;

using TotalBase.Enums;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalSmartCoding.Controllers.Inventories;
using TotalCore.Repositories.Inventories;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalCore.Services.Inventories;
using TotalSmartCoding.ViewModels.Inventories;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using TotalBase;
using TotalModel.Models;
using TotalSmartCoding.Properties;
using TotalDTO.Inventories;
using TotalSmartCoding.ViewModels.Helpers;

namespace TotalSmartCoding.Views.Inventories.GoodsIssues
{
    public partial class GoodsIssues : BaseView
    {
        private CustomTabControl customTabCenter;

        private GoodsIssueAPIs goodsIssueAPIs;
        private GoodsIssueViewModel goodsIssueViewModel { get; set; }

        private List<IPendingPrimaryDetail> pendingPrimaryDetails;

        public GoodsIssues()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastGoodsIssueIndex;

            this.goodsIssueAPIs = new GoodsIssueAPIs(CommonNinject.Kernel.Get<IGoodsIssueAPIRepository>());

            this.goodsIssueViewModel = CommonNinject.Kernel.Get<GoodsIssueViewModel>();
            this.goodsIssueViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.goodsIssueViewModel;

        }

        private void Pickups_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                this.naviIndex.Bands[0].ClientArea.Controls.Add(this.fastGoodsIssueIndex);

                this.customTabCenter = new CustomTabControl();
                this.customTabCenter.DisplayStyle = TabStyle.VisualStudio;

                this.customTabCenter.TabPages.Add("tabDetailPallets", "Pallet Details        ");
                this.customTabCenter.TabPages.Add("tabDetailPallets", "Carton Details        ");
                this.customTabCenter.TabPages.Add("tabDescription", "Description         ");
                this.customTabCenter.TabPages.Add("tabRemarks", "Remarks                 ");
                this.customTabCenter.TabPages[0].Controls.Add(this.gridexPalletDetails);
                this.customTabCenter.TabPages[1].Controls.Add(this.gridexCartonDetails);
                this.customTabCenter.TabPages[2].Controls.Add(this.textexDescription);
                this.customTabCenter.TabPages[3].Controls.Add(this.textexRemarks);

                this.customTabCenter.TabPages[2].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[3].Padding = new Padding(30, 30, 30, 30);

                this.customTabCenter.Dock = DockStyle.Fill;
                this.gridexPalletDetails.Dock = DockStyle.Fill;
                this.gridexCartonDetails.Dock = DockStyle.Fill;
                this.textexDescription.Dock = DockStyle.Fill;
                this.textexRemarks.Dock = DockStyle.Fill;
                this.panelMaster.Controls.Add(this.customTabCenter);

                this.naviDetails.ExpandedHeight = this.naviDetails.HeaderHeight + this.textexVoucherCodes.Size.Height + this.textexVehicle.Size.Height + this.textexVehicleDriver.Size.Height + 4 * 5 + 5;
                this.naviDetails.Expanded = false;

                this.labelCaption.Left = 68; this.labelCaption.Top = 12;
                if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.GoodsIssue) { ViewHelpers.SetFont(this, new Font("Calibri", 11), new Font("Calibri", 11), new Font("Calibri", 11)); ViewHelpers.SetFont(this.MdiParent, new Font("Calibri", 11), new Font("Calibri", 11), new Font("Calibri", 11)); } else { this.labelCaption.Top = this.labelCaption.Top + 1; }

                this.customTabCenter.SelectedIndexChanged += customTabCenter_SelectedIndexChanged;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void customTabCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.gridexPalletDetails.AutoResizeColumns();
            this.gridexCartonDetails.AutoResizeColumns();
        }



        Binding bindingEntryDate;
        Binding bindingVoucherCodes;
        Binding bindingVehicle;
        Binding bindingVehicleDriver;
        Binding bindingCaption;
        Binding bindingDescription;
        Binding bindingRemarks;

        Binding bindingForkliftDriverID;
        Binding bindingStorekeeperID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingCaption = this.labelCaption.DataBindings.Add("Text", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.Caption));

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVoucherCodes = this.textexVoucherCodes.DataBindings.Add("Text", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.VoucherCodes), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVehicle = this.textexVehicle.DataBindings.Add("Text", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.Vehicle), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVehicleDriver = this.textexVehicleDriver.DataBindings.Add("Text", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.VehicleDriver), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);


            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

            this.combexForkliftDriverID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.goodsIssueViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexForkliftDriverID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexForkliftDriverID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingForkliftDriverID = this.combexForkliftDriverID.DataBindings.Add("SelectedValue", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.ForkliftDriverID), true, DataSourceUpdateMode.OnPropertyChanged);


            this.combexStorekeeperID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.goodsIssueViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexStorekeeperID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexStorekeeperID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingStorekeeperID = this.combexStorekeeperID.DataBindings.Add("SelectedValue", this.goodsIssueViewModel, CommonExpressions.PropertyName<GoodsIssueViewModel>(p => p.StorekeeperID), true, DataSourceUpdateMode.OnPropertyChanged);


            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVoucherCodes.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVehicle.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVehicleDriver.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCaption.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingForkliftDriverID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastGoodsIssueIndex.AboutToCreateGroups += fastGoodsIssueIndex_AboutToCreateGroups;

            this.fastGoodsIssueIndex.ShowGroups = true;
            this.olvApproved.Renderer = new MappedImageRenderer(new Object[] { false, Resources.Placeholder16 });

            this.tableLayoutMaster.ColumnStyles[this.tableLayoutMaster.ColumnCount - 1].SizeType = SizeType.Absolute; this.tableLayoutMaster.ColumnStyles[this.tableLayoutMaster.ColumnCount - 1].Width = 10;
        }

        private void fastGoodsIssueIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Forklift_Yellow-32";
                    olvGroup.Subtitle = "List count: " + olvGroup.Contents.Count.ToString();
                    if ((DateTime)olvGroup.Key < DateTime.Today.AddDays(-1)) olvGroup.Collapsed = true;
                }
            }
        }

        protected override void InitializeDataGridBinding()
        {
            this.gridexPalletDetails.AutoGenerateColumns = false;
            this.gridexCartonDetails.AutoGenerateColumns = false;
            this.gridexPalletDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexCartonDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.gridexPalletDetails.DataSource = this.goodsIssueViewModel.PalletDetails;
            this.gridexCartonDetails.DataSource = this.goodsIssueViewModel.CartonDetails;

            this.goodsIssueViewModel.ViewDetails.ListChanged += ViewDetails_ListChanged;
        }

        private void ViewDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
            {
                this.customTabCenter.TabPages[0].Text = "Pallet Details [" + this.goodsIssueViewModel.PalletDetails.Count.ToString("N0") + " item(s)]             ";
                this.customTabCenter.TabPages[1].Text = "Carton Details [" + this.goodsIssueViewModel.CartonDetails.Count.ToString("N0") + " item(s)]             ";

                this.gridexPalletDetails.Columns["Pallet" + CommonExpressions.PropertyName<GoodsIssueDetailDTO>(p => p.PrimaryReference)].HeaderText = this.goodsIssueViewModel.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice ? "D.A" : "Orders";
                this.gridexCartonDetails.Columns["Carton" + CommonExpressions.PropertyName<GoodsIssueDetailDTO>(p => p.PrimaryReference)].HeaderText = this.goodsIssueViewModel.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice ? "D.A" : "Orders";
                this.gridexPalletDetails.Columns["Pallet" + CommonExpressions.PropertyName<GoodsIssueDetailDTO>(p => p.PrimaryReference)].Visible = this.goodsIssueViewModel.DeliveryAdviceID == null && this.goodsIssueViewModel.TransferOrderID == null;
                this.gridexCartonDetails.Columns["Carton" + CommonExpressions.PropertyName<GoodsIssueDetailDTO>(p => p.PrimaryReference)].Visible = this.goodsIssueViewModel.DeliveryAdviceID == null && this.goodsIssueViewModel.TransferOrderID == null;
            }
        }

        protected override void EditableModeChanged(bool editableMode)
        {
            base.EditableModeChanged(editableMode);
            this.toolStripNaviGroup.Visible = editableMode;
        }

        protected override Controllers.BaseController myController
        {
            get { return new GoodsIssueController(CommonNinject.Kernel.Get<IGoodsIssueService>(), this.goodsIssueViewModel); }
        }

        public override void Loading()
        {
            this.fastGoodsIssueIndex.SetObjects(this.goodsIssueAPIs.GetGoodsIssueIndexes());
            this.fastGoodsIssueIndex.Sort(this.olvEntryDate, SortOrder.Descending);

            base.Loading();
        }

        protected override void invokeEdit(int? id)
        {
            base.invokeEdit(id);
            this.getPendingItems();
        }

        public override void Save(bool escapeAfterSave)
        {
            base.Save(escapeAfterSave);
            this.getPendingItems();
        }

        private void callAutoSave()
        {
            try
            {
                if (this.goodsIssueViewModel.IsDirty && this.goodsIssueViewModel.IsValid) this.Save(false);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void getPendingItems()
        {
            try
            {
                if (this.goodsIssueViewModel.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice)
                {
                    List<PendingDeliveryAdviceDetail> pendingDeliveryAdviceDetails = this.goodsIssueAPIs.GetPendingDeliveryAdviceDetails(this.goodsIssueViewModel.LocationID, this.goodsIssueViewModel.GoodsIssueID, this.goodsIssueViewModel.DeliveryAdviceID, this.goodsIssueViewModel.CustomerID, this.goodsIssueViewModel.ReceiverID, string.Join(",", this.goodsIssueViewModel.ViewDetails.Select(d => d.DeliveryAdviceDetailID)), false);
                    this.pendingPrimaryDetails = pendingDeliveryAdviceDetails.ToList<IPendingPrimaryDetail>();
                    this.fastPendingPrimaryDetails.SetObjects(pendingDeliveryAdviceDetails); this.olvPrimaryReference.Text = "D.A";
                }
                if (this.goodsIssueViewModel.GoodsIssueTypeID == (int)GlobalEnums.GoodsIssueTypeID.TransferOrder)
                {
                    List<PendingTransferOrderDetail> pendingTransferOrderDetails = this.goodsIssueAPIs.GetPendingTransferOrderDetails(this.goodsIssueViewModel.LocationID, this.goodsIssueViewModel.GoodsIssueID, this.goodsIssueViewModel.WarehouseID, this.goodsIssueViewModel.TransferOrderID, this.goodsIssueViewModel.WarehouseReceiptID, string.Join(",", this.goodsIssueViewModel.ViewDetails.Select(d => d.TransferOrderDetailID)), false);
                    this.pendingPrimaryDetails = pendingTransferOrderDetails.ToList<IPendingPrimaryDetail>();
                    this.fastPendingPrimaryDetails.SetObjects(pendingTransferOrderDetails); this.olvPrimaryReference.Text = "Orders";
                }

                decimal? existBatchID = this.fastPendingPrimaryDetails.Objects.Cast<IPendingPrimaryDetail>().Where(w => w.BatchID != null).Count();
                this.olvBatchCode.Width = existBatchID != null && existBatchID > 0 ? 80 : 0; this.olvBatchCode.Text = this.olvBatchCode.Width == 0 ? "" : "Batches";

                this.olvPrimaryReference.Width = this.goodsIssueViewModel.DeliveryAdviceID != null || this.goodsIssueViewModel.TransferOrderID != null ? 0 : 70;
                this.olvCommodityName.Width = (this.goodsIssueViewModel.DeliveryAdviceID != null || this.goodsIssueViewModel.TransferOrderID != null ? (122 + 70) : 122) + (this.olvBatchCode.Width == 0 ? 80 : 0);

                //this.naviPendingItems.Text = "Pending " + this.fastPendingPrimaryDetails.GetItemCount().ToString("N0") + " row" + (this.fastPendingPrimaryDetails.GetItemCount() > 1 ? "s" : "");
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected override DialogResult wizardMaster()
        {
            WizardMaster wizardMaster = new WizardMaster(this.goodsIssueAPIs, this.goodsIssueViewModel);
            DialogResult dialogResult = wizardMaster.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK) this.callAutoSave();

            wizardMaster.Dispose();
            return dialogResult;
        }

        private void fastPendingPallets_MouseClick(object sender, MouseEventArgs e)
        {
            this.AddDetails(true);
        }

        private void buttonImportHandheld_Click(object sender, EventArgs e)
        {
            this.AddDetails(false);
        }

        private void AddDetails(bool usingTablet)
        {
            try
            {
                if (this.EditableMode && this.goodsIssueViewModel.Editable && this.goodsIssueViewModel.IsValid && !this.goodsIssueViewModel.IsDirty)
                {
                    IPendingPrimaryDetail pendingPrimaryDetail = null; string fileName = null;

                    if (usingTablet)
                        pendingPrimaryDetail = this.fastPendingPrimaryDetails.SelectedObject as IPendingPrimaryDetail;
                    else
                    {
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "Text Document (.txt)|*.txt";
                        if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "") fileName = openFileDialog.FileName;
                    }

                    if (pendingPrimaryDetail != null || fileName != null)
                    {
                        bool dialogResultOK;
                        WizardDetail wizardDetail = new WizardDetail(this.goodsIssueViewModel, this.pendingPrimaryDetails, pendingPrimaryDetail, fileName);

                        if (usingTablet)
                        {
                            TabletMDI tabletMDI = new TabletMDI(wizardDetail);
                            dialogResultOK = tabletMDI.ShowDialog() == System.Windows.Forms.DialogResult.OK;
                            wizardDetail.Dispose(); tabletMDI.Dispose();
                        }
                        else
                        {
                            dialogResultOK = wizardDetail.ShowDialog() == System.Windows.Forms.DialogResult.OK;
                            wizardDetail.Dispose();
                        }
                        if (dialogResultOK) this.callAutoSave();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonRemoveDetailItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.EditableMode && this.goodsIssueViewModel.Editable)
                {
                    Equin.ApplicationFramework.ObjectView<TotalDTO.Inventories.GoodsIssueDetailDTO> objectViewGoodsIssueDetailDTO = (this.customTabCenter.SelectedIndex == 0 ? this.gridexPalletDetails : this.gridexCartonDetails).CurrentRow.DataBoundItem as Equin.ApplicationFramework.ObjectView<TotalDTO.Inventories.GoodsIssueDetailDTO>;
                    GoodsIssueDetailDTO goodsIssueDetailDTO = (GoodsIssueDetailDTO)objectViewGoodsIssueDetailDTO;

                    if (goodsIssueDetailDTO != null && CustomMsgBox.Show(this, "Xác nhận xóa " + (goodsIssueDetailDTO.PalletID != null ? "pallet" : "carton") + ":" + (char)13 + (char)13 + (goodsIssueDetailDTO.PalletID != null ? goodsIssueDetailDTO.PalletCode : goodsIssueDetailDTO.CartonCode) + (char)13 + (char)13 + "Tại vi trí: " + (char)13 + (char)13 + goodsIssueDetailDTO.BinLocationCode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.goodsIssueViewModel.ViewDetails.Remove(goodsIssueDetailDTO);
                        this.callAutoSave();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void gridexViewDetails_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.callAutoSave();
        }

        protected override PrintViewModel InitPrintViewModel()
        {
            PrintViewModel printViewModel = base.InitPrintViewModel();
            printViewModel.ReportPath = "GoodsIssueSheet";
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("GoodsIssueID", this.goodsIssueViewModel.GoodsIssueID.ToString()));
            return printViewModel;
        }

    }
}
