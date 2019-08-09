using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using BrightIdeasSoftware;

using TotalBase.Enums;
using TotalModel.Models;
using TotalDTO.Inventories;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Inventories;
using TotalSmartCoding.Views.Mains;
using TotalDTO.Helpers.Interfaces;


namespace TotalSmartCoding.Views.Inventories.GoodsReceipts
{
    public partial class WizardDetail : Form
    {
        private GoodsReceiptAPIs goodsReceiptAPIs;
        private GoodsReceiptViewModel goodsReceiptViewModel;
        private CustomTabControl customTabBatch;
        public WizardDetail(GoodsReceiptAPIs goodsReceiptAPIs, GoodsReceiptViewModel goodsReceiptViewModel)
        {
            InitializeComponent();

            this.customTabBatch = new CustomTabControl();

            this.customTabBatch.Font = this.fastPendingPallets.Font;
            this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
            this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

            this.customTabBatch.TabPages.Add("tabPendingPallets", "Pending pallets");
            this.customTabBatch.TabPages.Add("tabPendingCartons", "Pending cartons");
            this.customTabBatch.TabPages[0].Controls.Add(this.fastPendingPallets);
            this.customTabBatch.TabPages[1].Controls.Add(this.fastPendingCartons);


            this.customTabBatch.Dock = DockStyle.Fill;
            this.fastPendingPallets.Dock = DockStyle.Fill;
            this.fastPendingCartons.Dock = DockStyle.Fill;
            this.panelMaster.Controls.Add(this.customTabBatch);


            this.goodsReceiptAPIs = goodsReceiptAPIs;
            this.goodsReceiptViewModel = goodsReceiptViewModel;

            this.olvCartonSelected.HeaderCheckState = this.goodsReceiptViewModel.GoodsReceiptTypeID == (int)GlobalEnums.GoodsReceiptTypeID.Pickup ? CheckState.Checked : CheckState.Unchecked;
            this.olvPalletSelected.HeaderCheckState = this.goodsReceiptViewModel.GoodsReceiptTypeID == (int)GlobalEnums.GoodsReceiptTypeID.Pickup ? CheckState.Checked : CheckState.Unchecked;
            this.menuOptionBinLocations.Visible = this.goodsReceiptViewModel.GoodsReceiptTypeID == (int)GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer;
        }


        private void WizardDetail_Load(object sender, EventArgs e)
        {
            try
            {
                List<PendingPickupDetail> pendingPickupDetails = null;
                List<PendingGoodsIssueTransferDetail> pendingGoodsIssueTransferDetails = null;

                if (this.goodsReceiptViewModel.GoodsReceiptTypeID == (int)GlobalEnums.GoodsReceiptTypeID.Pickup)
                    pendingPickupDetails = this.goodsReceiptAPIs.GetPendingPickupDetails(this.goodsReceiptViewModel.LocationID, this.goodsReceiptViewModel.GoodsReceiptID, this.goodsReceiptViewModel.PickupID, this.goodsReceiptViewModel.WarehouseID, string.Join(",", this.goodsReceiptViewModel.ViewDetails.Select(d => d.PickupDetailID)), false);
                if (this.goodsReceiptViewModel.GoodsReceiptTypeID == (int)GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer)
                    pendingGoodsIssueTransferDetails = this.goodsReceiptAPIs.GetPendingGoodsIssueTransferDetails(this.goodsReceiptViewModel.LocationID, this.goodsReceiptViewModel.GoodsReceiptID, this.goodsReceiptViewModel.GoodsIssueID, this.goodsReceiptViewModel.WarehouseID, string.Join(",", this.goodsReceiptViewModel.ViewDetails.Select(d => d.GoodsIssueTransferDetailID)), false);

                if (pendingPickupDetails != null)
                {
                    this.fastPendingPallets.SetObjects(pendingPickupDetails.Where(w => w.PalletID != null));
                    this.fastPendingCartons.SetObjects(pendingPickupDetails.Where(w => w.CartonID != null));
                }

                if (pendingGoodsIssueTransferDetails != null)
                {
                    this.fastPendingPallets.SetObjects(pendingGoodsIssueTransferDetails.Where(w => w.PalletID != null));
                    this.fastPendingCartons.SetObjects(pendingGoodsIssueTransferDetails.Where(w => w.CartonID != null));
                }

                this.ShowRowCount(true, true);
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
                    FastObjectListView fastPendingList = this.customTabBatch.SelectedIndex == 0 ? this.fastPendingPallets : (this.customTabBatch.SelectedIndex == 1 ? this.fastPendingCartons : null);

                    if (fastPendingList != null)
                    {
                        if (fastPendingList.CheckedObjects.Count > 0)
                        {
                            IEnumerable<IPendingforGoodsReceiptDetail> pendingforGoodsReceiptDetails = fastPendingList.CheckedObjects.Cast<IPendingforGoodsReceiptDetail>();
                            if (pendingforGoodsReceiptDetails.Where(w => w.BinLocationID <= 0).FirstOrDefault() != null) throw new Exception("Vui lòng chọn Bin Location.");

                            this.goodsReceiptViewModel.ViewDetails.RaiseListChangedEvents = false;
                            foreach (IPendingforGoodsReceiptDetail pendingforGoodsReceiptDetail in pendingforGoodsReceiptDetails)
                            {
                                GoodsReceiptDetailDTO goodsReceiptDetailDTO = new GoodsReceiptDetailDTO()
                                {
                                    GoodsReceiptID = this.goodsReceiptViewModel.GoodsReceiptID,

                                    PickupID = pendingforGoodsReceiptDetail.PickupID > 0 ? pendingforGoodsReceiptDetail.PickupID : (int?)null,
                                    PickupDetailID = pendingforGoodsReceiptDetail.PickupDetailID > 0 ? pendingforGoodsReceiptDetail.PickupDetailID : (int?)null,
                                    PickupReference = pendingforGoodsReceiptDetail.PrimaryReference,
                                    PickupEntryDate = pendingforGoodsReceiptDetail.PickupID > 0 ? pendingforGoodsReceiptDetail.PrimaryEntryDate : (DateTime?)null,

                                    GoodsIssueID = pendingforGoodsReceiptDetail.GoodsIssueID > 0 ? pendingforGoodsReceiptDetail.GoodsIssueID : (int?)null,
                                    GoodsIssueTransferDetailID = pendingforGoodsReceiptDetail.GoodsIssueTransferDetailID > 0 ? pendingforGoodsReceiptDetail.GoodsIssueTransferDetailID : (int?)null,
                                    GoodsIssueReference = pendingforGoodsReceiptDetail.PrimaryReference,
                                    GoodsIssueEntryDate = pendingforGoodsReceiptDetail.GoodsIssueID > 0 ? pendingforGoodsReceiptDetail.PrimaryEntryDate : (DateTime?)null,

                                    BatchID = pendingforGoodsReceiptDetail.BatchID,
                                    BatchEntryDate = pendingforGoodsReceiptDetail.BatchEntryDate,

                                    BinLocationID = pendingforGoodsReceiptDetail.BinLocationID,
                                    BinLocationCode = pendingforGoodsReceiptDetail.BinLocationCode,

                                    CommodityID = pendingforGoodsReceiptDetail.CommodityID,
                                    CommodityCode = pendingforGoodsReceiptDetail.CommodityCode,
                                    CommodityName = pendingforGoodsReceiptDetail.CommodityName,

                                    Quantity = (decimal)pendingforGoodsReceiptDetail.QuantityRemains,
                                    LineVolume = (decimal)pendingforGoodsReceiptDetail.LineVolumeRemains,


                                    PackID = pendingforGoodsReceiptDetail.PackID,
                                    PackCode = pendingforGoodsReceiptDetail.PackCode,
                                    CartonID = pendingforGoodsReceiptDetail.CartonID,
                                    CartonCode = pendingforGoodsReceiptDetail.CartonCode,
                                    PalletID = pendingforGoodsReceiptDetail.PalletID,
                                    PalletCode = pendingforGoodsReceiptDetail.PalletCode,

                                    PackCounts = pendingforGoodsReceiptDetail.PackCounts,
                                    CartonCounts = pendingforGoodsReceiptDetail.CartonCounts,
                                    PalletCounts = pendingforGoodsReceiptDetail.PalletCounts
                                };
                                this.goodsReceiptViewModel.ViewDetails.Add(goodsReceiptDetailDTO);
                            }
                            this.goodsReceiptViewModel.ViewDetails.RaiseListChangedEvents = true;
                            this.goodsReceiptViewModel.ViewDetails.ResetBindings();
                        }
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

        private void textexFilters_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OLVHelpers.ApplyFilters(this.fastPendingPallets, this.textexFilters.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                OLVHelpers.ApplyFilters(this.fastPendingCartons, this.textexFilters.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

                this.ShowRowCount(true, true);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonClearFilters_Click(object sender, EventArgs e)
        {
            this.textexFilters.Text = "";
        }

        private void ShowRowCount(bool showPalletCount, bool showCartonCount)
        {
            if (showPalletCount)
            {
                decimal? totalQuantityRemains = this.fastPendingPallets.FilteredObjects.Cast<IPendingforGoodsReceiptDetail>().Select(o => o.QuantityRemains).Sum();
                decimal? totalLineVolumeRemains = this.fastPendingPallets.FilteredObjects.Cast<IPendingforGoodsReceiptDetail>().Select(o => o.LineVolumeRemains).Sum();
                this.customTabBatch.TabPages[0].Text = "Pending " + this.fastPendingPallets.GetItemCount().ToString("N0") + " pallet" + (this.fastPendingPallets.GetItemCount() > 1 ? "s" : "") + ", Quantity: " + (totalQuantityRemains != null ? ((decimal)totalQuantityRemains).ToString("N0") : "0") + ", Volume: " + (totalLineVolumeRemains != null ? ((decimal)totalLineVolumeRemains).ToString("N0") : "0") + "       ";
            }
            if (showCartonCount)
            {
                decimal? totalQuantityRemains = this.fastPendingCartons.FilteredObjects.Cast<IPendingforGoodsReceiptDetail>().Select(o => o.QuantityRemains).Sum();
                decimal? totalLineVolumeRemains = this.fastPendingCartons.FilteredObjects.Cast<IPendingforGoodsReceiptDetail>().Select(o => o.LineVolumeRemains).Sum();
                this.customTabBatch.TabPages[1].Text = "Pending " + this.fastPendingCartons.GetItemCount().ToString("N0") + " carton" + (this.fastPendingCartons.GetItemCount() > 1 ? "s" : "") + ", Quantity: " + (totalQuantityRemains != null ? ((decimal)totalQuantityRemains).ToString("N0") : "0") + ", Volume: " + (totalLineVolumeRemains != null ? ((decimal)totalLineVolumeRemains).ToString("N0") : "0") + "       ";
            }
        }

        private void menuOptionBinLocations_Click(object sender, EventArgs e)
        {
            try
            {
                FastObjectListView fastPendingList = this.customTabBatch.SelectedIndex == 0 ? this.fastPendingPallets : (this.customTabBatch.SelectedIndex == 1 ? this.fastPendingCartons : null);

                if (fastPendingList != null && fastPendingList.SelectedObject != null)
                {
                    IPendingforGoodsReceiptDetail pendingforGoodsReceiptDetail = (IPendingforGoodsReceiptDetail)fastPendingList.SelectedObject;
                    if (pendingforGoodsReceiptDetail != null)
                    {
                        LineDetailBinlLocation lineDetailBinlLocation = new LineDetailBinlLocation()
                        {
                            CommodityID = pendingforGoodsReceiptDetail.CommodityID,
                            CommodityCode = pendingforGoodsReceiptDetail.CommodityCode,
                            CommodityName = pendingforGoodsReceiptDetail.CommodityName,
                            PackID = pendingforGoodsReceiptDetail.PackID,
                            PackCode = pendingforGoodsReceiptDetail.PackCode,
                            CartonID = pendingforGoodsReceiptDetail.CartonID,
                            CartonCode = pendingforGoodsReceiptDetail.CartonCode,
                            PalletID = pendingforGoodsReceiptDetail.PalletID,
                            PalletCode = pendingforGoodsReceiptDetail.PalletCode,
                            WarehouseID = this.goodsReceiptViewModel.WarehouseID,
                            BinLocationID = pendingforGoodsReceiptDetail.BinLocationID,
                            BinLocationCode = pendingforGoodsReceiptDetail.BinLocationCode,
                            Quantity = (decimal)pendingforGoodsReceiptDetail.Quantity,
                            LineVolume = pendingforGoodsReceiptDetail.LineVolume
                        };

                        Pickups.WizardDetail wizardDetail = new Pickups.WizardDetail(lineDetailBinlLocation);
                        TabletMDI tabletMDI = new TabletMDI(wizardDetail);

                        if (tabletMDI.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            //if (fastPendingList.Equals(this.fastPendingCartons)) NEU AAP DUNG CACH NAY: CHI CARTON MOI CHO PHEP SET BIN LOCATION HANG LOAT
                            //{
                            foreach (var checkedObject in fastPendingList.CheckedObjects)
                            {
                                IPendingforGoodsReceiptDetail p = (IPendingforGoodsReceiptDetail)checkedObject;
                                p.BinLocationID = (int)lineDetailBinlLocation.BinLocationID;
                                p.BinLocationCode = lineDetailBinlLocation.BinLocationCode;
                            }
                            //}
                            //else
                            //{
                            //    pendingforGoodsReceiptDetail.BinLocationID = (int)lineDetailBinlLocation.BinLocationID;
                            //    pendingforGoodsReceiptDetail.BinLocationCode = lineDetailBinlLocation.BinLocationCode;
                            //}

                            fastPendingList.RefreshObject(pendingforGoodsReceiptDetail);
                        }

                        wizardDetail.Dispose(); tabletMDI.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void fastPendingBarcodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.menuOptionBinLocations_Click(sender, new EventArgs());
        }
    }
}
