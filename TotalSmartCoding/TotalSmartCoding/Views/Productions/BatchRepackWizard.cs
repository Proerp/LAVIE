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
using TotalDTO.Productions;
using AutoMapper;
using TotalSmartCoding.ViewModels.Productions;
using TotalSmartCoding.Controllers.Productions;
using TotalCore.Services.Productions;


namespace TotalSmartCoding.Views.Productions
{
    public partial class BatchRepackWizard : Form
    {
        private CustomTabControl customTabBatch;
        public virtual ToolStrip toolstripChild { get; protected set; }

        private string fileName;

        private FillingData fillingData;
        private RepackViewModel repackViewModel;
        
        private List<BatchRepackDTO> batchRepacks;

        public BatchRepackWizard(FillingData fillingData, string fileName)
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripBottom;
            this.customTabBatch = new CustomTabControl();

            this.customTabBatch.Font = this.fastBatchRepacks.Font;
            this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
            this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

            this.customTabBatch.TabPages.Add("tabAvailablePallets", "Packs found");
            this.customTabBatch.TabPages.Add("tabMismatchedBarcodes", "Mismatched barcodes");
            this.customTabBatch.TabPages[0].Controls.Add(this.fastBatchRepacks);
            this.customTabBatch.TabPages[this.customTabBatch.TabPages.Count - 1].Controls.Add(this.fastMismatchedBarcodes);

            this.fastBatchRepacks.Dock = DockStyle.Fill;
            this.fastMismatchedBarcodes.Dock = DockStyle.Fill;
            this.customTabBatch.Dock = DockStyle.Fill;
            this.panelMaster.Controls.Add(this.customTabBatch);

            this.fileName = fileName;
            this.fillingData = fillingData;
            this.repackViewModel = CommonNinject.Kernel.Get<RepackViewModel>();

            this.fastBatchRepacks.AboutToCreateGroups += fastBarcodes_AboutToCreateGroups;
            this.fastMismatchedBarcodes.AboutToCreateGroups += fastBarcodes_AboutToCreateGroups;
            //this.fastBatchRepacks.ShowGroups = true;
            this.fastMismatchedBarcodes.ShowGroups = true;
        }

        private void fastBarcodes_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    //olvGroup.TitleImage = "Storage32";
                    olvGroup.Subtitle = olvGroup.Contents.Count.ToString() + " Pack(s)";
                }
            }
        }

        private void BatchRepackWizard_Load(object sender, EventArgs e)
        {
            try
            {
                this.batchRepacks = new List<BatchRepackDTO>();
                List<MismatchedBarcode> mismatchedBarcodes = new List<MismatchedBarcode>();

                string[] barcodes = System.IO.File.ReadAllLines(fileName); int lineIndex = 0; int mismatchedLineNo = 0; int serialID = 0;
                if (barcodes.Count() > 0)
                {
                    RepackController repackController = new RepackController(CommonNinject.Kernel.Get<IRepackService>(), this.repackViewModel);
                    foreach (string barcode in barcodes)
                    {
                        IList<BatchRepack> lookupRepacks = repackController.repackService.LookupRepacks(barcode);
                        if (lookupRepacks != null && lookupRepacks.Count > 0)
                        {
                            BatchRepack batchRepack = lookupRepacks.First();
                            BatchRepackDTO batchRepackDTO = Mapper.Map<BatchRepack, BatchRepackDTO>(batchRepack);

                            if (this.batchRepacks.Where(w => w.Code == batchRepackDTO.Code).Count() == 0)
                            {
                                if (batchRepackDTO.CommodityID == this.fillingData.CommodityID)
                                {
                                    batchRepackDTO.LineIndex = ++lineIndex;
                                    batchRepackDTO.SerialID = ++serialID;
                                    this.batchRepacks.Add(batchRepackDTO);
                                }
                                else
                                    mismatchedBarcodes.Add(new MismatchedBarcode() { LineIndex = ++mismatchedLineNo, SerialID = ++serialID, Barcode = barcode, APICode = batchRepackDTO.APICode, CommodityName = batchRepackDTO.CommodityName, Description = "Không cùng mã sản phẩm." });
                            }
                            else
                                mismatchedBarcodes.Add(new MismatchedBarcode() { LineIndex = ++mismatchedLineNo, SerialID = serialID, Barcode = barcode, APICode = batchRepackDTO.APICode, CommodityName = batchRepackDTO.CommodityName, Description = "Trùng mã vạch lon." });
                        }
                        else
                            mismatchedBarcodes.Add(new MismatchedBarcode() { LineIndex = ++mismatchedLineNo, SerialID = ++serialID, Barcode = barcode, Description = "Không tìm thấy mã vạch." });
                    }
                }

                this.fastBatchRepacks.SetObjects(this.batchRepacks);
                //if (this.batchRepacks.Count > 0) this.fastBatchRepacks.Sort(this.olvBatchCode, SortOrder.Descending);
                this.fastMismatchedBarcodes.SetObjects(mismatchedBarcodes);
                if (mismatchedBarcodes.Count > 0) this.fastMismatchedBarcodes.Sort(this.olvDescription, SortOrder.Ascending);

                this.customTabBatch.TabPages[0].Text = this.fastBatchRepacks.GetItemCount().ToString("N0") + " Pack" + (this.fastBatchRepacks.GetItemCount() > 1 ? "s" : "") + " found            ";
                this.customTabBatch.TabPages[this.customTabBatch.TabPages.Count - 1].Text = this.fastMismatchedBarcodes.GetItemCount().ToString("N0") + " Mismatched Barcode" + (this.fastMismatchedBarcodes.GetItemCount() > 1 ? "s      " : "      ");
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonAddESC_Click(object sender, EventArgs e)
        {
            int firstSavedRepackID = 0; string lastSavedBarcode = "";
            RepackController repackController = new RepackController(CommonNinject.Kernel.Get<IRepackService>(), this.repackViewModel);
            try
            {
                if (sender.Equals(this.buttonAddExit))
                {
                    foreach (var batchRepack in this.batchRepacks)
                    {
                        lastSavedBarcode = batchRepack.Code;
                        RepackDTO repackDTO = new RepackDTO();
                        repackDTO.BatchID = this.fillingData.BatchID;
                        repackDTO.PackID = batchRepack.PackID;
                        repackDTO.Code = batchRepack.Code;

                        repackDTO.SerialID = batchRepack.SerialID;

                        if (!repackController.repackService.Save(repackDTO))
                            throw new Exception(lastSavedBarcode);
                        else
                            if (firstSavedRepackID == 0) firstSavedRepackID = repackDTO.RepackID;
                    }
                    this.DialogResult = DialogResult.OK;
                }
                if (sender.Equals(this.buttonESC))
                    this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                if (firstSavedRepackID > 0) repackController.repackService.RepackRollback(this.fillingData.BatchID, firstSavedRepackID);
                ExceptionHandlers.ShowExceptionMessageBox(this, new Exception("Lỗi lưu barcode: " + lastSavedBarcode, exception));
            }
        }
    }

    public class MismatchedBarcode
    {
        public int LineIndex { get; set; }
        public int SerialID { get; set; }
        public string Barcode { get; set; }
        public string APICode { get; set; }
        public string CommodityName { get; set; }
        public string Description { get; set; }
    }
}
