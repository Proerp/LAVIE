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
using TotalSmartCoding.Controllers.Productions;
using TotalCore.Services.Productions;
using TotalSmartCoding.ViewModels.Productions;

namespace TotalSmartCoding.Views.Productions
{
    public partial class QuickView : Form
    {
        private ScannerAPIs scannerAPIs;

        public QuickView(IList<BarcodeDTO> barcodeList, string caption)
        {
            InitializeComponent();

            this.scannerAPIs = new ScannerAPIs(CommonNinject.Kernel.Get<IPackRepository>(), CommonNinject.Kernel.Get<ICartonRepository>(), CommonNinject.Kernel.Get<IPalletRepository>());

            this.fastBarcodes.SetObjects(barcodeList);

            this.Text = caption;

            if (barcodeList.Count > 0)
            {
                PalletDTO palletDTO = barcodeList[0] as PalletDTO;
                if (palletDTO != null) { this.labelLock.Visible = true; }
            }
        }

        private void textFilter_TextChanged(object sender, EventArgs e)
        {
            OLVHelpers.ApplyFilters(this.fastBarcodes, textFilter.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
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
                    if (this.fastBarcodes.SelectedObject != null)
                    {
                        int fillingLineID = 0; int cartonID = 0; int palletID = 0; int batchID = 0;

                        CartonDTO cartonDTO = this.fastBarcodes.SelectedObject as CartonDTO;
                        if (cartonDTO != null) { fillingLineID = cartonDTO.FillingLineID; cartonID = cartonDTO.CartonID; }

                        PalletDTO palletDTO = this.fastBarcodes.SelectedObject as PalletDTO;
                        if (palletDTO != null) { fillingLineID = palletDTO.FillingLineID; palletID = palletDTO.PalletID; }

                        if (fillingLineID > 0)
                        {
                            QuickView quickView = new QuickView(this.scannerAPIs.GetBarcodeList((GlobalVariables.FillingLine)fillingLineID, cartonID, palletID, batchID), (cartonDTO != null ? "Carton: " + cartonDTO.Code : (palletDTO != null ? "Pallet: " + palletDTO.Code : "")));
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

        private void labelLock_Click(object sender, EventArgs e)
        {
            try
            {
                PalletDTO palletDTO = this.fastBarcodes.SelectedObject as PalletDTO;
                if (palletDTO != null) {

                    PalletViewModel palletViewModel = CommonNinject.Kernel.Get<PalletViewModel>();
                    PalletController palletController = new PalletController(CommonNinject.Kernel.Get<IPalletService>(), palletViewModel);
                    
                    palletController.Lock(palletDTO.PalletID);

                    if (CustomMsgBox.Show(this, "Are you sure you want to " + (palletViewModel.Lockable ? "lock" : "un-lock") + " this entry data" + "?", "Warning", MessageBoxButtons.YesNo, (palletViewModel.Lockable ? MessageBoxIcon.Information : MessageBoxIcon.Warning)) == DialogResult.Yes)
                        if (palletController.LockConfirmed())
                        {
                            ((PalletDTO)this.fastBarcodes.SelectedObject).Locked = !palletViewModel.Locked;
                            this.fastBarcodes.RefreshObject(this.fastBarcodes.SelectedObject);
                        }
                        else
                            throw new Exception("Lỗi khóa hay mở khóa pallet: " + palletViewModel.Code + "\r\n\r\nVui lòng đóng phần mềm và thử lại sau.");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

    }
}
