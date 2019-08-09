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

using TotalSmartCoding.Controllers.Commons;
using TotalCore.Repositories.Commons;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using TotalBase;
using TotalModel.Models;
using TotalDTO.Commons;
using BrightIdeasSoftware;
using TotalSmartCoding.Libraries.StackedHeaders;


namespace TotalSmartCoding.Views.Commons.Customers
{
    public partial class Customers : BaseView
    {
        private CustomTabControl customTabLeft;
        private CustomTabControl customTabCenter;

        private CustomerAPIs customerAPIs;
        private CustomerViewModel customerViewModel { get; set; }

        public Customers()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastCustomerIndex;

            this.customerAPIs = new CustomerAPIs(CommonNinject.Kernel.Get<ICustomerAPIRepository>());

            this.customerViewModel = CommonNinject.Kernel.Get<CustomerViewModel>();
            this.customerViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.customerViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                #region TabLeft
                this.customTabLeft = new CustomTabControl();
                this.customTabLeft.DisplayStyle = TabStyle.VisualStudio;
                this.customTabLeft.Font = this.panelLeft.Font;

                this.customTabLeft.TabPages.Add("tabLeftAA", "General  ");
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
                this.customTabCenter.Font = this.panelCenter.Font;

                this.customTabCenter.TabPages.Add("tabCenterAA", "Customer Information ");

                this.customTabCenter.TabPages[0].Controls.Add(this.layoutTop);
                this.customTabCenter.TabPages[0].Padding = new Padding(15, 0, 0, 0);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.layoutTop.Dock = DockStyle.Fill;

                this.panelCenter.Controls.Add(this.customTabCenter);
                this.customTabCenter.Dock = DockStyle.Fill;
                #endregion TabCenter

                this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].SizeType = SizeType.Absolute; this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].Width = 15;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingCode;
        Binding bindingName;
        Binding bindingOfficialName;
        
        Binding bindingContactInfo;
        Binding bindingVATCode;
        Binding bindingTelephone;
        Binding bindingFacsimile;
        Binding bindingEmail;
        Binding bindingAttentionName;
        
        Binding bindingBillingAddress;
        Binding bindingShippingAddress;
        Binding bindingRemarks;
        Binding bindingCaption;

        Binding bindingCustomerTypeID;
        Binding bindingCustomerCategoryID;
        Binding bindingTerritoryID;
        Binding bindingSalespersonID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingCode = this.textexCode.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.Code), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingName = this.textexName.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingOfficialName = this.textexOfficialName.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.OfficialName), true, DataSourceUpdateMode.OnPropertyChanged);
            
            this.bindingContactInfo = this.textexContactInfo.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.ContactInfo), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVATCode = this.textexVATCode.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.VATCode), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingTelephone = this.textexTelephone.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.Telephone), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingFacsimile = this.textexFacsimile.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.Facsimile), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingEmail = this.textexEmail.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.Email), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingAttentionName = this.textexAttentionName.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.AttentionName), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingBillingAddress = this.textexBillingAddress.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.BillingAddress), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingShippingAddress = this.textexShippingAddress.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.ShippingAddress), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCaption = this.labelCaption.DataBindings.Add("Text", this.customerViewModel, CommonExpressions.PropertyName<CustomerDTO>(p => p.Caption));

            CustomerTypeAPIs customerTypeAPIs = new CustomerTypeAPIs(CommonNinject.Kernel.Get<ICustomerTypeAPIRepository>());
            this.combexCustomerTypeID.DataSource = customerTypeAPIs.GetCustomerTypeBases();
            this.combexCustomerTypeID.DisplayMember = CommonExpressions.PropertyName<CustomerTypeBase>(p => p.Name);
            this.combexCustomerTypeID.ValueMember = CommonExpressions.PropertyName<CustomerTypeBase>(p => p.CustomerTypeID);
            this.bindingCustomerTypeID = this.combexCustomerTypeID.DataBindings.Add("SelectedValue", this.customerViewModel, CommonExpressions.PropertyName<CustomerViewModel>(p => p.CustomerTypeID), true, DataSourceUpdateMode.OnPropertyChanged);

            CustomerCategoryAPIs customerCategoryAPIs = new CustomerCategoryAPIs(CommonNinject.Kernel.Get<ICustomerCategoryAPIRepository>());
            this.combexCustomerCategoryID.DataSource = customerCategoryAPIs.GetCustomerCategoryBases();
            this.combexCustomerCategoryID.DisplayMember = CommonExpressions.PropertyName<CustomerCategoryBase>(p => p.Name);
            this.combexCustomerCategoryID.ValueMember = CommonExpressions.PropertyName<CustomerCategoryBase>(p => p.CustomerCategoryID);
            this.bindingCustomerCategoryID = this.combexCustomerCategoryID.DataBindings.Add("SelectedValue", this.customerViewModel, CommonExpressions.PropertyName<CustomerViewModel>(p => p.CustomerCategoryID), true, DataSourceUpdateMode.OnPropertyChanged);

            TerritoryAPIs territoryAPIs = new TerritoryAPIs(CommonNinject.Kernel.Get<ITerritoryAPIRepository>());
            this.combexTerritoryID.DataSource = territoryAPIs.GetTerritoryBases();
            this.combexTerritoryID.DisplayMember = CommonExpressions.PropertyName<TerritoryBase>(p => p.Name);
            this.combexTerritoryID.ValueMember = CommonExpressions.PropertyName<TerritoryBase>(p => p.TerritoryID);
            this.bindingTerritoryID = this.combexTerritoryID.DataBindings.Add("SelectedValue", this.customerViewModel, CommonExpressions.PropertyName<CustomerViewModel>(p => p.TerritoryID), true, DataSourceUpdateMode.OnPropertyChanged);

            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());
            this.combexSalespersonID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.customerViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Saleperson);
            this.combexSalespersonID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexSalespersonID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingSalespersonID = this.combexSalespersonID.DataBindings.Add("SelectedValue", this.customerViewModel, CommonExpressions.PropertyName<CustomerViewModel>(p => p.SalespersonID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingOfficialName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingContactInfo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVATCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingTelephone.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingFacsimile.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingEmail.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingAttentionName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingBillingAddress.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingShippingAddress.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCaption.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingCustomerTypeID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCustomerCategoryID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingTerritoryID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingSalespersonID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastCustomerIndex.AboutToCreateGroups += fastCustomerIndex_AboutToCreateGroups;

            this.fastCustomerIndex.ShowGroups = true;
            this.olvInActive.Renderer = new MappedImageRenderer(new Object[] { false, Resources.Placeholder16 });
            this.naviGroupDetails.ExpandedHeight = this.naviGroupDetails.Size.Height;
        }

        private void fastCustomerIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "CustomerRed";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Order(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new CustomerController(CommonNinject.Kernel.Get<ICustomerService>(), this.customerViewModel); }
        }

        public override void Loading()
        {
            this.fastCustomerIndex.SetObjects(this.customerAPIs.GetCustomerIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastCustomerIndex.Sort(this.olvSalespersonName, SortOrder.Descending);
        }
    }
}
