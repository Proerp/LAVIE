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



using TotalBase.Enums;
using TotalSmartCoding.Properties;
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
using TotalDTO.Inventories;
using BrightIdeasSoftware;
using TotalSmartCoding.Libraries.StackedHeaders;
using TotalSmartCoding.ViewModels.Helpers;


namespace TotalSmartCoding.Views.Inventories.GoodsReceipts
{
    public partial class GoodsReceipts : BaseView
    {
        private CustomTabControl customTabLeft;
        private CustomTabControl customTabCenter;

        private GoodsReceiptAPIs goodsReceiptAPIs;
        private GoodsReceiptViewModel goodsReceiptViewModel { get; set; }

        public GoodsReceipts()
            : base()
        {
            InitializeComponent();


            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastGoodsReceiptIndex;

            this.goodsReceiptAPIs = new GoodsReceiptAPIs(CommonNinject.Kernel.Get<IGoodsReceiptAPIRepository>());

            this.goodsReceiptViewModel = CommonNinject.Kernel.Get<GoodsReceiptViewModel>();
            this.goodsReceiptViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.goodsReceiptViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                #region TabLeft
                this.customTabLeft = new CustomTabControl();
                this.customTabLeft.DisplayStyle = TabStyle.VisualStudio;

                this.customTabLeft.TabPages.Add("tabLeftAA", "Goods Receipt ");
                this.customTabLeft.TabPages[0].BackColor = this.panelLeft.BackColor;
                this.customTabLeft.TabPages[0].Padding = new Padding(15, 0, 0, 0);
                this.customTabLeft.TabPages[0].Controls.Add(this.layoutLeft);

                this.customTabLeft.Dock = DockStyle.Fill;
                this.panelLeft.Controls.Add(this.customTabLeft);

                this.layoutLeft.ColumnStyles[this.layoutLeft.ColumnCount - 1].SizeType = SizeType.Absolute; this.layoutLeft.ColumnStyles[this.layoutLeft.ColumnCount - 1].Width = 15;
                #endregion TabLeft

                #region TabCenter
                this.customTabCenter = new CustomTabControl();
                this.customTabCenter.DisplayStyle = TabStyle.VisualStudio;

                this.customTabCenter.TabPages.Add("tabCenterAA", "Pallets                  ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Cartons                  ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Description            ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Remarks                    ");

                this.customTabCenter.TabPages[0].Controls.Add(this.gridexPalletDetails);
                this.customTabCenter.TabPages[0].Controls.Add(this.toolStripPallet);
                this.customTabCenter.TabPages[1].Controls.Add(this.gridexCartonDetails);
                this.customTabCenter.TabPages[1].Controls.Add(this.toolStripCarton);
                this.customTabCenter.TabPages[2].Controls.Add(this.textexDescription);
                this.customTabCenter.TabPages[3].Controls.Add(this.textexRemarks);
                this.customTabCenter.TabPages[2].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[3].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.customTabCenter.TabPages[1].BackColor = this.panelCenter.BackColor;
                this.toolStripPallet.Dock = DockStyle.Left;
                this.gridexPalletDetails.Dock = DockStyle.Fill;
                this.toolStripCarton.Dock = DockStyle.Left;
                this.gridexCartonDetails.Dock = DockStyle.Fill;
                this.textexDescription.Dock = DockStyle.Fill;
                this.textexRemarks.Dock = DockStyle.Fill;

                this.customTabCenter.Dock = DockStyle.Fill;
                this.panelCenter.Controls.Add(this.customTabCenter);
                #endregion TabCenter

                this.tableLayoutPanelExtend.ColumnStyles[this.tableLayoutPanelExtend.ColumnCount - 1].SizeType = SizeType.Absolute; this.tableLayoutPanelExtend.ColumnStyles[this.tableLayoutPanelExtend.ColumnCount - 1].Width = 15;

                this.buttonExpandTop.Visible = this.naviGroupTop.Tag.ToString() == "Expandable";
                this.buttonExpandTop_Click(this.buttonExpandTop, new EventArgs());

                this.labelCaption.Top = 7;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingEntryDate;        
        Binding bindingWarehouseName;
        Binding bindingVehicleDriver;
        Binding bindingDescription;
        Binding bindingRemarks;
        Binding bindingCaption;

        Binding bindingStorekeeperID;
        Binding bindingForkliftDriverID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptDTO>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);            
            this.bindingWarehouseName = this.textexWarehouseName.DataBindings.Add("Text", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptDTO>(p => p.WarehouseName), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVehicleDriver = this.textexVehicleDriver.DataBindings.Add("Text", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptDTO>(p => p.VehicleDriver), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptDTO>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCaption = this.labelCaption.DataBindings.Add("Text", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptDTO>(p => p.Caption));

            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

            this.combexStorekeeperID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.goodsReceiptViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexStorekeeperID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexStorekeeperID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingStorekeeperID = this.combexStorekeeperID.DataBindings.Add("SelectedValue", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptViewModel>(p => p.StorekeeperID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.combexForkliftDriverID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.goodsReceiptViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexForkliftDriverID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexForkliftDriverID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingForkliftDriverID = this.combexForkliftDriverID.DataBindings.Add("SelectedValue", this.goodsReceiptViewModel, CommonExpressions.PropertyName<GoodsReceiptViewModel>(p => p.ForkliftDriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);            
            this.bindingWarehouseName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVehicleDriver.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCaption.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingForkliftDriverID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastGoodsReceiptIndex.AboutToCreateGroups += fastGoodsReceiptIndex_AboutToCreateGroups;

            this.fastGoodsReceiptIndex.ShowGroups = true;
            this.olvApproved.Renderer = new MappedImageRenderer(new Object[] { false, Resources.Placeholder16 });
            this.naviGroupDetails.ExpandedHeight = this.naviGroupDetails.Size.Height;
        }

        private void fastGoodsReceiptIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Storage32";
                    olvGroup.Subtitle = "List count: " + olvGroup.Contents.Count.ToString();
                }
            }
        }

        protected override void InitializeDataGridBinding()
        {
            this.gridexPalletDetails.AutoGenerateColumns = false;
            this.gridexCartonDetails.AutoGenerateColumns = false;
            this.gridexPalletDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexCartonDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexPalletDetails.DataSource = this.goodsReceiptViewModel.PalletDetails;
            this.gridexCartonDetails.DataSource = this.goodsReceiptViewModel.CartonDetails;

            this.goodsReceiptViewModel.ViewDetails.ListChanged += ViewDetails_ListChanged;

            StackedHeaderDecorator stackedHeaderDecoratorPallet = new StackedHeaderDecorator(this.gridexPalletDetails);
            StackedHeaderDecorator stackedHeaderDecoratorCarton = new StackedHeaderDecorator(this.gridexCartonDetails);
        }

        private void ViewDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
            {
                this.customTabCenter.TabPages[0].Text = "Pallets [" + this.goodsReceiptViewModel.PalletDetails.Count.ToString("N0") + " item(s)]             ";
                this.customTabCenter.TabPages[1].Text = "Cartons [" + this.goodsReceiptViewModel.CartonDetails.Count.ToString("N0") + " item(s)]             ";

                this.gridexPalletDetails.Columns["Pallet" + CommonExpressions.PropertyName<GoodsReceiptDetailDTO>(p => p.PrimaryReference)].Visible = this.goodsReceiptViewModel.PickupID == null && this.goodsReceiptViewModel.GoodsIssueID == null && this.goodsReceiptViewModel.WarehouseAdjustmentID == null;
                this.gridexPalletDetails.Columns["Pallet" + CommonExpressions.PropertyName<GoodsReceiptDetailDTO>(p => p.PrimaryEntryDate)].Visible = this.goodsReceiptViewModel.PickupID == null && this.goodsReceiptViewModel.GoodsIssueID == null && this.goodsReceiptViewModel.WarehouseAdjustmentID == null;

                this.gridexCartonDetails.Columns["Carton" + CommonExpressions.PropertyName<GoodsReceiptDetailDTO>(p => p.PrimaryReference)].Visible = this.goodsReceiptViewModel.PickupID == null && this.goodsReceiptViewModel.GoodsIssueID == null && this.goodsReceiptViewModel.WarehouseAdjustmentID == null;
                this.gridexCartonDetails.Columns["Carton" + CommonExpressions.PropertyName<GoodsReceiptDetailDTO>(p => p.PrimaryEntryDate)].Visible = this.goodsReceiptViewModel.PickupID == null && this.goodsReceiptViewModel.GoodsIssueID == null && this.goodsReceiptViewModel.WarehouseAdjustmentID == null;
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new GoodsReceiptController(CommonNinject.Kernel.Get<IGoodsReceiptService>(), this.goodsReceiptViewModel); }
        }

        public override void Loading()
        {
            this.fastGoodsReceiptIndex.SetObjects(this.goodsReceiptAPIs.GetGoodsReceiptIndexes());

            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastGoodsReceiptIndex.Sort(this.olvEntryDate, SortOrder.Descending);
        }

        protected override DialogResult wizardMaster()
        {
            WizardMaster wizardMaster = new WizardMaster(this.goodsReceiptAPIs, this.goodsReceiptViewModel);
            DialogResult dialogResult = wizardMaster.ShowDialog();

            wizardMaster.Dispose();
            return dialogResult;
        }

        protected override void wizardDetail()
        {
            base.wizardDetail();
            WizardDetail wizardDetail = new WizardDetail(this.goodsReceiptAPIs, this.goodsReceiptViewModel);
            wizardDetail.ShowDialog();

            wizardDetail.Dispose();
        }

        private void buttonAddDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.EditableMode)
                    this.wizardDetail();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void naviGroupDetails_HeaderMouseClick(object sender, MouseEventArgs e)
        {
            this.toolStripNaviGroup.Visible = this.naviGroupDetails.Expanded;
        }

        private void buttonExpandTop_Click(object sender, EventArgs e)
        {
            if (this.naviGroupTop.Tag.ToString() == "Expandable" || this.naviGroupTop.Expanded)
            {
                this.naviGroupTop.Expanded = !this.naviGroupTop.Expanded;
                this.naviGroupTop.Padding = new Padding(0, 0, 0, 0);
                this.buttonExpandTop.Image = this.naviGroupTop.Expanded ? Resources.chevron : Resources.chevron_expand;
            }
        }

        protected override PrintViewModel InitPrintViewModel()
        {
            PrintViewModel printViewModel = base.InitPrintViewModel();
            printViewModel.ReportPath = "GoodsReceiptSheet";
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("GoodsReceiptID", this.goodsReceiptViewModel.GoodsReceiptID.ToString()));
            return printViewModel;
        }

    }
}
