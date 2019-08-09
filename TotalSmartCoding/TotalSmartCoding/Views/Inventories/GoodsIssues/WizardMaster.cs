using System;
using System.Drawing;
using System.Windows.Forms;
using TotalBase;
using TotalBase.Enums;
using TotalModel.Models;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Inventories;

namespace TotalSmartCoding.Views.Inventories.GoodsIssues
{
    public partial class WizardMaster : Form
    {
        private GoodsIssueAPIs goodsIssueAPIs;
        private GoodsIssueViewModel goodsIssueViewModel;
        private CustomTabControl customTabBatch;
        public WizardMaster(GoodsIssueAPIs goodsIssueAPIs, GoodsIssueViewModel goodsIssueViewModel)
        {
            InitializeComponent();

            this.customTabBatch = new CustomTabControl();

            this.customTabBatch.Font = this.fastPendingDeliveryAdvices.Font;
            this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
            this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

            this.customTabBatch.TabPages.Add("tabPendingDeliveryAdvices", "Issue by every advice    ");
            this.customTabBatch.TabPages.Add("tabPendingDeliveryAdviceCustomers", "Cumulate advices per customer  ");
            this.customTabBatch.TabPages.Add("tabPendingDeliveryAdvices", "Issue by every transfer order ");
            this.customTabBatch.TabPages.Add("tabPendingDeliveryAdvices", "Cumulate transfer orders per warehouse ");
            this.customTabBatch.TabPages[0].Controls.Add(this.fastPendingDeliveryAdvices);
            this.customTabBatch.TabPages[1].Controls.Add(this.fastPendingDeliveryAdviceCustomers);
            this.customTabBatch.TabPages[2].Controls.Add(this.fastPendingTransferOrders);
            this.customTabBatch.TabPages[3].Controls.Add(this.fastPendingTransferOrderWarehouses);

            this.customTabBatch.Dock = DockStyle.Fill;

            this.fastPendingDeliveryAdvices.Dock = DockStyle.Fill;
            this.fastPendingDeliveryAdviceCustomers.Dock = DockStyle.Fill;

            this.fastPendingTransferOrders.Dock = DockStyle.Fill;
            this.fastPendingTransferOrderWarehouses.Dock = DockStyle.Fill;

            this.panelMaster.Controls.Add(this.customTabBatch);

            if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.GoodsIssue) ViewHelpers.SetFont(this, new Font("Calibri", 11), new Font("Calibri", 11), new Font("Calibri", 11));

            this.goodsIssueAPIs = goodsIssueAPIs;
            this.goodsIssueViewModel = goodsIssueViewModel;
        }


        private void Wizard_Load(object sender, EventArgs e)
        {
            try
            {
                this.fastPendingDeliveryAdvices.SetObjects(this.goodsIssueAPIs.GetPendingDeliveryAdvices(this.goodsIssueViewModel.LocationID));
                this.fastPendingDeliveryAdviceCustomers.SetObjects(this.goodsIssueAPIs.GetPendingDeliveryAdviceCustomers(this.goodsIssueViewModel.LocationID));

                this.fastPendingTransferOrders.SetObjects(this.goodsIssueAPIs.GetPendingTransferOrders(this.goodsIssueViewModel.LocationID));
                this.fastPendingTransferOrderWarehouses.SetObjects(this.goodsIssueAPIs.GetPendingTransferOrderWarehouses(this.goodsIssueViewModel.LocationID));
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    bool nextOK = false;
                    if (this.customTabBatch.SelectedIndex == 0 || this.customTabBatch.SelectedIndex == 1)
                    {
                        this.goodsIssueViewModel.GoodsIssueTypeID = (int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice;
                        if (this.customTabBatch.SelectedIndex == 0)
                        {
                            PendingDeliveryAdvice pendingDeliveryAdvice = (PendingDeliveryAdvice)this.fastPendingDeliveryAdvices.SelectedObject;
                            if (pendingDeliveryAdvice != null)
                            {
                                this.goodsIssueViewModel.DeliveryAdviceID = pendingDeliveryAdvice.DeliveryAdviceID;
                                this.goodsIssueViewModel.PrimaryReferences = pendingDeliveryAdvice.DeliveryAdviceReference;
                                this.goodsIssueViewModel.VoucherCodes = pendingDeliveryAdvice.VoucherCode;
                                this.goodsIssueViewModel.CustomerID = pendingDeliveryAdvice.CustomerID;
                                this.goodsIssueViewModel.CustomerName = pendingDeliveryAdvice.CustomerName;
                                this.goodsIssueViewModel.ReceiverID = pendingDeliveryAdvice.ReceiverID;
                                this.goodsIssueViewModel.ReceiverName = pendingDeliveryAdvice.ReceiverName;
                                this.goodsIssueViewModel.ForkliftDriverID = pendingDeliveryAdvice.ForkliftDriverID;
                                this.goodsIssueViewModel.StorekeeperID = pendingDeliveryAdvice.StorekeeperID;
                                this.goodsIssueViewModel.Vehicle = pendingDeliveryAdvice.Vehicle;
                                this.goodsIssueViewModel.VehicleDriver = pendingDeliveryAdvice.VehicleDriver;
                                nextOK = true;
                            }
                        }
                        if (this.customTabBatch.SelectedIndex == 1)
                        {
                            PendingDeliveryAdviceCustomer pendingDeliveryAdviceCustomer = (PendingDeliveryAdviceCustomer)this.fastPendingDeliveryAdviceCustomers.SelectedObject;
                            if (pendingDeliveryAdviceCustomer != null)
                            {
                                this.goodsIssueViewModel.CustomerID = pendingDeliveryAdviceCustomer.CustomerID;
                                this.goodsIssueViewModel.CustomerName = pendingDeliveryAdviceCustomer.CustomerName;
                                this.goodsIssueViewModel.ReceiverID = pendingDeliveryAdviceCustomer.ReceiverID;
                                this.goodsIssueViewModel.ReceiverName = pendingDeliveryAdviceCustomer.ReceiverName;
                                nextOK = true;
                            }
                        }
                    }

                    
                    if (this.customTabBatch.SelectedIndex == 2 || this.customTabBatch.SelectedIndex == 3)
                    {
                        this.goodsIssueViewModel.GoodsIssueTypeID = (int)GlobalEnums.GoodsIssueTypeID.TransferOrder;
                        if (this.customTabBatch.SelectedIndex == 2)
                        {
                            PendingTransferOrder pendingTransferOrder = (PendingTransferOrder)this.fastPendingTransferOrders.SelectedObject;
                            if (pendingTransferOrder != null)
                            {
                                this.goodsIssueViewModel.TransferOrderID = pendingTransferOrder.TransferOrderID;
                                this.goodsIssueViewModel.TransferOrderReference = pendingTransferOrder.TransferOrderReference;
                                this.goodsIssueViewModel.VoucherCodes = pendingTransferOrder.VoucherCode;
                                this.goodsIssueViewModel.WarehouseID = pendingTransferOrder.WarehouseID;
                                this.goodsIssueViewModel.WarehouseName = pendingTransferOrder.WarehouseName;
                                this.goodsIssueViewModel.WarehouseReceiptID = pendingTransferOrder.WarehouseReceiptID;
                                this.goodsIssueViewModel.WarehouseReceiptName = pendingTransferOrder.WarehouseReceiptName;
                                this.goodsIssueViewModel.ForkliftDriverID = pendingTransferOrder.ForkliftDriverID;
                                this.goodsIssueViewModel.StorekeeperID = pendingTransferOrder.StorekeeperID;
                                this.goodsIssueViewModel.Vehicle = pendingTransferOrder.Vehicle;
                                this.goodsIssueViewModel.VehicleDriver = pendingTransferOrder.VehicleDriver;
                                nextOK = true;
                            }
                        }
                        if (this.customTabBatch.SelectedIndex == 3)
                        {
                            PendingTransferOrderWarehouse pendingTransferOrderWarehouse = (PendingTransferOrderWarehouse)this.fastPendingTransferOrderWarehouses.SelectedObject;
                            if (pendingTransferOrderWarehouse != null)
                            {
                                this.goodsIssueViewModel.WarehouseID = pendingTransferOrderWarehouse.WarehouseID;
                                this.goodsIssueViewModel.WarehouseName = pendingTransferOrderWarehouse.WarehouseName;
                                this.goodsIssueViewModel.WarehouseReceiptID = pendingTransferOrderWarehouse.WarehouseReceiptID;
                                this.goodsIssueViewModel.WarehouseReceiptName = pendingTransferOrderWarehouse.WarehouseReceiptName;
                                nextOK = true;
                            }
                        }
                    }


                    if (nextOK)
                        this.DialogResult = DialogResult.OK;
                    else
                        CustomMsgBox.Show(this, "Vui lòng chọn phiếu giao thành phẩm sau đóng gói, hoặc kho nhận hàng.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
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
