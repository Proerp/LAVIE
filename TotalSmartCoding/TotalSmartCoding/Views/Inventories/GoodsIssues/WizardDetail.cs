using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using BrightIdeasSoftware;

using Ninject;

using TotalModel.Models;
using TotalCore.Repositories.Inventories;
using TotalDTO.Inventories;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Inventories;
using TotalBase;
using System.Collections;
using TotalBase.Enums;


namespace TotalSmartCoding.Views.Inventories.GoodsIssues
{
    public partial class WizardDetail : Form, IToolstripMerge, IToolstripTablet
    {
        private bool UsingPack = false; //NOW AT CHEVRON: WE DON'T ALLOW ISSUE BY PACK 

        private CustomTabControl customTabBatch;
        public virtual ToolStrip toolstripChild { get; protected set; }

        private GoodsIssueViewModel goodsIssueViewModel;

        private IEnumerable<IPendingPrimaryDetail> pendingPrimaryDetails;
        private IPendingPrimaryDetail pendingPrimaryDetail;

        private string fileName;

        /// <summary>
        /// GoodsIssueViewModel goodsIssueViewModel: CURRENT DETAILS OF GOODSISSUE
        /// List<IPendingPrimaryDetail> pendingPrimaryDetails: COLLECTION OF PENDING ITEMS REQUESTED FOR DELIVERY (HERE WE USE IPendingPrimaryDetail FOR TWO CASE: PendingDeliveryAdviceDetail AND PendingTransferOrderDetail WHICH ARE IMPLEMENTED interface IPendingPrimaryDetail
        /// IPendingPrimaryDetail pendingPrimaryDetail: CURRENT SELECTED PENDING ITEM (CURRENT SELECTED OF PendingDeliveryAdviceDetail OR PendingTransferOrderDetail). THIS PARAMETER IS REQUIRED BY TABLET MODE: MEANS: WHEN USING BY THE FORFLIFT DRIVER TO MANUAL SELECT BY BARCODE OR BIN LOCATION
        /// string fileName: WHEN IMPORTED FORM TEXT FILE
        /// </summary>
        /// <param name="goodsIssueViewModel"></param>
        /// <param name="pendingPrimaryDetails"></param>
        /// <param name="pendingPrimaryDetail"></param>
        /// <param name="fileName"></param>
        public WizardDetail(GoodsIssueViewModel goodsIssueViewModel, List<IPendingPrimaryDetail> pendingPrimaryDetails, IPendingPrimaryDetail pendingPrimaryDetail, string fileName)
        {
            InitializeComponent();

            if (!this.UsingPack) { this.fastAvailablePacks.Dock = DockStyle.None; this.fastAvailablePacks.Visible = false; }

            this.toolstripChild = this.toolStripBottom;
            this.customTabBatch = new CustomTabControl();

            this.customTabBatch.Font = this.fastAvailablePallets.Font;
            this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
            this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

            this.customTabBatch.TabPages.Add("tabAvailablePallets", "Available pallets");
            this.customTabBatch.TabPages.Add("tabAvailableCartons", "Available cartons");
            if (this.UsingPack) this.customTabBatch.TabPages.Add("tabAvailablePacks", "Available packs");
            this.customTabBatch.TabPages[0].Controls.Add(this.fastAvailablePallets);
            this.customTabBatch.TabPages[1].Controls.Add(this.fastAvailableCartons);
            if (this.UsingPack) this.customTabBatch.TabPages[2].Controls.Add(this.fastAvailablePacks);


            this.customTabBatch.Dock = DockStyle.Fill;
            this.fastAvailablePallets.Dock = DockStyle.Fill;
            this.fastAvailableCartons.Dock = DockStyle.Fill;
            if (this.UsingPack) this.fastAvailablePacks.Dock = DockStyle.Fill;
            this.panelMaster.Controls.Add(this.customTabBatch);

            if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.GoodsIssue) ViewHelpers.SetFont(this, new Font("Calibri", 11), new Font("Calibri", 11), new Font("Calibri", 11));

            this.goodsIssueViewModel = goodsIssueViewModel;

            this.pendingPrimaryDetails = pendingPrimaryDetails;
            this.pendingPrimaryDetail = pendingPrimaryDetail;

            this.fileName = fileName;

            if (this.fileName != null)
            {
                this.toolStripBottom.Visible = true;
                this.fastMismatchedBarcodes.Visible = true;
                this.customTabBatch.TabPages.Add("tabMismatchedBarcodes", "Mismatched Barcodes");
                this.customTabBatch.TabPages[this.customTabBatch.TabPages.Count - 1].Controls.Add(this.fastMismatchedBarcodes);
                this.fastMismatchedBarcodes.Dock = DockStyle.Fill;
            }
        }

        private void WizardDetail_Load(object sender, EventArgs e)
        {
            try
            {
                string commodityIDs = "";
                Dictionary<int, int> filterBatchPerCommodity = new Dictionary<int, int>();  //Dictionary with pair: [CommodityID, BatchID]

                if (this.fileName != null)
                {
                    foreach (IPendingPrimaryDetail ipendingPrimaryDetail in this.pendingPrimaryDetails)
                    {
                        if (commodityIDs.IndexOf(ipendingPrimaryDetail.CommodityID.ToString()) < 0) commodityIDs = commodityIDs + (commodityIDs != "" ? "," : "") + ipendingPrimaryDetail.CommodityID.ToString();

                        if (ipendingPrimaryDetail.BatchID != null) { filterBatchPerCommodity[ipendingPrimaryDetail.CommodityID] = (int)ipendingPrimaryDetail.BatchID; }
                    }
                }

                GoodsReceiptAPIs goodsReceiptAPIs = new GoodsReceiptAPIs(CommonNinject.Kernel.Get<IGoodsReceiptAPIRepository>());

                List<GoodsReceiptDetailAvailable> goodsReceiptDetailAvailables = goodsReceiptAPIs.GetGoodsReceiptDetailAvailables(this.goodsIssueViewModel.LocationID, this.goodsIssueViewModel.WarehouseID, this.fileName == null ? this.pendingPrimaryDetail.CommodityID : (int?)null, this.fileName == null ? null : commodityIDs, this.fileName == null ? this.pendingPrimaryDetail.BatchID : (int?)null, string.Join(",", this.goodsIssueViewModel.ViewDetails.Select(d => d.GoodsReceiptDetailID)), true);
                IEnumerable<GoodsReceiptDetailAvailable> goodsReceiptPalletAvailables = null;
                IEnumerable<GoodsReceiptDetailAvailable> goodsReceiptCartonAvailables = null;
                IEnumerable<GoodsReceiptDetailAvailable> goodsReceiptPackAvailables = null;

                List<MismatchedBarcode> mismatchedBarcodes = new List<MismatchedBarcode>();

                if (this.fileName == null)
                {
                    goodsReceiptDetailAvailables.Each(goodsReceiptDetailAvailable =>
                    {
                        goodsReceiptDetailAvailable.DeliveryAdviceID = this.pendingPrimaryDetail.DeliveryAdviceID;
                        goodsReceiptDetailAvailable.DeliveryAdviceDetailID = this.pendingPrimaryDetail.DeliveryAdviceDetailID;

                        goodsReceiptDetailAvailable.TransferOrderID = this.pendingPrimaryDetail.TransferOrderID;
                        goodsReceiptDetailAvailable.TransferOrderDetailID = this.pendingPrimaryDetail.TransferOrderDetailID;

                        goodsReceiptDetailAvailable.PrimaryReference = this.pendingPrimaryDetail.PrimaryReference;
                        goodsReceiptDetailAvailable.PrimaryEntryDate = this.pendingPrimaryDetail.PrimaryEntryDate;

                        goodsReceiptDetailAvailable.QuantityRemains = (decimal)this.pendingPrimaryDetail.QuantityRemains;
                        goodsReceiptDetailAvailable.LineVolumeRemains = (decimal)this.pendingPrimaryDetail.LineVolumeRemains;
                    });

                    goodsReceiptPalletAvailables = goodsReceiptDetailAvailables.Where(w => w.PalletID != null);
                    goodsReceiptCartonAvailables = goodsReceiptDetailAvailables.Where(w => w.CartonID != null);
                    if (this.UsingPack) goodsReceiptPackAvailables = goodsReceiptDetailAvailables.Where(w => w.PackID != null);
                }
                else
                {
                    string[] barcodes = System.IO.File.ReadAllLines(fileName);
                    if (barcodes.Count() > 0)
                    {
                        if (filterBatchPerCommodity.Count > 0) //Remove row that does not match pair: [CommodityID, BatchID]
                            foreach (KeyValuePair<int, int> change in filterBatchPerCommodity)
                            { //LƯU Ý: CÂU LỆNH SAU ĐÂY SẼ REMOVE TẤT CẢ CommodityID NOT MATCH WITH BatchID => DO ĐÓ: NẾU 1 D.A/ T.O: VỪA CÓ CHỈ ĐỊNH BATCH, VỪA KHÔNG CHỈ ĐỊNH BATCH => THÌ PHẢI IMPORT BATCH TRƯỚC, SAU ĐÓ IMPORT NON-BATCH SAU (HOẶC ĐƠN GIẢN HƠN LÀ TÁCH THÀNH 2 GOODSISSUE)
                                goodsReceiptDetailAvailables.RemoveAll(w => w.CommodityID == change.Key && w.BatchID != change.Value);
                            }

                        foreach (string barcode in barcodes)
                        {
                            GoodsReceiptDetailAvailable goodsReceiptDetailAvailable = goodsReceiptDetailAvailables.Find(w => (w.PalletCode == barcode || w.CartonCode == barcode || w.PackCode == barcode));
                            if (goodsReceiptDetailAvailable != null)
                            {
                                if (goodsReceiptDetailAvailable.IsSelected == false)
                                {
                                    IPendingPrimaryDetail pendingPrimaryDetail = this.pendingPrimaryDetails.ToList().Find(w => w.CommodityID == goodsReceiptDetailAvailable.CommodityID && (w.BatchID == null || w.BatchID == goodsReceiptDetailAvailable.BatchID) && (w.QuantityRemains - w.QuantityIssue) >= goodsReceiptDetailAvailable.QuantityAvailable && (w.LineVolumeRemains - w.LineVolumeIssue) >= goodsReceiptDetailAvailable.LineVolumeAvailable);
                                    if (pendingPrimaryDetail != null)
                                    {
                                        //WHEN THIS FORM IS LOADED, pendingPrimaryDetail.QuantityIssue AND pendingPrimaryDetail.LineVolumeIssue IS ALWAYS = 0. THESE PROPERTIES (QuantityIssue, LineVolumeIssue) ARE USED TO CHECK (w.QuantityRemains - w.QuantityIssue) > goodsReceiptDetailAvailable.QuantityAvailable && (w.LineVolumeRemains - w.LineVolumeIssue) > goodsReceiptDetailAvailable.LineVolumeAvailable. THESE PROPERTIES ARE USED FOR THIS PURPOSE ONLY
                                        pendingPrimaryDetail.QuantityIssue = Math.Round(pendingPrimaryDetail.QuantityIssue + (decimal)goodsReceiptDetailAvailable.QuantityAvailable, GlobalEnums.rndQuantity, MidpointRounding.AwayFromZero);
                                        pendingPrimaryDetail.LineVolumeIssue = Math.Round(pendingPrimaryDetail.LineVolumeIssue + (decimal)goodsReceiptDetailAvailable.LineVolumeAvailable, GlobalEnums.rndVolume, MidpointRounding.AwayFromZero);

                                        goodsReceiptDetailAvailable.DeliveryAdviceID = pendingPrimaryDetail.DeliveryAdviceID;
                                        goodsReceiptDetailAvailable.DeliveryAdviceDetailID = pendingPrimaryDetail.DeliveryAdviceDetailID;

                                        goodsReceiptDetailAvailable.TransferOrderID = pendingPrimaryDetail.TransferOrderID;
                                        goodsReceiptDetailAvailable.TransferOrderDetailID = pendingPrimaryDetail.TransferOrderDetailID;

                                        goodsReceiptDetailAvailable.PrimaryReference = pendingPrimaryDetail.PrimaryReference;
                                        goodsReceiptDetailAvailable.PrimaryEntryDate = pendingPrimaryDetail.PrimaryEntryDate;

                                        goodsReceiptDetailAvailable.QuantityRemains = (decimal)pendingPrimaryDetail.QuantityRemains;
                                        goodsReceiptDetailAvailable.LineVolumeRemains = (decimal)pendingPrimaryDetail.LineVolumeRemains;

                                        goodsReceiptDetailAvailable.IsSelected = true;
                                    }
                                    else
                                    {
                                        mismatchedBarcodes.Add(new MismatchedBarcode() { Barcode = barcode, Description = "Số lượng xuất vượt quá yêu cầu. Vui lòng kiểm tra lại.", QuantityAvailable = goodsReceiptDetailAvailable.QuantityAvailable, LineVolumeAvailable = goodsReceiptDetailAvailable.LineVolumeAvailable });
                                    }
                                }
                                else
                                {
                                    mismatchedBarcodes.Add(new MismatchedBarcode() { Barcode = barcode, Description = "Trùng mã vạch. Bạn đã quét mã vạch này nhiều lần?", QuantityAvailable = goodsReceiptDetailAvailable.QuantityAvailable, LineVolumeAvailable = goodsReceiptDetailAvailable.LineVolumeAvailable });
                                }
                            }
                            else
                            {
                                mismatchedBarcodes.Add(new MismatchedBarcode() { Barcode = barcode, Description = "Không tìm thấy. Bạn đã xả pallet ra chưa?" });
                            }
                        }
                    }

                    goodsReceiptPalletAvailables = goodsReceiptDetailAvailables.Where(w => w.PalletID != null && w.IsSelected == true);
                    goodsReceiptCartonAvailables = goodsReceiptDetailAvailables.Where(w => w.CartonID != null && w.IsSelected == true);
                    if (this.UsingPack) goodsReceiptPackAvailables = goodsReceiptDetailAvailables.Where(w => w.PackID != null && w.IsSelected == true);
                }

                this.fastAvailablePallets.SetObjects(goodsReceiptPalletAvailables);
                this.fastAvailableCartons.SetObjects(goodsReceiptCartonAvailables);
                if (this.UsingPack) this.fastAvailablePacks.SetObjects(goodsReceiptPackAvailables);
                if (this.fileName != null) this.fastMismatchedBarcodes.SetObjects(mismatchedBarcodes);

                this.ShowRowCount();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
            finally
            {
                this.pendingPrimaryDetails.Each(p => { p.QuantityIssue = 0; p.LineVolumeIssue = 0; });
            }
        }

        public void ApplyFilter(string filterTexts)
        {
            this.fastAvailablePallets.CheckedObjects = null;
            this.fastAvailableCartons.CheckedObjects = null;
            if (this.UsingPack) this.fastAvailablePacks.CheckedObjects = null;

            this.fastAvailablePallets.SelectedObject = null;
            this.fastAvailableCartons.SelectedObject = null;
            if (this.UsingPack) this.fastAvailablePacks.SelectedObject = null;

            OLVHelpers.ApplyFilters(this.fastAvailablePallets, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            OLVHelpers.ApplyFilters(this.fastAvailableCartons, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            if (this.UsingPack) OLVHelpers.ApplyFilters(this.fastAvailablePacks, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

            this.ShowRowCount();
        }

        private void ShowRowCount()
        {
            this.customTabBatch.TabPages[0].Text = "Available " + this.fastAvailablePallets.GetItemCount().ToString("N0") + " pallet" + (this.fastAvailablePallets.GetItemCount() > 1 ? "s      " : "      ");
            this.customTabBatch.TabPages[1].Text = "Available " + this.fastAvailableCartons.GetItemCount().ToString("N0") + " carton" + (this.fastAvailableCartons.GetItemCount() > 1 ? "s      " : "      ");
            if (this.UsingPack) this.customTabBatch.TabPages[2].Text = "Available " + this.fastAvailablePacks.GetItemCount().ToString("N0") + " pack" + (this.fastAvailablePacks.GetItemCount() > 1 ? "s      " : "      ");
            if (this.fileName != null) this.customTabBatch.TabPages[this.customTabBatch.TabPages.Count - 1].Text = this.fastMismatchedBarcodes.GetItemCount().ToString("N0") + " Mismatched Barcode" + (this.fastMismatchedBarcodes.GetItemCount() > 1 ? "s      " : "      ");
        }


        private void fastAvailableGoodsReceiptDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.fileName == null)
            {//WHEN IMPORT: DEFUALT IS MULTIPLE SELECTED
                FastObjectListView fastAvailableGoodsReceiptDetails = (FastObjectListView)sender;
                if (fastAvailableGoodsReceiptDetails != null && fastAvailableGoodsReceiptDetails.SelectedObject != null)
                {//CLEAR ALL CHECKED OBJECTS => THEN CHECK THE CURRENT SELECTED ROW
                    GoodsReceiptDetailAvailable goodsReceiptDetailAvailable = (GoodsReceiptDetailAvailable)fastAvailableGoodsReceiptDetails.SelectedObject;
                    if (goodsReceiptDetailAvailable != null) { fastAvailableGoodsReceiptDetails.CheckedObjects = null; fastAvailableGoodsReceiptDetails.CheckObject(goodsReceiptDetailAvailable); }
                }
            }
        }

        private void fastAvailableGoodsReceiptDetails_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (this.fastAvailablePallets.CheckedObjects != null && this.fastAvailablePallets.CheckedObjects.Count > 0) this.buttonAddExit.Enabled = true; else this.buttonAddExit.Enabled = false;
            if (!this.buttonAddExit.Enabled && this.fastAvailableCartons.CheckedObjects != null && this.fastAvailableCartons.CheckedObjects.Count > 0) this.buttonAddExit.Enabled = true;
            if (this.UsingPack) if (!this.buttonAddExit.Enabled && this.fastAvailablePacks.CheckedObjects != null && this.fastAvailablePacks.CheckedObjects.Count > 0) this.buttonAddExit.Enabled = true;
        }

        private void buttonAddESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonAddExit))
                {
                    FastObjectListView fastAvailableGoodsReceiptDetails = this.customTabBatch.SelectedIndex == 0 ? this.fastAvailablePallets : (this.customTabBatch.SelectedIndex == 1 ? this.fastAvailableCartons : this.customTabBatch.SelectedIndex == 2 ? this.fastAvailablePacks : null);

                    if (fastAvailableGoodsReceiptDetails != null)
                    {
                        if (fastAvailableGoodsReceiptDetails.CheckedObjects.Count > 0)
                        {
                            this.goodsIssueViewModel.ViewDetails.RaiseListChangedEvents = false;
                            foreach (var checkedObjects in fastAvailableGoodsReceiptDetails.CheckedObjects)
                            {
                                GoodsReceiptDetailAvailable goodsReceiptDetailAvailable = (GoodsReceiptDetailAvailable)checkedObjects;
                                GoodsIssueDetailDTO goodsIssueDetailDTO = new GoodsIssueDetailDTO()
                                {
                                    GoodsIssueID = this.goodsIssueViewModel.GoodsIssueID,

                                    DeliveryAdviceID = goodsReceiptDetailAvailable.DeliveryAdviceID > 0 ? goodsReceiptDetailAvailable.DeliveryAdviceID : (int?)null,
                                    DeliveryAdviceDetailID = goodsReceiptDetailAvailable.DeliveryAdviceDetailID > 0 ? goodsReceiptDetailAvailable.DeliveryAdviceDetailID : (int?)null,
                                    DeliveryAdviceReference = goodsReceiptDetailAvailable.PrimaryReference,
                                    DeliveryAdviceEntryDate = goodsReceiptDetailAvailable.PrimaryEntryDate,

                                    TransferOrderID = goodsReceiptDetailAvailable.TransferOrderID > 0 ? goodsReceiptDetailAvailable.TransferOrderID : (int?)null,
                                    TransferOrderDetailID = goodsReceiptDetailAvailable.TransferOrderDetailID > 0 ? goodsReceiptDetailAvailable.TransferOrderDetailID : (int?)null,
                                    TransferOrderReference = goodsReceiptDetailAvailable.PrimaryReference,
                                    TransferOrderEntryDate = goodsReceiptDetailAvailable.PrimaryEntryDate,

                                    CommodityID = goodsReceiptDetailAvailable.CommodityID,
                                    CommodityCode = goodsReceiptDetailAvailable.CommodityCode,
                                    CommodityName = goodsReceiptDetailAvailable.CommodityName,

                                    PackageSize = goodsReceiptDetailAvailable.PackageSize,

                                    Volume = goodsReceiptDetailAvailable.Volume,
                                    PackageVolume = goodsReceiptDetailAvailable.PackageVolume,

                                    GoodsReceiptID = goodsReceiptDetailAvailable.GoodsReceiptID,
                                    GoodsReceiptDetailID = goodsReceiptDetailAvailable.GoodsReceiptDetailID,

                                    GoodsReceiptReference = goodsReceiptDetailAvailable.GoodsReceiptReference,
                                    GoodsReceiptEntryDate = goodsReceiptDetailAvailable.GoodsReceiptEntryDate,

                                    BatchID = goodsReceiptDetailAvailable.BatchID,
                                    BatchEntryDate = goodsReceiptDetailAvailable.BatchEntryDate,

                                    BinLocationID = goodsReceiptDetailAvailable.BinLocationID,
                                    BinLocationCode = goodsReceiptDetailAvailable.BinLocationCode,

                                    WarehouseID = goodsReceiptDetailAvailable.WarehouseID,
                                    WarehouseCode = goodsReceiptDetailAvailable.WarehouseCode,

                                    PackID = goodsReceiptDetailAvailable.PackID,
                                    PackCode = goodsReceiptDetailAvailable.PackCode,
                                    CartonID = goodsReceiptDetailAvailable.CartonID,
                                    CartonCode = goodsReceiptDetailAvailable.CartonCode,
                                    PalletID = goodsReceiptDetailAvailable.PalletID,
                                    PalletCode = goodsReceiptDetailAvailable.PalletCode,

                                    PackCounts = goodsReceiptDetailAvailable.PackCounts,
                                    CartonCounts = goodsReceiptDetailAvailable.CartonCounts,
                                    PalletCounts = goodsReceiptDetailAvailable.PalletCounts,

                                    QuantityAvailable = (decimal)goodsReceiptDetailAvailable.QuantityAvailable,
                                    LineVolumeAvailable = (decimal)goodsReceiptDetailAvailable.LineVolumeAvailable,

                                    QuantityRemains = goodsReceiptDetailAvailable.QuantityRemains,
                                    LineVolumeRemains = goodsReceiptDetailAvailable.LineVolumeRemains,

                                    Quantity = (decimal)goodsReceiptDetailAvailable.QuantityAvailable, //SHOULD: Quantity = QuantityAvailable (ALSO: LineVolume = LineVolumeAvailable): BECAUSE: WE ISSUE BY WHOLE UNIT OF PALLET/ OR CARTON/ OR PACK
                                    LineVolume = (decimal)goodsReceiptDetailAvailable.LineVolumeAvailable //IF Quantity > QuantityRemains (OR LineVolume > LineVolumeRemains) => THE GoodsIssueDetailDTO WILL BREAK THE ValidationRule => CAN NOT SAVE => USER MUST SELECT OTHER APPROPRIATE UNIT OF PALLET/ OR CARTON/ OR PACK WHICH MATCH THE Quantity/ LineVolume                                
                                };
                                this.goodsIssueViewModel.ViewDetails.Insert(0, goodsIssueDetailDTO);
                            }
                        }
                    }

                    if (this.MdiParent != null) this.MdiParent.DialogResult = DialogResult.OK; else this.DialogResult = DialogResult.OK;
                }

                if (sender.Equals(this.buttonESC))
                    if (this.MdiParent != null) this.MdiParent.DialogResult = DialogResult.Cancel; else this.DialogResult = DialogResult.Cancel;

            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
            finally
            {
                if (!this.goodsIssueViewModel.ViewDetails.RaiseListChangedEvents)
                {
                    this.goodsIssueViewModel.ViewDetails.RaiseListChangedEvents = true;
                    this.goodsIssueViewModel.ViewDetails.ResetBindings();
                }
            }
        }

        private void fastAvailableBarcodes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.buttonAddExit.Enabled)
                this.buttonAddESC_Click(this.buttonAddExit, new EventArgs());
        }

    }

    public class MismatchedBarcode
    {
        public string Barcode { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> QuantityAvailable { get; set; }
        public Nullable<decimal> LineVolumeAvailable { get; set; }
    }
}
