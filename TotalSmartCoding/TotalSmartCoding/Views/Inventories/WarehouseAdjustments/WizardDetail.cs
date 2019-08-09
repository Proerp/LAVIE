using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using BrightIdeasSoftware;

using Ninject;

using TotalBase;
using TotalModel.Models;
using TotalCore.Repositories.Productions;
using TotalCore.Repositories.Inventories;
using TotalDTO.Inventories;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Inventories;
using TotalDTO.Helpers.Interfaces;
using TotalSmartCoding.Views.Mains;
using TotalBase.Enums;


namespace TotalSmartCoding.Views.Inventories.WarehouseAdjustments
{
    public partial class WizardDetail : Form
    {
        private WarehouseAdjustmentViewModel warehouseAdjustmentViewModel;
        private CustomTabControl customTabBatch;

        List<GoodsReceiptDetailAvailable> goodsReceiptDetailAvailables;

        public WizardDetail(WarehouseAdjustmentViewModel warehouseAdjustmentViewModel)
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


            this.warehouseAdjustmentViewModel = warehouseAdjustmentViewModel;

            if (this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID != (int)GlobalEnums.WarehouseAdjustmentTypeID.ChangeBinLocation)
            {
                this.olvCartonNewBinLocationCode.Width = 0;
                this.olvPalletNewBinLocationCode.Width = 0;
            }
        }


        private void WizardDetail_Load(object sender, EventArgs e)
        {
            try
            {
                GoodsReceiptAPIs goodsReceiptAPIs = new GoodsReceiptAPIs(CommonNinject.Kernel.Get<IGoodsReceiptAPIRepository>());
                this.goodsReceiptDetailAvailables = goodsReceiptAPIs.GetGoodsReceiptDetailAvailables(this.warehouseAdjustmentViewModel.LocationID, this.warehouseAdjustmentViewModel.WarehouseID, null, null, null, string.Join(",", this.warehouseAdjustmentViewModel.ViewDetails.Select(d => d.GoodsReceiptDetailID)), false);

                this.fastPendingPallets.SetObjects(goodsReceiptDetailAvailables.Where(w => w.PalletID != null));
                this.fastPendingCartons.SetObjects(goodsReceiptDetailAvailables.Where(w => w.CartonID != null));

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
                    List<GoodsReceiptDetailAvailable> selectedGoodsReceiptDetailAvailables = this.goodsReceiptDetailAvailables.Where(w => w.IsSelected).ToList();
                    if (selectedGoodsReceiptDetailAvailables.Count() > 0)
                    {
                        if (selectedGoodsReceiptDetailAvailables.Where(w => w.BinLocationID <= 0).FirstOrDefault() != null) throw new Exception("Vui lòng chọn Bin Location.");
                        if (this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.ChangeBinLocation && selectedGoodsReceiptDetailAvailables.Where(w => w.NewBinLocationID == null || w.NewBinLocationID <= 0).FirstOrDefault() != null) throw new Exception("Vui lòng chọn New Bin Location.");

                        this.warehouseAdjustmentViewModel.ViewDetails.RaiseListChangedEvents = false;
                        foreach (GoodsReceiptDetailAvailable goodsReceiptDetailAvailable in selectedGoodsReceiptDetailAvailables)
                        {
                            WarehouseAdjustmentDetailDTO issueWarehouseAdjustmentDetailDTO = this.newWarehouseAdjustmentDetailDTO(goodsReceiptDetailAvailable.CommodityID, goodsReceiptDetailAvailable.CommodityCode, goodsReceiptDetailAvailable.CommodityName, goodsReceiptDetailAvailable.PackageSize, goodsReceiptDetailAvailable.Volume, goodsReceiptDetailAvailable.PackageVolume, goodsReceiptDetailAvailable.GoodsReceiptID, goodsReceiptDetailAvailable.GoodsReceiptDetailID, goodsReceiptDetailAvailable.GoodsReceiptReference, goodsReceiptDetailAvailable.GoodsReceiptEntryDate, goodsReceiptDetailAvailable.BatchID, goodsReceiptDetailAvailable.BatchEntryDate, goodsReceiptDetailAvailable.BinLocationID, goodsReceiptDetailAvailable.BinLocationCode, goodsReceiptDetailAvailable.WarehouseID, goodsReceiptDetailAvailable.WarehouseCode, goodsReceiptDetailAvailable.PackID, goodsReceiptDetailAvailable.PackCode, goodsReceiptDetailAvailable.CartonID, goodsReceiptDetailAvailable.CartonCode, goodsReceiptDetailAvailable.PalletID, goodsReceiptDetailAvailable.PalletCode, goodsReceiptDetailAvailable.PackCounts, goodsReceiptDetailAvailable.CartonCounts, goodsReceiptDetailAvailable.PalletCounts, (decimal)goodsReceiptDetailAvailable.QuantityAvailable, (decimal)goodsReceiptDetailAvailable.LineVolumeAvailable, -(decimal)goodsReceiptDetailAvailable.QuantityAvailable, -(decimal)goodsReceiptDetailAvailable.LineVolumeAvailable);
                            this.warehouseAdjustmentViewModel.ViewDetails.Add(issueWarehouseAdjustmentDetailDTO);

                            if (this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID != (int)GlobalEnums.WarehouseAdjustmentTypeID.ReturnToProduction && this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID != (int)GlobalEnums.WarehouseAdjustmentTypeID.OtherIssues)
                            {
                                WarehouseAdjustmentDetailDTO receiptWarehouseAdjustmentDetailDTO = this.newWarehouseAdjustmentDetailDTO(goodsReceiptDetailAvailable.CommodityID, goodsReceiptDetailAvailable.CommodityCode, goodsReceiptDetailAvailable.CommodityName, goodsReceiptDetailAvailable.PackageSize, goodsReceiptDetailAvailable.Volume, goodsReceiptDetailAvailable.PackageVolume, goodsReceiptDetailAvailable.GoodsReceiptID, goodsReceiptDetailAvailable.GoodsReceiptDetailID, goodsReceiptDetailAvailable.GoodsReceiptReference, goodsReceiptDetailAvailable.GoodsReceiptEntryDate, goodsReceiptDetailAvailable.BatchID, goodsReceiptDetailAvailable.BatchEntryDate, this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.ChangeBinLocation ? (int)goodsReceiptDetailAvailable.NewBinLocationID : goodsReceiptDetailAvailable.BinLocationID, this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.ChangeBinLocation ? goodsReceiptDetailAvailable.NewBinLocationCode : goodsReceiptDetailAvailable.BinLocationCode, goodsReceiptDetailAvailable.WarehouseID, goodsReceiptDetailAvailable.WarehouseCode, goodsReceiptDetailAvailable.PackID, goodsReceiptDetailAvailable.PackCode, goodsReceiptDetailAvailable.CartonID, goodsReceiptDetailAvailable.CartonCode, goodsReceiptDetailAvailable.PalletID, goodsReceiptDetailAvailable.PalletCode, goodsReceiptDetailAvailable.PackCounts, goodsReceiptDetailAvailable.CartonCounts, goodsReceiptDetailAvailable.PalletCounts, (decimal)goodsReceiptDetailAvailable.QuantityAvailable, (decimal)goodsReceiptDetailAvailable.LineVolumeAvailable, (decimal)goodsReceiptDetailAvailable.QuantityAvailable, (decimal)goodsReceiptDetailAvailable.LineVolumeAvailable);
                                this.warehouseAdjustmentViewModel.ViewDetails.Add(receiptWarehouseAdjustmentDetailDTO); //issueWarehouseAdjustmentDetailDTO VS receiptWarehouseAdjustmentDetailDTO: INITIALLIIZE BY THE SAME DATA, EXCEPT: + OR - LineVolumeAvailable/ + OR - QuantityAvailable
                            }
                        }
                        this.warehouseAdjustmentViewModel.ViewDetails.RaiseListChangedEvents = true;
                        this.warehouseAdjustmentViewModel.ViewDetails.ResetBindings();
                    }
                }

                if (sender.Equals(this.buttonAdd))
                    this.WizardDetail_Load(this, new EventArgs());
                else
                    this.DialogResult = sender.Equals(this.buttonAddExit) ? DialogResult.OK : DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private WarehouseAdjustmentDetailDTO newWarehouseAdjustmentDetailDTO(int commodityID, string commodityCode, string commodityName, string packageSize, decimal volume, decimal packageVolume, int? goodsReceiptID, int? goodsReceiptDetailID, string goodsReceiptReference, DateTime? goodsReceiptEntryDate, int batchID, DateTime batchEntryDate, int binLocationID, string binLocationCode, int warehouseID, string warehouseCode, int? packID, string packCode, int? cartonID, string cartonCode, int? palletID, string palletCode, int packCounts, int cartonCounts, int palletCounts, decimal quantityAvailable, decimal lineVolumeAvailable, decimal quantity, decimal lineVolume)
        {
            WarehouseAdjustmentDetailDTO warehouseAdjustmentDetailDTO = new WarehouseAdjustmentDetailDTO()
            {
                WarehouseAdjustmentID = this.warehouseAdjustmentViewModel.WarehouseAdjustmentID,

                CommodityID = commodityID,
                CommodityCode = commodityCode,
                CommodityName = commodityName,

                PackageSize = packageSize,

                Volume = volume,
                PackageVolume = packageVolume,

                GoodsReceiptID = goodsReceiptID,
                GoodsReceiptDetailID = goodsReceiptDetailID,

                GoodsReceiptReference = goodsReceiptReference,
                GoodsReceiptEntryDate = goodsReceiptEntryDate,

                BatchID = batchID,
                BatchEntryDate = batchEntryDate,

                BinLocationID = binLocationID,
                BinLocationCode = binLocationCode,

                WarehouseID = warehouseID,
                WarehouseCode = warehouseCode,

                PackID = packID,
                PackCode = packCode,
                CartonID = cartonID,
                CartonCode = cartonCode,
                PalletID = palletID,
                PalletCode = palletCode,

                PackCounts = packCounts,
                CartonCounts = cartonCounts,
                PalletCounts = palletCounts,

                QuantityAvailable = quantityAvailable,
                LineVolumeAvailable = lineVolumeAvailable,

                Quantity = quantity,
                LineVolume = lineVolume
            };
            return warehouseAdjustmentDetailDTO;
        }

        private void textexFilters_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.fastPendingPallets.CheckedObjects = null;
                OLVHelpers.ApplyFilters(this.fastPendingPallets, this.textexFilters.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

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
            if (showPalletCount) this.customTabBatch.TabPages[0].Text = "Available " + this.fastPendingPallets.GetItemCount().ToString("N0") + " pallet" + (this.fastPendingPallets.GetItemCount() > 1 ? "s      " : "      ");
            if (showCartonCount) this.customTabBatch.TabPages[1].Text = "Carton: " + this.fastPendingCartons.GetItemCount().ToString("N0") + " item" + (this.fastPendingCartons.GetItemCount() > 1 ? "s      " : "      ");
        }

        private void fastPendingList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                FastObjectListView fastPendingList = this.customTabBatch.SelectedIndex == 0 ? this.fastPendingPallets : (this.customTabBatch.SelectedIndex == 1 ? this.fastPendingCartons : null);

                if (this.warehouseAdjustmentViewModel.WarehouseAdjustmentTypeID == (int)GlobalEnums.WarehouseAdjustmentTypeID.ChangeBinLocation && fastPendingList.SelectedObject != null)
                {
                    if (fastPendingList != null && fastPendingList.SelectedObject != null)
                    {
                        GoodsReceiptDetailAvailable goodsReceiptDetailAvailable = (GoodsReceiptDetailAvailable)fastPendingList.SelectedObject;
                        if (goodsReceiptDetailAvailable != null)
                        {
                            LineDetailBinlLocation lineDetailBinlLocation = new LineDetailBinlLocation()
                            {
                                CommodityID = goodsReceiptDetailAvailable.CommodityID,
                                CommodityCode = goodsReceiptDetailAvailable.CommodityCode,
                                CommodityName = goodsReceiptDetailAvailable.CommodityName,
                                PackID = goodsReceiptDetailAvailable.PackID,
                                PackCode = goodsReceiptDetailAvailable.PalletCode,
                                CartonID = goodsReceiptDetailAvailable.CartonID,
                                CartonCode = goodsReceiptDetailAvailable.CartonCode,
                                PalletID = goodsReceiptDetailAvailable.PalletID,
                                PalletCode = goodsReceiptDetailAvailable.PalletCode,
                                WarehouseID = (int)this.warehouseAdjustmentViewModel.WarehouseReceiptID,
                                BinLocationID = goodsReceiptDetailAvailable.NewBinLocationID,
                                BinLocationCode = goodsReceiptDetailAvailable.NewBinLocationCode,
                                Quantity = (decimal)goodsReceiptDetailAvailable.QuantityAvailable,
                                LineVolume = (decimal)goodsReceiptDetailAvailable.LineVolumeAvailable
                            };

                            Pickups.WizardDetail wizardDetail = new Pickups.WizardDetail(lineDetailBinlLocation);
                            TabletMDI tabletMDI = new TabletMDI(wizardDetail);

                            if (tabletMDI.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                foreach (var checkedObject in fastPendingList.CheckedObjects)
                                {
                                    GoodsReceiptDetailAvailable p = (GoodsReceiptDetailAvailable)checkedObject;
                                    p.NewBinLocationID = (int)lineDetailBinlLocation.BinLocationID;
                                    p.NewBinLocationCode = lineDetailBinlLocation.BinLocationCode;
                                }
                                fastPendingList.RefreshObject(goodsReceiptDetailAvailable);
                            }

                            wizardDetail.Dispose(); tabletMDI.Dispose();
                        }
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
