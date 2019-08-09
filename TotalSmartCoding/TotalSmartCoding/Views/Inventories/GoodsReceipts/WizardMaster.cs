using System;
using System.Drawing;
using System.Windows.Forms;
using TotalModel.Models;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Inventories;

namespace TotalSmartCoding.Views.Inventories.GoodsReceipts
{
    public partial class WizardMaster : Form
    {
        private GoodsReceiptAPIs goodsReceiptAPIs;
        private GoodsReceiptViewModel goodsReceiptViewModel;
        private CustomTabControl customTabMain;
        public WizardMaster(GoodsReceiptAPIs goodsReceiptAPIs, GoodsReceiptViewModel goodsReceiptViewModel)
        {
            InitializeComponent();

            this.customTabMain = new CustomTabControl();

            this.customTabMain.Font = this.fastPendingPickups.Font;
            this.customTabMain.DisplayStyle = TabStyle.VisualStudio;
            this.customTabMain.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

            this.customTabMain.TabPages.Add("tabPendingPickups", "Receipt by every pickup ");
            this.customTabMain.TabPages.Add("tabPendingPickupWarehouses", "Cumulate pickups per warehouse ");
            this.customTabMain.TabPages.Add("tabPendingGoodsIssueTransfers", "Receipt by every transfer ");
            this.customTabMain.TabPages.Add("tabPendingGoodsIssueTransferWarehouses", "Cumulate transfers per warehouse ");
            //this.customTabMain.TabPages.Add("tabPendingPurchases", "Purchase Invoice     ");
            //this.customTabMain.TabPages.Add("tabPendingPurchases", "Sales Return     ");

            this.customTabMain.TabPages[0].Controls.Add(this.fastPendingPickups);
            this.customTabMain.TabPages[1].Controls.Add(this.fastPendingPickupWarehouses);
            this.customTabMain.TabPages[2].Controls.Add(this.fastPendingGoodsIssueTransfers);
            this.customTabMain.TabPages[3].Controls.Add(this.fastPendingGoodsIssueTransferWarehouses);

            this.customTabMain.Dock = DockStyle.Fill;
            this.fastPendingPickups.Dock = DockStyle.Fill;
            this.fastPendingPickupWarehouses.Dock = DockStyle.Fill;
            this.fastPendingGoodsIssueTransfers.Dock = DockStyle.Fill;
            this.fastPendingGoodsIssueTransferWarehouses.Dock = DockStyle.Fill;
            this.panelMaster.Controls.Add(this.customTabMain);


            this.goodsReceiptAPIs = goodsReceiptAPIs;
            this.goodsReceiptViewModel = goodsReceiptViewModel;
        }


        private void Wizard_Load(object sender, EventArgs e)
        {
            try
            {
                this.fastPendingPickups.SetObjects(this.goodsReceiptAPIs.GetPendingPickups(this.goodsReceiptViewModel.LocationID));
                this.fastPendingPickupWarehouses.SetObjects(this.goodsReceiptAPIs.GetPendingPickupWarehouses(this.goodsReceiptViewModel.LocationID));
                this.fastPendingGoodsIssueTransfers.SetObjects(this.goodsReceiptAPIs.GetPendingGoodsIssueTransfers(this.goodsReceiptViewModel.LocationID));
                this.fastPendingGoodsIssueTransferWarehouses.SetObjects(this.goodsReceiptAPIs.GetPendingGoodsIssueTransferWarehouses(this.goodsReceiptViewModel.LocationID));
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
                    this.goodsReceiptViewModel.HasPickup = true;

                    bool nextOK = false;

                    Object selectedObject = this.customTabMain.SelectedIndex == 0 ? this.fastPendingPickups.SelectedObject : (this.customTabMain.SelectedIndex == 1 ? this.fastPendingPickupWarehouses.SelectedObject : (this.customTabMain.SelectedIndex == 2 ? this.fastPendingGoodsIssueTransfers.SelectedObject : this.customTabMain.SelectedIndex == 3 ? this.fastPendingGoodsIssueTransferWarehouses.SelectedObject : null));
                    if (selectedObject != null)
                    {
                        IPendingforGoodsReceipt pendingforGoodsReceipt = (IPendingforGoodsReceipt)selectedObject;
                        if (pendingforGoodsReceipt != null)
                        {
                            this.goodsReceiptViewModel.PickupID = pendingforGoodsReceipt.PickupID > 0 ? pendingforGoodsReceipt.PickupID : (int?)null;
                            this.goodsReceiptViewModel.GoodsIssueID = pendingforGoodsReceipt.GoodsIssueID > 0 ? pendingforGoodsReceipt.GoodsIssueID : (int?)null;
                            this.goodsReceiptViewModel.PickupReference = pendingforGoodsReceipt.PrimaryReference;
                            this.goodsReceiptViewModel.GoodsIssueReference = pendingforGoodsReceipt.PrimaryReference;

                            this.goodsReceiptViewModel.GoodsReceiptTypeID = pendingforGoodsReceipt.GoodsReceiptTypeID;
                            this.goodsReceiptViewModel.GoodsReceiptTypeName = pendingforGoodsReceipt.GoodsReceiptTypeName;
                            this.goodsReceiptViewModel.WarehouseID = pendingforGoodsReceipt.WarehouseID;
                            this.goodsReceiptViewModel.WarehouseName = pendingforGoodsReceipt.WarehouseName;

                            nextOK = true;
                        }
                    }

                    if (nextOK)
                        this.DialogResult = DialogResult.OK;
                    else
                        CustomMsgBox.Show(this, "Vui lòng chọn phiếu giao thành phẩm sau đóng gói, hoặc kho nhận.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
