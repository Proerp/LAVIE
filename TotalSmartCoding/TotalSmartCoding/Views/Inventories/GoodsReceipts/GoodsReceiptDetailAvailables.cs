using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Ninject;


using TotalSmartCoding.Views.Mains;


using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalCore.Repositories.Inventories;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalBase;
using TotalModel.Models;
using TotalSmartCoding.ViewModels.Helpers;
using TotalSmartCoding.ViewModels.Inventories;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using BrightIdeasSoftware;


namespace TotalSmartCoding.Views.Inventories.GoodsReceipts
{
    public partial class GoodsReceiptDetailAvailables : BaseView
    {
        private CustomTabControl customTabBatch;

        private GoodsReceiptAPIs goodsReceiptAPIs;

        public GoodsReceiptDetailAvailables()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;

            this.goodsReceiptAPIs = new GoodsReceiptAPIs(CommonNinject.Kernel.Get<IGoodsReceiptAPIRepository>());

            this.baseDTO = new GoodsReceiptDetailAvailableViewModel(); ;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                this.customTabBatch = new CustomTabControl();

                this.customTabBatch.Font = this.fastAvailablePallets.Font;
                this.customTabBatch.DisplayStyle = TabStyle.VisualStudio;
                this.customTabBatch.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

                this.customTabBatch.TabPages.Add("tabPendingPallets", "Pending pallets");
                this.customTabBatch.TabPages.Add("tabPendingCartons", "Pending cartons");
                this.customTabBatch.TabPages[0].Controls.Add(this.fastAvailablePallets);
                this.customTabBatch.TabPages[1].Controls.Add(this.fastAvailableCartons);


                this.customTabBatch.Dock = DockStyle.Fill;
                this.fastAvailablePallets.Dock = DockStyle.Fill;
                this.fastAvailableCartons.Dock = DockStyle.Fill;
                this.Controls.Add(this.customTabBatch);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingLocationID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.LocationID = ContextAttributes.User.LocationID;
            LocationAPIs locationAPIs = new LocationAPIs(CommonNinject.Kernel.Get<ILocationAPIRepository>());

            this.comboLocationID.ComboBox.DataSource = locationAPIs.GetLocationBases();
            this.comboLocationID.ComboBox.DisplayMember = CommonExpressions.PropertyName<LocationBase>(p => p.Name);
            this.comboLocationID.ComboBox.ValueMember = CommonExpressions.PropertyName<LocationBase>(p => p.LocationID);
            this.bindingLocationID = this.comboLocationID.ComboBox.DataBindings.Add("SelectedValue", this, "LocationID", true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingLocationID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastAvailablePallets.AboutToCreateGroups += fastAvailableItems_AboutToCreateGroups;
            this.fastAvailableCartons.AboutToCreateGroups += fastAvailableItems_AboutToCreateGroups;
            this.fastAvailablePallets.ShowGroups = true;
            this.fastAvailableCartons.ShowGroups = true;
            this.fastAvailablePallets.Sort(this.olvPalletCommodityCode);
            this.fastAvailableCartons.Sort(this.olvCartonCommodityCode);
        }

        private void fastAvailableItems_AboutToCreateGroups(object sender, BrightIdeasSoftware.CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = sender.Equals(this.fastAvailablePallets) ? "Pallet-32-O" : "Carton-32";
                    olvGroup.Subtitle = "List count: " + olvGroup.Contents.Count.ToString();
                }
            }
        }

        private int locationID;
        public int LocationID
        {
            get { return this.locationID; }
            set
            {
                if (this.locationID != value)
                {
                    this.locationID = value;
                    if (this.locationID > 0)
                    {
                        List<GoodsReceiptDetailAvailable> goodsReceiptDetailAvailables = goodsReceiptAPIs.GetGoodsReceiptDetailAvailables(this.LocationID, null, null, null, null, null, false);

                        this.fastAvailablePallets.SetObjects(goodsReceiptDetailAvailables.Where(w => w.PalletID != null));
                        this.fastAvailableCartons.SetObjects(goodsReceiptDetailAvailables.Where(w => w.CartonID != null));

                        this.ShowRowCount();
                    }
                }
            }
        }

        public override void ApplyFilter(string filterTexts)
        {
            OLVHelpers.ApplyFilters(this.fastAvailablePallets, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));
            OLVHelpers.ApplyFilters(this.fastAvailableCartons, filterTexts.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));

            this.ShowRowCount();
        }

        private void ShowRowCount()
        {
            decimal? totalQuantityAvailables = this.fastAvailablePallets.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.QuantityAvailable).Sum();
            decimal? totalLineVolumeAvailables = this.fastAvailablePallets.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.LineVolumeAvailable).Sum();
            this.customTabBatch.TabPages[0].Text = "Available " + this.fastAvailablePallets.GetItemCount().ToString("N0") + " pallet" + (this.fastAvailablePallets.GetItemCount() > 1 ? "s" : "") + ", Total quantity: " + (totalQuantityAvailables != null ? ((decimal)totalQuantityAvailables).ToString("N0") : "0") + ", Total volume: " + (totalLineVolumeAvailables != null ? ((decimal)totalLineVolumeAvailables).ToString("N2") : "0") + "       ";

            totalQuantityAvailables = this.fastAvailableCartons.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.QuantityAvailable).Sum();
            totalLineVolumeAvailables = this.fastAvailableCartons.FilteredObjects.Cast<GoodsReceiptDetailAvailable>().Select(o => o.LineVolumeAvailable).Sum();
            this.customTabBatch.TabPages[1].Text = "Available " + this.fastAvailableCartons.GetItemCount().ToString("N0") + " carton" + (this.fastAvailableCartons.GetItemCount() > 1 ? "s" : "") + ", Total quantity: " + (totalQuantityAvailables != null ? ((decimal)totalQuantityAvailables).ToString("N0") : "0") + ", Total volume: " + (totalLineVolumeAvailables != null ? ((decimal)totalLineVolumeAvailables).ToString("N2") : "0") + "       ";
        }

        protected override PrintViewModel InitPrintViewModel()
        {
            PrintViewModel printViewModel = base.InitPrintViewModel();
            printViewModel.ReportPath = "AvailableItems";
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("LocationID", this.LocationID.ToString()));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("LocationCode", this.comboLocationID.Text));
            return printViewModel;
        }

        private void buttonWarehouseJournals_Click(object sender, EventArgs e)
        {
            try
            {
                PrintViewModel printViewModel = new PrintViewModel();
                printViewModel.ReportPath = "WarehouseJournals";
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("LocationID", this.LocationID.ToString()));

                SsrsViewer ssrsViewer = new SsrsViewer(printViewModel);
                ssrsViewer.Show();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
    }
}
