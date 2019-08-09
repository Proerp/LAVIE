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


namespace TotalSmartCoding.Views.Inventories.WarehouseAdjustments
{
    public partial class WarehouseAdjustments : BaseView
    {
        private CustomTabControl customTabLeft;
        private CustomTabControl customTabCenter;
        private CustomTabControl customTabCenterPositive;
        private CustomTabControl customTabCenterNegative;

        private WarehouseAdjustmentAPIs warehouseAdjustmentAPIs;
        private WarehouseAdjustmentViewModel warehouseAdjustmentViewModel { get; set; }

        public WarehouseAdjustments()
            : base()
        {
            InitializeComponent();


            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastWarehouseAdjustmentIndex;

            this.warehouseAdjustmentAPIs = new WarehouseAdjustmentAPIs(CommonNinject.Kernel.Get<IWarehouseAdjustmentAPIRepository>());

            this.warehouseAdjustmentViewModel = CommonNinject.Kernel.Get<WarehouseAdjustmentViewModel>();
            this.warehouseAdjustmentViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.warehouseAdjustmentViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                #region TabLeft
                this.customTabLeft = new CustomTabControl();
                this.customTabLeft.DisplayStyle = TabStyle.VisualStudio;

                this.customTabLeft.TabPages.Add("tabLeftAA", "Warehouse Adjustment ");
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

                this.customTabCenter.TabPages.Add("tabCenterNegative", "Negative adjustment quantities      ");
                this.customTabCenter.TabPages.Add("tabCenterPositive", "Positive adjustment quantities      ");
                this.customTabCenter.TabPages.Add("tabCenterDescription", "Description            ");
                this.customTabCenter.TabPages.Add("tabCenterRemarks", "Remarks                    ");

                this.customTabCenterPositive = new CustomTabControl();
                this.customTabCenterPositive.DisplayStyle = TabStyle.Rounded;
                this.customTabCenterPositive.TabPages.Add("tabCenterPositivePallets", "Pallets   ");
                this.customTabCenterPositive.TabPages.Add("tabCenterNegativeCartons", "Cartons   ");
                this.customTabCenterPositive.TabPages[0].Controls.Add(this.gridexPositivePalletDetails);
                this.customTabCenterPositive.TabPages[0].Controls.Add(this.toolStripPositivePallet);
                this.customTabCenterPositive.TabPages[1].Controls.Add(this.gridexPositiveCartonDetails);
                this.customTabCenterPositive.TabPages[1].Controls.Add(this.toolStripPositiveCarton);

                this.customTabCenterNegative = new CustomTabControl();
                this.customTabCenterNegative.DisplayStyle = TabStyle.Rounded;
                this.customTabCenterNegative.TabPages.Add("tabCenterNegativePallets", "Pallets   ");
                this.customTabCenterNegative.TabPages.Add("tabCenterNegativeCartons", "Cartons   ");
                this.customTabCenterNegative.TabPages[0].Controls.Add(this.gridexNegativePalletDetails);
                this.customTabCenterNegative.TabPages[0].Controls.Add(this.toolStripNegativePallet);
                this.customTabCenterNegative.TabPages[1].Controls.Add(this.gridexNegativeCartonDetails);
                this.customTabCenterNegative.TabPages[1].Controls.Add(this.toolStripNegativeCarton);

                this.customTabCenter.TabPages[0].Controls.Add(this.customTabCenterNegative);
                this.customTabCenter.TabPages[1].Controls.Add(this.customTabCenterPositive);                
                this.customTabCenter.TabPages[2].Controls.Add(this.textexDescription);
                this.customTabCenter.TabPages[3].Controls.Add(this.textexRemarks);
                this.customTabCenter.TabPages[2].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[3].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenterPositive.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.customTabCenterPositive.TabPages[1].BackColor = this.panelCenter.BackColor;
                this.customTabCenterNegative.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.customTabCenterNegative.TabPages[1].BackColor = this.panelCenter.BackColor;

                this.toolStripPositivePallet.Dock = DockStyle.Left;
                this.gridexPositivePalletDetails.Dock = DockStyle.Fill;
                this.toolStripPositiveCarton.Dock = DockStyle.Left;
                this.gridexPositiveCartonDetails.Dock = DockStyle.Fill;

                this.toolStripNegativePallet.Dock = DockStyle.Left;
                this.gridexNegativePalletDetails.Dock = DockStyle.Fill;
                this.toolStripNegativeCarton.Dock = DockStyle.Left;
                this.gridexNegativeCartonDetails.Dock = DockStyle.Fill;
                
                this.textexDescription.Dock = DockStyle.Fill;
                this.textexRemarks.Dock = DockStyle.Fill;

                this.customTabCenter.Dock = DockStyle.Fill;
                this.customTabCenterPositive.Dock = DockStyle.Fill;
                this.customTabCenterNegative.Dock = DockStyle.Fill;
                this.panelCenter.Controls.Add(this.customTabCenter);
                #endregion TabCenter

                this.tableLayoutPanelExtend.ColumnStyles[this.tableLayoutPanelExtend.ColumnCount - 1].SizeType = SizeType.Absolute; this.tableLayoutPanelExtend.ColumnStyles[this.tableLayoutPanelExtend.ColumnCount - 1].Width = 15;

                this.buttonExpandTop.Visible = this.naviGroupTop.Tag.ToString() == "Expandable";
                this.buttonExpandTop_Click(this.buttonExpandTop, new EventArgs());

            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingEntryDate;
        Binding bindingReference;
        Binding bindingWarehouseName;
        Binding bindingAdjustmentJobs;
        Binding bindingDescription;
        Binding bindingRemarks;
        Binding bindingCaption;

        Binding bindingStorekeeperID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentDTO>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingReference = this.textexReference.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentDTO>(p => p.Reference), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingWarehouseName = this.textexWarehouseName.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentDTO>(p => p.WarehouseName), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingAdjustmentJobs = this.textexAdjustmentJobs.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentDTO>(p => p.AdjustmentJobs), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentDTO>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCaption = this.labelCaption.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentDTO>(p => p.Caption));

            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

            this.combexStorekeeperID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.warehouseAdjustmentViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexStorekeeperID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexStorekeeperID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingStorekeeperID = this.combexStorekeeperID.DataBindings.Add("SelectedValue", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.StorekeeperID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingReference.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingWarehouseName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingAdjustmentJobs.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCaption.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastWarehouseAdjustmentIndex.AboutToCreateGroups += fastWarehouseAdjustmentIndex_AboutToCreateGroups;

            this.fastWarehouseAdjustmentIndex.ShowGroups = true;
            this.olvApproved.Renderer = new MappedImageRenderer(new Object[] { false, Resources.Placeholder16 });
            this.naviGroupDetails.ExpandedHeight = this.naviGroupDetails.Size.Height;
        }

        private void fastWarehouseAdjustmentIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Adjustment_32";
                    olvGroup.Subtitle = "List count: " + olvGroup.Contents.Count.ToString();
                }
            }
        }

        protected override void InitializeDataGridBinding()
        {
            this.gridexPositivePalletDetails.AutoGenerateColumns = false;
            this.gridexPositiveCartonDetails.AutoGenerateColumns = false;
            this.gridexNegativePalletDetails.AutoGenerateColumns = false;
            this.gridexNegativeCartonDetails.AutoGenerateColumns = false;
            this.gridexPositivePalletDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexPositiveCartonDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexNegativePalletDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexNegativeCartonDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexPositivePalletDetails.DataSource = this.warehouseAdjustmentViewModel.PositivePalletDetails;
            this.gridexPositiveCartonDetails.DataSource = this.warehouseAdjustmentViewModel.PositiveCartonDetails;
            this.gridexNegativePalletDetails.DataSource = this.warehouseAdjustmentViewModel.NegativePalletDetails;
            this.gridexNegativeCartonDetails.DataSource = this.warehouseAdjustmentViewModel.NegativeCartonDetails;

            this.warehouseAdjustmentViewModel.ViewDetails.ListChanged += ViewDetails_ListChanged;

            StackedHeaderDecorator stackedHeaderDecoratorPositivePallet = new StackedHeaderDecorator(this.gridexPositivePalletDetails);
            StackedHeaderDecorator stackedHeaderDecoratorPositiveCarton = new StackedHeaderDecorator(this.gridexPositiveCartonDetails);
            StackedHeaderDecorator stackedHeaderDecoratorNegativePallet = new StackedHeaderDecorator(this.gridexNegativePalletDetails);
            StackedHeaderDecorator stackedHeaderDecoratorNegativeCarton = new StackedHeaderDecorator(this.gridexNegativeCartonDetails);
        }

        private void ViewDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
            {
                this.customTabCenterNegative.TabPages[0].Text = "-" + this.warehouseAdjustmentViewModel.NegativePalletDetails.Count.ToString("N0") + " Pallet" + (this.warehouseAdjustmentViewModel.NegativePalletDetails.Count > 1 ? "s" : "") + "        ";
                this.customTabCenterNegative.TabPages[1].Text = "-" + this.warehouseAdjustmentViewModel.NegativeCartonDetails.Count.ToString("N0") + " Carton" + (this.warehouseAdjustmentViewModel.NegativeCartonDetails.Count > 1 ? "s" : "") + "        ";
                this.customTabCenter.TabPages[0].Text = "Issue:  " + this.customTabCenterNegative.TabPages[0].Text.Trim() + ",  " + this.customTabCenterNegative.TabPages[1].Text.Trim() + "        ";

                this.customTabCenterPositive.TabPages[0].Text = "+" + this.warehouseAdjustmentViewModel.PositivePalletDetails.Count.ToString("N0") + " Pallet" + (this.warehouseAdjustmentViewModel.PositivePalletDetails.Count > 1 ? "s" : "") + "        ";
                this.customTabCenterPositive.TabPages[1].Text = "+" + this.warehouseAdjustmentViewModel.PositiveCartonDetails.Count.ToString("N0") + " Carton" + (this.warehouseAdjustmentViewModel.PositiveCartonDetails.Count > 1 ? "s" : "") + "        ";
                this.customTabCenter.TabPages[1].Text = "Receipt: " + this.customTabCenterPositive.TabPages[0].Text.Trim() + ", " + this.customTabCenterPositive.TabPages[1].Text.Trim() + "        ";                
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new WarehouseAdjustmentController(CommonNinject.Kernel.Get<IWarehouseAdjustmentService>(), this.warehouseAdjustmentViewModel); }
        }

        public override void Loading()
        {
            this.fastWarehouseAdjustmentIndex.SetObjects(this.warehouseAdjustmentAPIs.GetWarehouseAdjustmentIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastWarehouseAdjustmentIndex.Sort(this.olvEntryDate, SortOrder.Descending);
        }

        protected override DialogResult wizardMaster()
        {
            WizardMaster wizardMaster = new WizardMaster(this.warehouseAdjustmentViewModel);
            DialogResult dialogResult = wizardMaster.ShowDialog();

            wizardMaster.Dispose();
            return dialogResult;
        }

        protected override void wizardDetail()
        {
            base.wizardDetail();
            Form wizardDetail = new Form();

            if (this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.UnpackPallet)
                wizardDetail = new WizardUnpack(this.warehouseAdjustmentViewModel);

            if (this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.ChangeBinLocation || this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.HoldUnHold || this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.ReturnToProduction || this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.OtherIssues)
                wizardDetail = new WizardDetail(this.warehouseAdjustmentViewModel);

            if (wizardDetail is WizardUnpack || wizardDetail is WizardDetail)
            {
                wizardDetail.ShowDialog();
                wizardDetail.Dispose();
            }
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
    }
}
