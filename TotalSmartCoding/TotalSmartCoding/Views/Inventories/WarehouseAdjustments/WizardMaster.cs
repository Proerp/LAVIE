using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using Ninject;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Commons;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Inventories;
using TotalBase.Enums;


namespace TotalSmartCoding.Views.Inventories.WarehouseAdjustments
{
    public partial class WizardMaster : Form
    {
        private WarehouseAdjustmentViewModel warehouseAdjustmentViewModel;

        Binding bindingWarehouseID;
        Binding bindingWarehouseReceiptID;
        Binding bindingWarehouseAdjustmentTypeID;
        Binding bindingStorekeeperID;

        Binding bindingAdjustmentJobs;
        Binding bindingDescription;
        Binding bindingRemarks;

        public WizardMaster(WarehouseAdjustmentViewModel warehouseAdjustmentViewModel)
        {
            InitializeComponent();

            this.warehouseAdjustmentViewModel = warehouseAdjustmentViewModel;
        }

        private void WizardMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.warehouseAdjustmentViewModel.PropertyChanged += warehouseAdjustmentDetailDTO_PropertyChanged;

                WarehouseAPIs warehouseAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());
                this.combexWarehouseID.DataSource = warehouseAPIs.GetWarehouseBases();
                this.combexWarehouseID.DisplayMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.Name);
                this.combexWarehouseID.ValueMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.WarehouseID);
                this.bindingWarehouseID = this.combexWarehouseID.DataBindings.Add("SelectedValue", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.WarehouseID), true, DataSourceUpdateMode.OnPropertyChanged);

                WarehouseAPIs warehouseReceiptAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());
                this.combexWarehouseReceiptID.DataSource = warehouseReceiptAPIs.GetWarehouseBases();
                this.combexWarehouseReceiptID.DisplayMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.Name);
                this.combexWarehouseReceiptID.ValueMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.WarehouseID);
                this.bindingWarehouseReceiptID = this.combexWarehouseReceiptID.DataBindings.Add("SelectedValue", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.WarehouseReceiptID), true, DataSourceUpdateMode.OnPropertyChanged);
                this.combexWarehouseReceiptID.DataBindings.Add("Enabled", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.WarehouseReceiptEnabled), true, DataSourceUpdateMode.OnPropertyChanged);

                WarehouseAdjustmentTypeAPIs warehouseAdjustmentTypeAPIs = new WarehouseAdjustmentTypeAPIs(CommonNinject.Kernel.Get<IWarehouseAdjustmentTypeAPIRepository>());

                this.combexWarehouseAdjustmentTypeID.DataSource = warehouseAdjustmentTypeAPIs.GetWarehouseAdjustmentTypeBases();
                this.combexWarehouseAdjustmentTypeID.DisplayMember = CommonExpressions.PropertyName<WarehouseAdjustmentTypeBase>(p => p.Name);
                this.combexWarehouseAdjustmentTypeID.ValueMember = CommonExpressions.PropertyName<WarehouseAdjustmentTypeBase>(p => p.WarehouseAdjustmentTypeID);
                this.bindingWarehouseAdjustmentTypeID = this.combexWarehouseAdjustmentTypeID.DataBindings.Add("SelectedValue", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.WarehouseAdjustmentTypeID), true, DataSourceUpdateMode.OnPropertyChanged);

                EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

                this.combexStorekeeperID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.warehouseAdjustmentViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
                this.combexStorekeeperID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
                this.combexStorekeeperID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
                this.bindingStorekeeperID = this.combexStorekeeperID.DataBindings.Add("SelectedValue", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.StorekeeperID), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingAdjustmentJobs = this.textexAdjustmentJobs.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.AdjustmentJobs), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.warehouseAdjustmentViewModel, CommonExpressions.PropertyName<WarehouseAdjustmentViewModel>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingWarehouseID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingWarehouseReceiptID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingWarehouseAdjustmentTypeID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.bindingAdjustmentJobs.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.errorProviderMaster.DataSource = this.warehouseAdjustmentViewModel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void warehouseAdjustmentDetailDTO_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.buttonOK.Enabled = this.warehouseAdjustmentViewModel.IsValid;
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception) { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
            if (sender.Equals(this.bindingWarehouseID))
            {
                if (this.combexWarehouseID.SelectedItem != null)
                {
                    WarehouseBase warehouseBase = (WarehouseBase)this.combexWarehouseID.SelectedItem;
                    this.warehouseAdjustmentViewModel.WarehouseName = warehouseBase.Name;
                }
            }
            if (sender.Equals(this.bindingWarehouseReceiptID))
            {
                if (this.combexWarehouseReceiptID.SelectedItem != null)
                {
                    WarehouseBase warehouseBase = (WarehouseBase)this.combexWarehouseReceiptID.SelectedItem;
                    this.warehouseAdjustmentViewModel.WarehouseReceiptName = warehouseBase.Name;
                }
            }
            if (sender.Equals(this.bindingWarehouseAdjustmentTypeID))
            {
                if (this.combexWarehouseAdjustmentTypeID.SelectedItem != null)
                {
                    WarehouseAdjustmentTypeBase warehouseAdjustmentTypeBase = (WarehouseAdjustmentTypeBase)this.combexWarehouseAdjustmentTypeID.SelectedItem;
                    this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeName = warehouseAdjustmentTypeBase.Name;
                }
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                    this.DialogResult = DialogResult.OK;

                if (sender.Equals(this.buttonESC))
                    this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }



    }
}
