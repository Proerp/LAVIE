using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

using Ninject;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Commons;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Sales;
using TotalBase.Enums;


namespace TotalSmartCoding.Views.Sales.TransferOrders
{
    public partial class WizardMaster : Form
    {
        private TransferOrderViewModel transferOrderViewModel;

        Binding bindingWarehouseID;
        Binding bindingWarehouseReceiptID;

        Binding bindingTransferOrderTypeID;
        Binding bindingTransferPackageTypeID;

        Binding bindingForkliftDriverID;
        Binding bindingStorekeeperID;

        
        Binding bindingEntryDate;
        Binding bindingVoucherCode;
        Binding bindingVehicle;
        Binding bindingVehicleDriver;
        Binding bindingTransferJobs;
        Binding bindingRemarks;

        public WizardMaster(TransferOrderViewModel transferOrderViewModel)
        {
            InitializeComponent();

            this.transferOrderViewModel = transferOrderViewModel;
        }

        private void WizardMaster_Load(object sender, EventArgs e)
        {
            try
            {
                this.transferOrderViewModel.PropertyChanged += transferOrderDetailDTO_PropertyChanged;

                WarehouseAPIs warehouseAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());

                this.combexWarehouseID.DataSource = warehouseAPIs.GetWarehouseBases();
                this.combexWarehouseID.DisplayMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.Name);
                this.combexWarehouseID.ValueMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.WarehouseID);
                this.bindingWarehouseID = this.combexWarehouseID.DataBindings.Add("SelectedValue", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.WarehouseID), true, DataSourceUpdateMode.OnPropertyChanged);

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
                
                this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingVoucherCode = this.textexVoucherCode.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.VoucherCode), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingVehicle = this.textexVehicle.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.Vehicle), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingVehicleDriver = this.textexVehicleDriver.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.VehicleDriver), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingTransferJobs = this.textexTransferJobs.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.TransferJobs), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.transferOrderViewModel, CommonExpressions.PropertyName<TransferOrderViewModel>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingWarehouseID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingWarehouseReceiptID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.bindingTransferOrderTypeID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingTransferPackageTypeID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            
                this.bindingForkliftDriverID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.bindingVoucherCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingVehicle.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingVehicleDriver.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingTransferJobs.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.errorProviderMaster.DataSource = this.transferOrderViewModel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void transferOrderDetailDTO_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.buttonOK.Enabled = this.transferOrderViewModel.IsValid;
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception) { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
            if (sender.Equals(this.bindingWarehouseID))
            {
                if (this.combexWarehouseID.SelectedItem != null)
                {
                    WarehouseBase warehouseBase = (WarehouseBase)this.combexWarehouseID.SelectedItem;
                    this.transferOrderViewModel.WarehouseName = warehouseBase.Name;
                }
            }
            if (sender.Equals(this.bindingWarehouseReceiptID))
            {
                if (this.combexWarehouseReceiptID.SelectedItem != null)
                {
                    WarehouseBase warehouseBase = (WarehouseBase)this.combexWarehouseReceiptID.SelectedItem;
                    this.transferOrderViewModel.WarehouseReceiptName = warehouseBase.Name;
                }
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.transferOrderViewModel.WarehouseID != null && this.transferOrderViewModel.WarehouseReceiptID != null && this.transferOrderViewModel.ForkliftDriverID != null)
                    this.DialogResult = DialogResult.OK;
                    else
                        CustomMsgBox.Show(this, "Vui lòng chọn kho xuất, kho nhập, và nhân viên đề nghị chuyển kho.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                }

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
