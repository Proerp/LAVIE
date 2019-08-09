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


namespace TotalSmartCoding.Views.Sales.DeliveryAdvices
{
    public partial class DeliveryAdvices : BaseView
    {
        private CustomTabControl customTabLeft;
        private CustomTabControl customTabCenter;

        private DeliveryAdviceAPIs deliveryAdviceAPIs;
        private DeliveryAdviceViewModel deliveryAdviceViewModel { get; set; }

        public DeliveryAdvices()
            : base()
        {
            InitializeComponent();


            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastDeliveryAdviceIndex;

            this.deliveryAdviceAPIs = new DeliveryAdviceAPIs(CommonNinject.Kernel.Get<IDeliveryAdviceAPIRepository>());

            this.deliveryAdviceViewModel = CommonNinject.Kernel.Get<DeliveryAdviceViewModel>();
            this.deliveryAdviceViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.deliveryAdviceViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                #region TabLeft
                this.customTabLeft = new CustomTabControl();
                this.customTabLeft.DisplayStyle = TabStyle.VisualStudio;

                this.customTabLeft.TabPages.Add("tabLeftAA", "Delivery Advice   ");
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Advice Line Details ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Description            ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Remarks                    ");

                this.customTabCenter.TabPages[0].Controls.Add(this.gridexViewDetails);
                this.customTabCenter.TabPages[0].Controls.Add(this.toolStripPallet);
                this.customTabCenter.TabPages[1].Controls.Add(this.textexDescription);
                this.customTabCenter.TabPages[2].Controls.Add(this.textexRemarks);
                this.customTabCenter.TabPages[1].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[2].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.toolStripPallet.Dock = DockStyle.Left;
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
        Binding bindingVoucherCode;
        Binding bindingVehicle;
        Binding bindingVehicleDriver;
        Binding bindingDeliveryAddress;
        Binding bindingDescription;
        Binding bindingRemarks;
        Binding bindingCaption;

        Binding bindingCustomerID;
        Binding bindingReceiverID;
        Binding bindingSalespersonID;
        Binding bindingForkliftDriverID;
        Binding bindingStorekeeperID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVoucherCode = this.textexVoucherCode.DataBindings.Add("Text", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.VoucherCode), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVehicle = this.textexVehicle.DataBindings.Add("Text", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.Vehicle), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVehicleDriver = this.textexVehicleDriver.DataBindings.Add("Text", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.VehicleDriver), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingDeliveryAddress = this.textexDeliveryAddress.DataBindings.Add("Text", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.ShippingAddress), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCaption = this.labelCaption.DataBindings.Add("Text", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceDTO>(p => p.Caption));

            CustomerAPIs customerAPIs = new CustomerAPIs(CommonNinject.Kernel.Get<ICustomerAPIRepository>());
            this.combexCustomerID.DataSource = customerAPIs.GetCustomerBases();
            this.combexCustomerID.DisplayMember = CommonExpressions.PropertyName<CustomerBase>(p => p.Name);
            this.combexCustomerID.ValueMember = CommonExpressions.PropertyName<CustomerBase>(p => p.CustomerID);
            this.bindingCustomerID = this.combexCustomerID.DataBindings.Add("SelectedValue", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceViewModel>(p => p.CustomerID), true, DataSourceUpdateMode.OnPropertyChanged);

            CustomerAPIs receiverAPIs = new CustomerAPIs(CommonNinject.Kernel.Get<ICustomerAPIRepository>());
            this.combexReceiverID.DataSource = receiverAPIs.GetCustomerBases();
            this.combexReceiverID.DisplayMember = CommonExpressions.PropertyName<CustomerBase>(p => p.Name);
            this.combexReceiverID.ValueMember = CommonExpressions.PropertyName<CustomerBase>(p => p.CustomerID);
            this.bindingReceiverID = this.combexReceiverID.DataBindings.Add("SelectedValue", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<SalesOrderViewModel>(p => p.ReceiverID), true, DataSourceUpdateMode.OnPropertyChanged);

            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

            this.combexSalespersonID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.deliveryAdviceViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Saleperson);
            this.combexSalespersonID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexSalespersonID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingSalespersonID = this.combexSalespersonID.DataBindings.Add("SelectedValue", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceViewModel>(p => p.SalespersonID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.combexForkliftDriverID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.deliveryAdviceViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexForkliftDriverID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexForkliftDriverID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingForkliftDriverID = this.combexForkliftDriverID.DataBindings.Add("SelectedValue", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceViewModel>(p => p.ForkliftDriverID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.combexStorekeeperID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.deliveryAdviceViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexStorekeeperID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexStorekeeperID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingStorekeeperID = this.combexStorekeeperID.DataBindings.Add("SelectedValue", this.deliveryAdviceViewModel, CommonExpressions.PropertyName<DeliveryAdviceViewModel>(p => p.StorekeeperID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVoucherCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVehicle.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVehicleDriver.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDeliveryAddress.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCaption.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingCustomerID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingReceiverID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingSalespersonID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingForkliftDriverID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastDeliveryAdviceIndex.AboutToCreateGroups += fastDeliveryAdviceIndex_AboutToCreateGroups;

            this.fastDeliveryAdviceIndex.ShowGroups = true;
            this.olvApproved.Renderer = new MappedImageRenderer(new Object[] { 1, Resources.Placeholder16, 2, Resources.Void_16 });
            this.naviGroupDetails.ExpandedHeight = this.naviGroupDetails.Size.Height;
        }

        protected override void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            base.CommonControl_BindingComplete(sender, e);
            if (this.EditableMode)
            {
                if (sender.Equals(this.bindingCustomerID) && this.combexCustomerID.SelectedItem != null)
                {
                    CustomerBase customerBase = (CustomerBase)this.combexCustomerID.SelectedItem;
                    this.deliveryAdviceViewModel.CustomerName = customerBase.Name; //HERE: CHANGE => DONT SET setDirty: SEE ApplyPropertyChange FOR MORE INFO
                    //THIS CommonControl_BindingComplete WILL BE RAISED FOR EVERY BINDING => SO WE CAN NOT UPDATE RELATIVE PROPERTY BY THIS WAY. SHOULD THINK OF NEW WAY FOR UPDATE SUCH RELATIVE PROPERTY (SUCH AS: ContactInfo, ShippingAddress OF Customer)
                    //this.deliveryAdviceViewModel.ContactInfo = customerBase.ContactInfo;
                    //this.deliveryAdviceViewModel.ShippingAddress = customerBase.ShippingAddress;
                }
                if (sender.Equals(this.bindingReceiverID) && this.combexReceiverID.SelectedItem != null)
                {
                    CustomerBase receiverBase = (CustomerBase)this.combexReceiverID.SelectedItem;
                    this.deliveryAdviceViewModel.ReceiverName = receiverBase.Name;
                    //THIS CommonControl_BindingComplete WILL BE RAISED FOR EVERY BINDING => SO WE CAN NOT UPDATE RELATIVE PROPERTY BY THIS WAY. SHOULD THINK OF NEW WAY FOR UPDATE SUCH RELATIVE PROPERTY (SUCH AS: ContactInfo, ShippingAddress OF Receiver)
                    //this.deliveryAdviceViewModel.ContactInfo = receiverBase.ContactInfo;
                    //this.deliveryAdviceViewModel.ShippingAddress = receiverBase.ShippingAddress;
                }
            }
        }

        protected override void EditableModeChanged(bool editableMode)
        {
            base.EditableModeChanged(editableMode);
            this.menuOptionBatches.Visible = editableMode;
        }

        private void fastDeliveryAdviceIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Schedule-32";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Advice(s)";
                }
            }
        }

        protected override void InitializeDataGridBinding()
        {
            base.InitializeDataGridBinding();
            this.InitializeDataGridReadonlyColumns(this.gridexViewDetails);

            this.gridexViewDetails.AutoGenerateColumns = false;
            this.gridexViewDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            this.gridexViewDetails.DataSource = this.deliveryAdviceViewModel.ViewDetails;

            this.deliveryAdviceViewModel.ViewDetails.ListChanged += ViewDetails_ListChanged;
            this.gridexViewDetails.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.dataGridViewDetails_EditingControlShowing);
            this.gridexViewDetails.ReadOnlyChanged += new System.EventHandler(this.dataGrid_ReadOnlyChanged);

            DataGridViewComboBoxColumn comboBoxColumn;
            CommodityAPIs commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>());

            comboBoxColumn = (DataGridViewComboBoxColumn)this.gridexViewDetails.Columns[CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.CommodityID)];
            comboBoxColumn.DataSource = commodityAPIs.GetCommodityBases(true);
            comboBoxColumn.DisplayMember = CommonExpressions.PropertyName<CommodityBase>(p => p.Code);
            comboBoxColumn.ValueMember = CommonExpressions.PropertyName<CommodityBase>(p => p.CommodityID);

            StackedHeaderDecorator stackedHeaderDecorator = new StackedHeaderDecorator(this.gridexViewDetails);
        }

        private void ViewDetails_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.Reset)
            {
                this.customTabCenter.TabPages[0].Text = "Advice Line Details [" + this.deliveryAdviceViewModel.ViewDetails.Count.ToString("N0") + " item(s)]             ";

                this.customizeColumnWidth();
            }

            if (this.EditableMode && e.PropertyDescriptor != null && e.NewIndex >= 0 && e.NewIndex < this.deliveryAdviceViewModel.ViewDetails.Count)
            {
                DeliveryAdviceDetailDTO deliveryAdviceDetailDTO = this.deliveryAdviceViewModel.ViewDetails[e.NewIndex];
                if (deliveryAdviceDetailDTO != null)
                    this.CalculateQuantityDetailDTO(deliveryAdviceDetailDTO, e.PropertyDescriptor.Name, this.deliveryAdviceViewModel.DeliveryAdviceID, null);
            }
        }

        private void customizeColumnWidth()
        {
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.SalesOrderReference)].Visible = this.deliveryAdviceViewModel.SalesOrderID == null;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.SalesOrderEntryDate)].Visible = this.deliveryAdviceViewModel.SalesOrderID == null;

            bool hasOptionBatches = this.deliveryAdviceViewModel.HasOptionBatches;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.BatchCode)].Visible = hasOptionBatches;
            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.QuantityBatchAvailable)].Visible = hasOptionBatches;

            this.gridexViewDetails.Columns[CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.CommodityName)].Visible = !(this.deliveryAdviceViewModel.SalesOrderID == null && hasOptionBatches);
        }

        protected override Controllers.BaseController myController
        {
            get { return new DeliveryAdviceController(CommonNinject.Kernel.Get<IDeliveryAdviceService>(), this.deliveryAdviceViewModel); }
        }

        public override void Loading()
        {
            this.fastDeliveryAdviceIndex.SetObjects(this.deliveryAdviceAPIs.GetDeliveryAdviceIndexes());

            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastDeliveryAdviceIndex.Sort(this.olvEntryDate, SortOrder.Descending);
        }

        protected override DialogResult wizardMaster()
        {
            WizardMaster wizardMaster = new WizardMaster(this.deliveryAdviceAPIs, this.deliveryAdviceViewModel);
            DialogResult dialogResult = wizardMaster.ShowDialog();

            wizardMaster.Dispose();
            return dialogResult;
        }

        protected override void wizardDetail()
        {
            base.wizardDetail();
            WizardDetail wizardDetail = new WizardDetail(this.deliveryAdviceAPIs, this.deliveryAdviceViewModel);
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

        private void menuOptionBatches_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.EditableMode)
                {
                    DeliveryAdviceDetailDTO deliveryAdviceDetailDTO = this.gridexViewDetails.CurrentRow.DataBoundItem as DeliveryAdviceDetailDTO;
                    if (deliveryAdviceDetailDTO != null)
                    {
                        deliveryAdviceDetailDTO.DeliveryAdviceID = this.deliveryAdviceViewModel.DeliveryAdviceID;
                        deliveryAdviceDetailDTO.LocationID = this.deliveryAdviceViewModel.LocationID;
                        OptionBatches optionBatches = new OptionBatches(deliveryAdviceDetailDTO);
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
