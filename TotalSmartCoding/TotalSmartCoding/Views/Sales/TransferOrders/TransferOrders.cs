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

using TotalSmartCoding.Controllers.Sales;
using TotalCore.Repositories.Sales;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalCore.Services.Sales;
using TotalSmartCoding.ViewModels.Sales;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using TotalBase;
using TotalModel.Models;
using TotalDTO.Sales;
using BrightIdeasSoftware;
using TotalSmartCoding.Libraries.StackedHeaders;


namespace TotalSmartCoding.Views.Sales.TransferOrders
{
    public partial class TransferOrders : BaseView
    {
        private CustomTabControl customTabLeft;
        private CustomTabControl customTabCenter;

        private TransferOrderAPIs transferOrderAPIs;
        private TransferOrderViewModel transferOrderViewModel { get; set; }

        public TransferOrders()
            : base()
        {
            InitializeComponent();


            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastTransferOrderIndex;

            this.transferOrderAPIs = new TransferOrderAPIs(CommonNinject.Kernel.Get<ITransferOrderAPIRepository>());

            this.transferOrderViewModel = CommonNinject.Kernel.Get<TransferOrderViewModel>();
            this.transferOrderViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.transferOrderViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                #region TabLeft
                this.customTabLeft = new CustomTabControl();
                this.customTabLeft.DisplayStyle = TabStyle.VisualStudio;

                this.customTabLeft.TabPages.Add("tabLeftAA", "Transfer Order  ");
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Order Lines            ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Description            ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Remarks                    ");

                this.customTabCenter.TabPages[0].Controls.Add(this.gridexViewDetails);
                this.customTabCenter.TabPages[1].Controls.Add(this.textexDescription);
                this.customTabCenter.TabPages[2].Controls.Add(this.textexRemarks);
                this.customTabCenter.TabPages[1].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[2].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.gridexViewDetails.Dock = DockStyle.Fill;
                this.textexDescription.Dock = DockStyle.Fill;
                this.textexRemarks.Dock = DockStyle.Fill;

                this.customTabCenter.Dock = DockStyle.Fill;
                this.panelCenter.Controls.Add(this.customTabCenter);
                #endregion TabCenter

                this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].SizeType = SizeType.Absolute; this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].Width = 15;

                this.buttonExpandTop.Visible = this.naviGroupTop.Tag.ToString() == "Expandable";
                this.buttonExpandTop_Click(this.buttonExpandTop, new EventArgs());
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingEntryDate;
        Binding bindingWarehouseName;
        Binding bindingVoucherCode;
        Binding bindingVehicle;
        Binding bindingVehicleDriver;
        Binding bindingTransferJobs;
        Binding bindingDescription;
        Binding bindingRemarks;
        Binding bindingCaption;

        Binding bindingWarehouseReceiptID;
        Binding bindingTransferOrderTypeID;
        Binding bindingTransferPackageTypeID;

        Binding bindingForkliftDriverID;
        Binding bindingStorekeeperID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingWarehouseName = this.textexWarehouseName.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.WarehouseName), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVoucherCode = this.textexVoucherCode.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.VoucherCode), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVehicle = this.textexVehicle.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.Vehicle), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVehicleDriver = this.textexVehicleDriver.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.VehicleDriver), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingTransferJobs = this.textexTransferJobs.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.TransferJobs), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCaption = this.labelCaption.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderDTO>(p => p.Caption));

            WarehouseAPIs warehouseReceiptAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());

            this.combexWarehouseReceiptID.DataSource = warehouseReceiptAPIs.GetWarehouseBases();
            this.combexWarehouseReceiptID.DisplayMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.Name);
            this.combexWarehouseReceiptID.ValueMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.WarehouseID);
            this.bindingWarehouseReceiptID = this.combexWarehouseReceiptID.DataBindings.Add("SelectedValue", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.WarehouseReceiptID), true, DataSourceUpdateMode.OnPropertyChanged);

            TransferOrderTypeAPIs transferOrderTypeAPIs = new TransferOrderTypeAPIs(CommonNinject.Kernel.Get<ITransferOrderTypeAPIRepository>());

            this.combexTransferOrderTypeID.DataSource = transferOrderTypeAPIs.GetTransferOrderTypeBases();
            this.combexTransferOrderTypeID.DisplayMember = CommonExpressions.PropertyName<TransferOrderTypeBase>(p => p.Name);
            this.combexTransferOrderTypeID.ValueMember = CommonExpressions.PropertyName<TransferOrderTypeBase>(p => p.TransferOrderTypeID);
            this.bindingTransferOrderTypeID = this.combexTransferOrderTypeID.DataBindings.Add("SelectedValue", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.TransferOrderTypeID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.combexTransferPackageTypeID.DataSource = transferOrderTypeAPIs.GetTransferPackageTypeBases();
            this.combexTransferPackageTypeID.DisplayMember = CommonExpressions.PropertyName<TransferPackageTypeBase>(p => p.Name);
            this.combexTransferPackageTypeID.ValueMember = CommonExpressions.PropertyName<TransferPackageTypeBase>(p => p.TransferPackageTypeID);
            this.bindingTransferPackageTypeID = this.combexTransferPackageTypeID.DataBindings.Add("SelectedValue", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.TransferPackageTypeID), true, DataSourceUpdateMode.OnPropertyChanged);

            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

            this.combexForkliftDriverID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.transferOrderViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexForkliftDriverID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexForkliftDriverID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingForkliftDriverID = this.combexForkliftDriverID.DataBindings.Add("SelectedValue", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.ForkliftDriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.combexStorekeeperID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.transferOrderViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexStorekeeperID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexStorekeeperID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingStorekeeperID = this.combexStorekeeperID.DataBindings.Add("SelectedValue", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.StorekeeperID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingWarehouseName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVoucherCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVehicle.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVehicleDriver.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingTransferJobs.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCaption.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingWarehouseReceiptID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingTransferOrderTypeID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingTransferPackageTypeID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingForkliftDriverID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastTransferOrderIndex.AboutToCreateGroups += fastTransferOrderIndex_AboutToCreateGroups;

            this.fastTransferOrderIndex.ShowGroups = true;
            this.olvApproved.Renderer = new MappedImageRenderer(new Object[] { 1, Resources.Placeholder16, 2, Resources.Void_16 });
            this.naviGroupDetails.ExpandedHeight = this.naviGroupDetails.Size.Height;
        }

        protected override void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            base.CommonControl_BindingComplete(sender, e);
            if (this.EditableMode)
            {
                if (sender.Equals(this.bindingWarehouseReceiptID))
                {
                    if (this.combexWarehouseReceiptID.SelectedItem != null)
                    {
                        WarehouseBase warehouseBase = (WarehouseBase)this.combexWarehouseReceiptID.SelectedItem;
                        this.transferOrderViewModel.WarehouseReceiptName = warehouseBase.Name;
                    }
                }
            }
        }

        private void fastTransferOrderIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Sign_Order_32";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Order(s)";
                }
            }
        }

        private BindingSource bindingSourceViewDetails = new BindingSource();

        protected override void InitializeDataGridBinding()
        {
            base.InitializeDataGridBinding();
            this.InitializeDataGridReadonlyColumns(this.gridexViewDetails);

            this.gridexViewDetails.AutoGenerateColumns = false;
            this.gridexViewDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.bindingSourceViewDetails.DataSource = this.transferOrderViewModel.ViewDetails;
            this.gridexViewDetails.DataSource = this.bindingSourceViewDetails;

            this.bindingSourceViewDetails.AddingNew += bindingSourceViewDetails_AddingNew;
            this.transferOrderViewModel.ViewDetails.ListChanged += ViewDetails_ListChanged;
            this.gridexViewDetails.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dataGridViewDetails_EditingControlShowing);
            this.gridexViewDetails.ReadOnlyChanged += new System.EventHandler(this.dataGrid_ReadOnlyChanged);

            DataGridViewComboBoxColumn comboBoxColumn;
            CommodityAPIs commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>());

            comboBoxColumn = (DataGridViewComboBoxColumn)this.gridexViewDetails.Columns[CommonExpressions.PropertyName<TransferOrderDetailDTO>(p => p.CommodityID)];
            comboBoxColumn.DataSource = commodityAPIs.GetCommodityBases(true);
            comboBoxColumn.DisplayMember = CommonExpressions.PropertyName<CommodityBase>(p => p.Code);
            comboBoxColumn.ValueMember = CommonExpressions.PropertyName<CommodityBase>(p => p.CommodityID);

            StackedHeaderDecorator stackedHeaderDecorator = new StackedHeaderDecorator(this.gridexViewDetails);
        }

        private void bindingSourceViewDetails_AddingNew(object sender, AddingNewEventArgs e)
        {   //ONLY WHEN USING COMBOBOX TO ADD NEW ROW TO datagridview => WE SHOULD USE BindingSource => THEN WE HANDLE AN EVENT HANDLER FOR AddingNew EVENT
            //In this form, the datagridview using a combobox for add new item => add new row to the datagridview
            //If user cancel the combobox => the datagridview will not cancel new adding row (event no new row added???)
            //This will raise error when user move the cursor to the new row (means: the datagridview will add new row again!!!)
            //I find an workarround to handle this error from this https://stackoverflow.com/questions/2359124/datagridview-throwing-invalidoperationexception-operation-is-not-valid-whe
            //The following code: will remove current pending new row => in order add another new row again
            if (this.gridexViewDetails.Rows.Count == this.bindingSourceViewDetails.Count)
                this.bindingSourceViewDetails.RemoveAt(this.bindingSourceViewDetails.Count - 1);
            //-----------The following is explanation from internet (the link above): The reason it works is because on a DataGridView where AllowUserToAddRows is true, it adds an empty row at the end of its rows which if bound to a list creates a null element at the end of your list. Your code removes that element and then the AddNew in the BindingList will trigger the DataGridView to add it again. 
            //This code bypass the error, BUT NOT SOLVE THE PROBLEM COMPLETELY. SO: WE SHOULD ADVICE USER NOT CANCEL THE COMBOBOX => INSTEAD: CANCEL THE ROW AFTER SELECT THE COMBOBOX
        }

        private void ViewDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
                this.customTabCenter.TabPages[0].Text = "Order Lines [" + this.transferOrderViewModel.ViewDetails.Count.ToString("N0") + " item(s)]             ";

            if (this.EditableMode && e.PropertyDescriptor != null && e.NewIndex >= 0 && e.NewIndex < this.transferOrderViewModel.ViewDetails.Count)
            {
                TransferOrderDetailDTO transferOrderDetailDTO = this.transferOrderViewModel.ViewDetails[e.NewIndex];
                if (transferOrderDetailDTO != null)
                    this.CalculateQuantityDetailDTO(transferOrderDetailDTO, e.PropertyDescriptor.Name, null, this.transferOrderViewModel.TransferOrderID);
            }
        }

        protected override void invokeEdit(int? id)
        {
            base.invokeEdit(id);
            this.customizeColumnWidth();
        }

        private void customizeColumnWidth()
        {
            bool hasOptionBatches = this.transferOrderViewModel.HasOptionBatches;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<TransferOrderDetailDTO>(p => p.BatchCode)].Visible = hasOptionBatches;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<TransferOrderDetailDTO>(p => p.QuantityBatchAvailable)].Visible = hasOptionBatches;
        }

        protected override Controllers.BaseController myController
        {
            get { return new TransferOrderController(CommonNinject.Kernel.Get<ITransferOrderService>(), this.transferOrderViewModel); }
        }

        public override void Loading()
        {
            this.fastTransferOrderIndex.SetObjects(this.transferOrderAPIs.GetTransferOrderIndexes());

            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastTransferOrderIndex.Sort(this.olvEntryDate, SortOrder.Descending);
        }

        protected override DialogResult wizardMaster()
        {
            WizardMaster wizardMaster = new WizardMaster(this.transferOrderViewModel);
            DialogResult dialogResult = wizardMaster.ShowDialog();

            wizardMaster.Dispose();
            return dialogResult;
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

        private void menuOptionBatches_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.EditableMode)
                {
                    TransferOrderDetailDTO transferOrderDetailDTO = this.gridexViewDetails.CurrentRow.DataBoundItem as TransferOrderDetailDTO;
                    if (transferOrderDetailDTO != null)
                    {
                        transferOrderDetailDTO.TransferOrderID = this.transferOrderViewModel.TransferOrderID;
                        transferOrderDetailDTO.LocationID = this.transferOrderViewModel.LocationID;
                        OptionBatches optionBatches = new OptionBatches(transferOrderDetailDTO);
                        optionBatches.ShowDialog(); this.customizeColumnWidth();

                        optionBatches.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
    }
}
