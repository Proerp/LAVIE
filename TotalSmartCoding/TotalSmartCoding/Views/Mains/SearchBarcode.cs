using System;
using System.Windows.Forms;
using System.Collections.Generic;

using Ninject;

using TotalBase;
using TotalDTO.Productions;
using TotalCore.Repositories.Productions;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalSmartCoding.Libraries;
using System.Drawing;
using BrightIdeasSoftware;
using TotalSmartCoding.Views.Productions;
using TotalSmartCoding.ViewModels.Helpers;
using TotalBase.Enums;

namespace TotalSmartCoding.Views.Mains
{
    public partial class SearchBarcode : Form
    {
        private ScannerAPIs scannerAPIs;

        private CustomTabControl customTabBatch;

        public SearchBarcode(string barcode)
        {
            InitializeComponent();

            this.customTabBatch = new CustomTabControl();

            this.customTabBatch.Font = this.fastPacks.Font;
            this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
            this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

            this.customTabBatch.TabPages.Add("tabPendingPacks", "Packs");
            this.customTabBatch.TabPages.Add("tabPendingCartons", "Cartons");
            this.customTabBatch.TabPages.Add("tabPendingPallets", "Pallets");

            this.customTabBatch.TabPages[0].Controls.Add(this.fastPacks);
            this.customTabBatch.TabPages[0].Controls.Add(this.textFilterPack);
            this.customTabBatch.TabPages[1].Controls.Add(this.fastCartons);
            this.customTabBatch.TabPages[1].Controls.Add(this.textFilterCarton);
            this.customTabBatch.TabPages[2].Controls.Add(this.fastPallets);
            this.customTabBatch.TabPages[2].Controls.Add(this.textFilterPallet);

            this.textFilterPack.Dock = DockStyle.Top;
            this.textFilterCarton.Dock = DockStyle.Top;
            this.textFilterPallet.Dock = DockStyle.Top;
            this.fastPacks.Dock = DockStyle.Fill;
            this.fastCartons.Dock = DockStyle.Fill;
            this.fastPallets.Dock = DockStyle.Fill;
            this.customTabBatch.Dock = DockStyle.Fill;
            this.panelCenter.Controls.Add(this.customTabBatch);

            this.Text = "Search results for " + barcode.ToUpper();

            this.scannerAPIs = new ScannerAPIs(CommonNinject.Kernel.Get<IPackRepository>(), CommonNinject.Kernel.Get<ICartonRepository>(), CommonNinject.Kernel.Get<IPalletRepository>());

            this.fastPacks.SetObjects(this.scannerAPIs.SearchPacks(barcode));
            this.fastCartons.SetObjects(this.scannerAPIs.SearchCartons(barcode));
            this.fastPallets.SetObjects(this.scannerAPIs.SearchPallets(barcode));

            this.customTabBatch.TabPages[0].Text = "Pack " + this.fastPacks.GetItemCount().ToString("N0") + " pack" + (this.fastPacks.GetItemCount() > 1 ? "s" : "");
            this.customTabBatch.TabPages[1].Text = "Carton: " + this.fastCartons.GetItemCount().ToString("N0") + " carton" + (this.fastCartons.GetItemCount() > 1 ? "s" : "");
            this.customTabBatch.TabPages[2].Text = "Pallet: " + this.fastPallets.GetItemCount().ToString("N0") + " pallet" + (this.fastPallets.GetItemCount() > 1 ? "s" : "");

            this.customTabBatch.SelectedIndexChanged += customTabBatch_SelectedIndexChanged;
            //this.customTabBatch.SelectedIndexChanged += ;
        }


        private void textFilter_Enter(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text == "Enter any text here to search ...")
            {
                ((TextBox)sender).Text = "";
                ((TextBox)sender).ForeColor = SystemColors.ControlText;
            }
        }


        private void textFilter_Leave(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text.Trim() == "")
            {
                ((TextBox)sender).Text = "Enter any text here to search ...";
                ((TextBox)sender).ForeColor = SystemColors.ControlDark;
            }
        }

        private void textFilter_TextChanged(object sender, EventArgs e)
        {
            if (((TextBox)sender).Text != "Enter any text here to search ...")
            {
                if (sender.Equals(this.textFilterPack))
                    OLVHelpers.ApplyFilters(this.fastPacks, textFilterPack.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                if (sender.Equals(this.textFilterCarton))
                    OLVHelpers.ApplyFilters(this.fastCartons, textFilterCarton.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
                if (sender.Equals(this.textFilterPallet))
                    OLVHelpers.ApplyFilters(this.fastPallets, textFilterPallet.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            }
        }

        private void fastBarcodes_FormatRow(object sender, BrightIdeasSoftware.FormatRowEventArgs e)
        {
            e.Item.SubItems[0].Text = (e.RowIndex + 1).ToString();
        }

        private void fastBarcodes_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    FastObjectListView fastBarcodes = sender as FastObjectListView;
                    if (fastBarcodes.SelectedObject != null)
                    {
                        CartonDTO cartonDTO = null; PalletDTO palletDTO = null;
                        if (sender.Equals(this.fastCartons)) cartonDTO = fastBarcodes.SelectedObject as CartonDTO;
                        if (sender.Equals(this.fastPallets)) palletDTO = fastBarcodes.SelectedObject as PalletDTO;

                        if (cartonDTO != null || palletDTO != null)
                        {
                            QuickView quickView = new QuickView(this.scannerAPIs.GetBarcodeList((GlobalVariables.FillingLine)(cartonDTO != null ? cartonDTO.FillingLineID : palletDTO.FillingLineID), cartonDTO != null ? cartonDTO.CartonID : 0, palletDTO != null ? palletDTO.PalletID : 0, 0), cartonDTO != null ? "Carton: " + cartonDTO.Code : "Pallet: " + palletDTO.Code);
                            quickView.ShowDialog(); quickView.Dispose();
                        }
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        private void fastBarcodes_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FastObjectListView fastBarcodes = sender as FastObjectListView;
                if (fastBarcodes.SelectedObject != null)
                {
                    BarcodeDTO barcodeDTO = fastBarcodes.SelectedObject as BarcodeDTO;
                    if (barcodeDTO != null)
                    {
                        PrintViewModel printViewModel = new PrintViewModel();
                        printViewModel.ReportPath = "SearchBarcode";
                        printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("Barcode", barcodeDTO.Code));

                        SsrsViewer ssrsViewer = new SsrsViewer(printViewModel);
                        ssrsViewer.Show();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void customTabBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.labelReprintCarton.Visible = this.customTabBatch.SelectedIndex == 1;
        }

        private void labelReprintCarton_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.fastCartons.SelectedObject != null)
                {
                    CartonDTO cartonDTO = this.fastCartons.SelectedObject as CartonDTO;
                    if (cartonDTO != null)
                    {
                        GlobalEnums.ReprintCartonID = cartonDTO.CartonID;
                        this.DialogResult = System.Windows.Forms.DialogResult.Yes;
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
