using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using BrightIdeasSoftware;

using TotalModel.Models;
using TotalDTO.Sales;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Sales;


namespace TotalSmartCoding.Views.Sales.DeliveryAdvices
{
    public partial class WizardDetail : Form
    {
        private DeliveryAdviceAPIs deliveryAdviceAPIs;
        private DeliveryAdviceViewModel deliveryAdviceViewModel;
        private CustomTabControl customTabBatch;

        public WizardDetail(DeliveryAdviceAPIs deliveryAdviceAPIs, DeliveryAdviceViewModel deliveryAdviceViewModel)
        {
            InitializeComponent();

            this.customTabBatch = new CustomTabControl();

            this.customTabBatch.Font = this.fastPendingSalesOrderDetails.Font;
            this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
            this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

            this.customTabBatch.TabPages.Add("tabPendingSalesOrderDetails", "Pending items");
            this.customTabBatch.TabPages[0].Controls.Add(this.fastPendingSalesOrderDetails);


            this.customTabBatch.Dock = DockStyle.Fill;
            this.fastPendingSalesOrderDetails.Dock = DockStyle.Fill;
            this.panelMaster.Controls.Add(this.customTabBatch);


            this.deliveryAdviceAPIs = deliveryAdviceAPIs;
            this.deliveryAdviceViewModel = deliveryAdviceViewModel;
        }


        private void WizardDetail_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.deliveryAdviceViewModel.SalesOrderID != null) { this.olvSalesOrderEntryDate.Width = 0; this.olvSalesOrderReference.Width = 0; }

                List<PendingSalesOrderDetail> pendingSalesOrderDetails = this.deliveryAdviceAPIs.GetPendingSalesOrderDetails(this.deliveryAdviceViewModel.LocationID, this.deliveryAdviceViewModel.DeliveryAdviceID, this.deliveryAdviceViewModel.SalesOrderID, this.deliveryAdviceViewModel.CustomerID, this.deliveryAdviceViewModel.ReceiverID, string.Join(",", this.deliveryAdviceViewModel.ViewDetails.Select(d => d.SalesOrderDetailID)), false);
                this.fastPendingSalesOrderDetails.SetObjects(pendingSalesOrderDetails);
                this.customTabBatch.TabPages[0].Text = "Pending " + this.fastPendingSalesOrderDetails.GetItemCount().ToString("N0") + " item" + (this.fastPendingSalesOrderDetails.GetItemCount() > 1 ? "s      " : "      ");
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonAddESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonAdd) || sender.Equals(this.buttonAddExit))
                {
                    foreach (var checkedObjects in this.fastPendingSalesOrderDetails.CheckedObjects)
                    {
                        PendingSalesOrderDetail pendingSalesOrderDetail = (PendingSalesOrderDetail)checkedObjects;
                        DeliveryAdviceDetailDTO deliveryAdviceDetailDTO = new DeliveryAdviceDetailDTO()
                        {
                            DeliveryAdviceID = this.deliveryAdviceViewModel.DeliveryAdviceID,

                            SalesOrderID = pendingSalesOrderDetail.SalesOrderID,
                            SalesOrderDetailID = pendingSalesOrderDetail.SalesOrderDetailID,
                            SalesOrderReference = pendingSalesOrderDetail.SalesOrderReference,
                            SalesOrderEntryDate = pendingSalesOrderDetail.SalesOrderEntryDate,

                            VoucherCode = pendingSalesOrderDetail.SalesOrderVoucherCode,

                            CommodityID = pendingSalesOrderDetail.CommodityID,
                            CommodityCode = pendingSalesOrderDetail.CommodityCode,
                            CommodityName = pendingSalesOrderDetail.CommodityName,

                            PackageSize = pendingSalesOrderDetail.PackageSize,

                            Volume = pendingSalesOrderDetail.Volume,
                            PackageVolume = pendingSalesOrderDetail.PackageVolume,

                            QuantityAvailable = pendingSalesOrderDetail.QuantityAvailable,
                            LineVolumeAvailable = pendingSalesOrderDetail.LineVolumeAvailable,

                            QuantityRemains = pendingSalesOrderDetail.QuantityRemains,
                            LineVolumeRemains = pendingSalesOrderDetail.LineVolumeRemains,

                            Quantity = pendingSalesOrderDetail.QuantityRemains,
                            LineVolume = pendingSalesOrderDetail.LineVolumeRemains
                        };
                        this.deliveryAdviceViewModel.ViewDetails.Add(deliveryAdviceDetailDTO);
                    }


                    if (sender.Equals(this.buttonAddExit))
                        this.DialogResult = DialogResult.OK;
                    else
                        this.WizardDetail_Load(this, new EventArgs());
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
