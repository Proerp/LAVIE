using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Ninject;


using TotalSmartCoding.Views.Mains;


using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalBase;
using TotalModel.Models;
using TotalSmartCoding.ViewModels.Helpers;
using TotalSmartCoding.ViewModels.Generals;
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalCore.Repositories.Commons;
using BrightIdeasSoftware;
using TotalBase.Enums;
using TotalCore.Repositories.Generals;
using TotalSmartCoding.Controllers.APIs.Commons;
using System.ComponentModel;
using TotalCore.Services.Generals;
using TotalSmartCoding.Controllers.Generals;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalCore.Repositories.Productions;


namespace TotalSmartCoding.Views.Generals
{
    public partial class Reports : BaseView
    {
        #region MANAGE THE VIEW
        private TabPage tabPageWarehouses;
        private TabPage tabPageCommodities;
        private TabPage tabPageCustomers;
        private TabPage tabPageWarehouseIssues;
        private TabPage tabPageWarehouseReceipts;
        private TabPage tabPageWarehouseAdjustmentTypes;
        private TabPage tabPageBatchMasters;
        private TabPage tabPageBatchTypes;
        private TabPage tabPageFillingLines;
        private TabPage[] tabPages;
        private CustomTabControl customTabMaster;

        private ReportAPIs reportAPIs;
        private ReportViewModel reportViewModel { get; set; }

        public Reports()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastReportIndex;

            this.reportAPIs = new ReportAPIs(CommonNinject.Kernel.Get<IReportAPIRepository>());

            this.reportViewModel = CommonNinject.Kernel.Get<ReportViewModel>();
            this.reportViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.reportViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                this.tabPageWarehouses = new TabPage("Locations"); this.tabPageWarehouses.Tag = (int)GlobalEnums.ReportTabPageID.TabPageWarehouses;
                this.tabPageCommodities = new TabPage("Commodities"); this.tabPageCommodities.Tag = (int)GlobalEnums.ReportTabPageID.TabPageCommodities;
                this.tabPageCustomers = new TabPage("Customers"); this.tabPageCustomers.Tag = (int)GlobalEnums.ReportTabPageID.TabPageCustomers;
                this.tabPageWarehouseIssues = new TabPage("Source Warehouses"); this.tabPageWarehouseIssues.Tag = (int)GlobalEnums.ReportTabPageID.TabPageWarehouseIssues;
                this.tabPageWarehouseReceipts = new TabPage("Destination Warehouses"); this.tabPageWarehouseReceipts.Tag = (int)GlobalEnums.ReportTabPageID.TabPageWarehouseReceipts;
                this.tabPageWarehouseAdjustmentTypes = new TabPage("Adjustment Types"); this.tabPageWarehouseAdjustmentTypes.Tag = (int)GlobalEnums.ReportTabPageID.TabPageWarehouseAdjustmentTypes;
                this.tabPageBatchMasters = new TabPage("Batches"); this.tabPageBatchMasters.Tag = (int)GlobalEnums.ReportTabPageID.TabPageBatchMasters;
                this.tabPageBatchTypes = new TabPage("Batch Types"); this.tabPageBatchTypes.Tag = (int)GlobalEnums.ReportTabPageID.TabPageBatchTypes;
                this.tabPageFillingLines = new TabPage("Filling Lines"); this.tabPageFillingLines.Tag = (int)GlobalEnums.ReportTabPageID.TabPageFillingLines;

                this.tabPages = new TabPage[] { this.tabPageFillingLines, this.tabPageBatchMasters, this.tabPageBatchTypes, this.tabPageWarehouses, this.tabPageCommodities, this.tabPageCustomers, this.tabPageWarehouseIssues, this.tabPageWarehouseReceipts, this.tabPageWarehouseAdjustmentTypes };

                this.customTabMaster = new CustomTabControl();
                this.customTabMaster.Font = this.treeWarehouseID.Font;
                this.customTabMaster.DisplayStyle = TabStyle.VisualStudio;
                this.customTabMaster.DisplayStyleProvider.ImageAlign = ContentAlignment.MiddleLeft;

                this.tabPageWarehouses.Controls.Add(this.panelWarehouseID);
                this.tabPageCommodities.Controls.Add(this.panelCommodities);
                this.tabPageCustomers.Controls.Add(this.panelCustomers);
                this.tabPageWarehouseIssues.Controls.Add(this.panelWarehouseIssueID);
                this.tabPageWarehouseReceipts.Controls.Add(this.panelWarehouseReceiptID);
                this.tabPageWarehouseAdjustmentTypes.Controls.Add(this.panelWarehouseAdjustmentTypeID);
                this.tabPageBatchMasters.Controls.Add(this.panelBatchMasterID);
                this.tabPageBatchTypes.Controls.Add(this.panelBatchTypeID);
                this.tabPageFillingLines.Controls.Add(this.panelFillingLineID);

                this.panelWarehouseID.Dock = DockStyle.Fill;
                this.panelCommodities.Dock = DockStyle.Fill;
                this.panelCustomers.Dock = DockStyle.Fill;
                this.panelWarehouseIssueID.Dock = DockStyle.Fill;
                this.panelWarehouseReceiptID.Dock = DockStyle.Fill;
                this.panelWarehouseAdjustmentTypeID.Dock = DockStyle.Fill;
                this.panelBatchMasterID.Dock = DockStyle.Fill;
                this.panelBatchTypeID.Dock = DockStyle.Fill;
                this.panelFillingLineID.Dock = DockStyle.Fill;

                this.customTabMaster.Dock = DockStyle.Fill;
                this.panelCenter.Controls.Add(this.customTabMaster);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected override void InitializeReadOnlyModeBinding()
        {
            base.InitializeReadOnlyModeBinding();
            this.dateTimexFromDate.ReadOnly = false;
            this.dateTimexToDate.ReadOnly = false;
        }

        private IList<WarehouseTree> warehouseTrees;
        private IList<CommodityTree> commodityTrees;
        private IList<CommodityTypeTree> commodityTypeTrees;
        private IList<CustomerTree> customerTrees;
        private IList<EmployeeTree> employeeTrees;
        private IList<WarehouseTree> warehouseIssueTrees;
        private IList<WarehouseTree> warehouseReceiptTrees;
        private IList<WarehouseAdjustmentTypeTree> warehouseAdjustmentTypeTrees;
        private IList<BatchMasterTree> batchMasterTrees;
        private IList<BatchTypeTree> batchTypeTrees;
        private IList<FillingLineTree> fillingLineTrees;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.treeWarehouseID.RootKeyValue = 0;
            this.treeCommodityID.RootKeyValue = 0;
            this.treeCommodityTypeID.RootKeyValue = 0;
            this.treeCustomerID.RootKeyValue = 0;
            this.treeEmployeeID.RootKeyValue = 0;
            this.treeWarehouseIssueID.RootKeyValue = 0;
            this.treeWarehouseReceiptID.RootKeyValue = 0;
            this.treeWarehouseAdjustmentTypeID.RootKeyValue = 0;
            this.treeBatchMasterID.RootKeyValue = 0;
            this.treeBatchTypeID.RootKeyValue = 0;
            this.treeFillingLineID.RootKeyValue = 0;

            WarehouseAPIs warehouseAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());
            this.warehouseTrees = warehouseAPIs.GetWarehouseTrees(ContextAttributes.User.LocationID);
            this.treeWarehouseID.DataSource = new BindingSource(this.warehouseTrees, "");

            this.warehouseIssueTrees = warehouseAPIs.GetWarehouseTrees(null);
            this.treeWarehouseIssueID.DataSource = new BindingSource(this.warehouseIssueTrees, "");

            this.warehouseReceiptTrees = warehouseAPIs.GetWarehouseTrees(null);
            this.treeWarehouseReceiptID.DataSource = new BindingSource(this.warehouseReceiptTrees, "");

            WarehouseAdjustmentTypeAPIs warehouseAdjustmentTypeAPIs = new WarehouseAdjustmentTypeAPIs(CommonNinject.Kernel.Get<IWarehouseAdjustmentTypeAPIRepository>());
            this.warehouseAdjustmentTypeTrees = warehouseAdjustmentTypeAPIs.GetWarehouseAdjustmentTypeTrees();
            this.treeWarehouseAdjustmentTypeID.DataSource = new BindingSource(this.warehouseAdjustmentTypeTrees, "");

            BatchTypeAPIs batchTypeAPIs = new BatchTypeAPIs(CommonNinject.Kernel.Get<IBatchTypeAPIRepository>());
            this.batchTypeTrees = batchTypeAPIs.GetBatchTypeTrees();
            this.treeBatchTypeID.DataSource = new BindingSource(this.batchTypeTrees, "");

            FillingLineAPIs fillingLineAPIs = new FillingLineAPIs(CommonNinject.Kernel.Get<IFillingLineAPIRepository>());
            this.fillingLineTrees = fillingLineAPIs.GetFillingLineTrees();
            this.treeFillingLineID.DataSource = new BindingSource(this.fillingLineTrees, "");

            CommodityAPIs commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>());
            this.commodityTrees = commodityAPIs.GetCommodityTrees();
            this.treeCommodityID.DataSource = new BindingSource(this.commodityTrees, "");

            CommodityTypeAPIs commodityTypeAPIs = new CommodityTypeAPIs(CommonNinject.Kernel.Get<ICommodityTypeAPIRepository>());
            this.commodityTypeTrees = commodityTypeAPIs.GetCommodityTypeTrees();
            this.treeCommodityTypeID.DataSource = new BindingSource(this.commodityTypeTrees, "");

            CustomerAPIs customerAPIs = new CustomerAPIs(CommonNinject.Kernel.Get<ICustomerAPIRepository>());
            this.customerTrees = customerAPIs.GetCustomerTrees();
            this.treeCustomerID.DataSource = new BindingSource(this.customerTrees, "");

            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());
            this.employeeTrees = employeeAPIs.GetEmployeeTrees();
            this.treeEmployeeID.DataSource = new BindingSource(this.employeeTrees, "");

            this.comboSummaryVersusDetail.ComboBox.Items.AddRange(new string[] { "Summary only", "Show detail" });
            this.comboSummaryVersusDetail.ComboBox.SelectedIndex = 0;

            this.comboQuantityVersusVolume.ComboBox.Items.AddRange(new string[] { "By quantity", "By volume" });
            this.comboQuantityVersusVolume.ComboBox.SelectedIndex = 1;

            this.comboDateVersusMonth.ComboBox.Items.AddRange(new string[] { "Daily summary", "Monthly summary" });
            this.comboDateVersusMonth.ComboBox.SelectedIndex = 1;

            this.comboSalesVersusPromotion.ComboBox.Items.AddRange(new string[] { "Sales & promotions", "Sales only", "Promotions only" });
            this.comboSalesVersusPromotion.ComboBox.SelectedIndex = 0;

            this.comboPalletVersusCartonAndPack.ComboBox.Items.AddRange(new string[] { "Pallet Only", "Pallet & Carton", "Pallet, Carton & Pack" });
            this.comboPalletVersusCartonAndPack.ComboBox.SelectedIndex = 0;

            this.comboForecastFilters.ComboBox.Items.AddRange(new string[] { "[All Items in Stock]", "[Current Stock + In Transit] > 0", "[Current Stock + In Transit] = 0", "[Current Stock + In Transit] < 0", "[Current Stock + In Transit] Between Low-High Level", "[Current Stock + In Transit] Out of Range Low-High Level", "[Current Stock + In Transit] Under Low-Level", "[Current Stock + In Transit] Over High-Level" });
            this.comboForecastFilters.ComboBox.SelectedIndex = 0;

            this.comboSlowMoving.Items.AddRange(new string[] { "Regardless forecast", "And without forecast" });
            this.comboSlowMoving.SelectedIndex = 0;

            this.dateTimexFromDate.DataBindings.Add("Value", GlobalEnums.GlobalOptionSetting, CommonExpressions.PropertyName<OptionSetting>(p => p.FromDate), true, DataSourceUpdateMode.OnPropertyChanged);
            this.dateTimexToDate.DataBindings.Add("Value", GlobalEnums.GlobalOptionSetting, CommonExpressions.PropertyName<OptionSetting>(p => p.ToDate), true, DataSourceUpdateMode.OnPropertyChanged);

            this.fastReportIndex.AboutToCreateGroups += fastReportIndex_AboutToCreateGroups;
            this.fastReportIndex.ShowGroups = true;
            //this.olvApproved.Renderer = new MappedImageRenderer(new Object[] { 1, Resources.Placeholder16, 2, Resources.Void_16 });
        }

        private void fastReportIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            //return; //DON'T SHOW ICON
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Analytics";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Reports";

                    if ((string)olvGroup.Key == "2.GOODS RECEIPT JOURNALS" || (string)olvGroup.Key == "3.GOODS RECEIPT PIVOT REPORTS") olvGroup.Collapsed = true;
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new ReportController(CommonNinject.Kernel.Get<IReportService>(), this.reportViewModel); }
        }

        public override void Loading()
        {
            BatchMasterAPIs batchMasterAPIs = new BatchMasterAPIs(CommonNinject.Kernel.Get<IBatchMasterAPIRepository>());
            this.batchMasterTrees = batchMasterAPIs.GetBatchMasterTrees(GlobalEnums.GlobalOptionSetting.LowerFillterDate, GlobalEnums.GlobalOptionSetting.UpperFillterDate);
            this.treeBatchMasterID.DataSource = new BindingSource(this.batchMasterTrees, "");
            this.treeBatchMasterID.Sort(this.olvBatchMasterCode, SortOrder.Descending);

            this.fastReportIndex.SetObjects(this.reportAPIs.GetReportIndexes());

            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastReportIndex.Sort(this.olvReportGroupName, SortOrder.Ascending);

            if (this.treeWarehouseID.GetModelObject(0) != null) { this.treeWarehouseID.Expand(this.treeWarehouseID.GetModelObject(0)); if (this.treeWarehouseID.Items.Count >= 2) this.treeWarehouseID.SelectedIndex = 1; }
            if (this.treeCommodityID.GetModelObject(0) != null) { this.treeCommodityID.Expand(this.treeCommodityID.GetModelObject(0)); if (this.treeCommodityID.Items.Count >= 2) this.treeCommodityID.SelectedIndex = 1; }
            if (this.treeCommodityTypeID.GetModelObject(0) != null) { this.treeCommodityTypeID.Expand(this.treeCommodityTypeID.GetModelObject(0)); if (this.treeCommodityTypeID.Items.Count >= 2) this.treeCommodityTypeID.SelectedIndex = 1; }
            if (this.treeCustomerID.GetModelObject(0) != null) { this.treeCustomerID.Expand(this.treeCustomerID.GetModelObject(0)); if (this.treeCustomerID.Items.Count >= 2) this.treeCustomerID.SelectedIndex = 1; }
            if (this.treeEmployeeID.GetModelObject(0) != null) { this.treeEmployeeID.Expand(this.treeEmployeeID.GetModelObject(0)); if (this.treeEmployeeID.Items.Count >= 2) this.treeEmployeeID.SelectedIndex = 1; }
            if (this.treeWarehouseIssueID.GetModelObject(0) != null) { this.treeWarehouseIssueID.Expand(this.treeWarehouseIssueID.GetModelObject(0)); if (this.treeWarehouseIssueID.Items.Count >= 2) this.treeWarehouseIssueID.SelectedIndex = 1; }
            if (this.treeWarehouseReceiptID.GetModelObject(0) != null) { this.treeWarehouseReceiptID.Expand(this.treeWarehouseReceiptID.GetModelObject(0)); if (this.treeWarehouseReceiptID.Items.Count >= 2) this.treeWarehouseReceiptID.SelectedIndex = 1; }
            if (this.treeWarehouseAdjustmentTypeID.GetModelObject(0) != null) { this.treeWarehouseAdjustmentTypeID.Expand(this.treeWarehouseAdjustmentTypeID.GetModelObject(0)); if (this.treeWarehouseAdjustmentTypeID.Items.Count >= 2) this.treeWarehouseAdjustmentTypeID.SelectedIndex = 1; }
            if (this.treeBatchMasterID.GetModelObject(0) != null) { this.treeBatchMasterID.Expand(this.treeBatchMasterID.GetModelObject(0)); if (this.treeBatchMasterID.Items.Count >= 2) this.treeBatchMasterID.SelectedIndex = 1; }
            if (this.treeBatchTypeID.GetModelObject(0) != null) { this.treeBatchTypeID.Expand(this.treeBatchTypeID.GetModelObject(0)); if (this.treeBatchTypeID.Items.Count >= 2) this.treeBatchTypeID.SelectedIndex = 1; }
            if (this.treeFillingLineID.GetModelObject(0) != null) { this.treeFillingLineID.Expand(this.treeFillingLineID.GetModelObject(0)); if (this.treeFillingLineID.Items.Count >= 2) this.treeFillingLineID.SelectedIndex = 1; }
        }

        #endregion MANAGE THE VIEW

        #region CONTEXTUAL LOAD TAB PAGE: TAB FOR FILTER
        protected override void invokeEdit(int? id)
        {
            try
            {
                base.invokeEdit(id);

                this.reloadTabPages();


                this.dateTimexFromDate.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.FromDate)) != -1; this.labelFromDate.Visible = this.dateTimexFromDate.Visible; this.pictureFromDate.Visible = this.dateTimexFromDate.Visible;
                this.dateTimexToDate.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.ToDate)) != -1; this.labelToDate.Visible = this.dateTimexToDate.Visible; this.pictureToDate.Visible = this.dateTimexToDate.Visible; labelToDate.Text = this.dateTimexFromDate.Visible ? "To" : "As at";

                this.comboSummaryVersusDetail.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.SummaryVersusDetail)) != -1;
                this.comboQuantityVersusVolume.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.QuantityVersusVolume)) != -1; this.buttonQuantityVersusVolume.Visible = this.comboSummaryVersusDetail.Visible || this.comboQuantityVersusVolume.Visible;
                this.comboDateVersusMonth.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.DateVersusMonth)) != -1; this.buttonDateVersusMonth.Visible = this.comboDateVersusMonth.Visible;
                this.comboSalesVersusPromotion.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.SalesVersusPromotion)) != -1;
                this.comboPalletVersusCartonAndPack.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.PalletVersusCartonAndPack)) != -1;

                this.comboForecastFilters.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.ForecastFilters)) != -1; this.buttonSalesVersusPromotion.Visible = this.comboSalesVersusPromotion.Visible || this.comboPalletVersusCartonAndPack.Visible || this.comboForecastFilters.Visible;

                this.comboSlowMoving.Visible = this.reportViewModel.OptionBoxIDs.IndexOf(GlobalEnums.OBx(GlobalEnums.OptionBoxID.SlowMoving)) != -1; this.labelSlowMoving.Visible = this.comboSlowMoving.Visible; this.numericSlowMoving.Visible = this.comboSlowMoving.Visible;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void reloadTabPages()
        {
            try
            {
                this.customTabMaster.SuspendLayout();
                this.clearTabPages();
                foreach (TabPage tabpage in this.tabPages)
                {
                    if (this.reportViewModel.ReportTabPageIDs.IndexOf(tabpage.Tag.ToString()) != -1 && !this.customTabMaster.TabPages.Contains(tabpage))
                        this.customTabMaster.TabPages.Add(tabpage);
                }
                //if (this.customTabMaster.TabPages.Contains(this.tabPageCommodities)) this.customTabMaster.SelectedTab = this.tabPageCommodities;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
            finally
            {
                this.customTabMaster.ResumeLayout();
            }
        }

        private void clearTabPages()
        {
            try
            {
                foreach (TabPage tabpage in this.customTabMaster.TabPages)
                {
                    if (this.reportViewModel.ReportTabPageIDs.IndexOf(tabpage.Tag.ToString()) == -1)
                    {//CALL CollapseAll() TO PREVENT UNKNOW ERROR!!! //IF WE DON'T COLLAPSE ALL THE TREE, IT WIL RAISE ERROR WHEN WE CALL CLEAR OR REMOVE TABPAGES
                        if (tabpage.Equals(this.tabPageWarehouses))
                        { this.treeWarehouseID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageCommodities))
                        { this.treeCommodityID.CollapseAll(); this.treeCommodityTypeID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageCustomers))
                        { this.treeEmployeeID.CollapseAll(); this.treeCustomerID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageWarehouseIssues))
                        { this.treeWarehouseIssueID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageWarehouseReceipts))
                        { this.treeWarehouseReceiptID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageWarehouseAdjustmentTypes))
                        { this.treeWarehouseAdjustmentTypeID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageBatchMasters))
                        { this.treeBatchMasterID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageBatchTypes))
                        { this.treeBatchTypeID.CollapseAll(); }
                        if (tabpage.Equals(this.tabPageFillingLines))
                        { this.treeFillingLineID.CollapseAll(); }

                        this.customTabMaster.TabPages.Remove(tabpage);
                    }
                }
            }
            catch (Exception exception)
            { //EVENT WHEN ERROR OCCUR, WE IGNORE IT
                int i = 1;
                //ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
        #endregion CONTEXTUAL LOAD TAB PAGE: TAB FOR FILTER



        protected override PrintViewModel InitPrintViewModel()
        {
            PrintViewModel printViewModel = base.InitPrintViewModel();
            printViewModel.ReportPath = this.reportViewModel.ReportURL;

            this.PassFilterParameters(printViewModel);

            return printViewModel;
        }

        private void PassFilterParameters(PrintViewModel printViewModel)
        {
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("UserID", ContextAttributes.User.UserID.ToString()));
            string headerTitle = this.reportViewModel.ReportName; string headerTerms = "";

            if (this.dateTimexFromDate.Visible || this.dateTimexToDate.Visible)
            {
                if (this.dateTimexFromDate.Visible) printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("FromDate", this.dateTimexFromDate.Text));
                if (this.dateTimexToDate.Visible) printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("ToDate", this.dateTimexToDate.Text));
                headerTerms = headerTerms + " " + (this.dateTimexFromDate.Visible ? this.labelFromDate.Text + " " + this.dateTimexFromDate.Text : "") + " " + (this.dateTimexToDate.Visible ? this.labelToDate.Text + " " + this.dateTimexToDate.Text : "");
            }



            if (this.comboForecastFilters.Visible)
            {
                if (this.comboForecastFilters.ComboBox.SelectedIndex != 0) headerTitle = headerTitle + " [" + this.comboForecastFilters.Text + "]";
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("FilterID", (this.comboForecastFilters.ComboBox.SelectedIndex - 1).ToString()));
            }

            if (this.comboSlowMoving.Visible)
            {
                headerTerms = "Not sold over " + this.numericSlowMoving.Value.ToString("N0") + " days" + (this.comboSlowMoving.SelectedIndex != 0 ? "; " + this.comboSlowMoving.Text : "") + ". " + headerTerms;
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("FilterID", ((int)GlobalEnums.ForecastFilterID.SlowMoving + this.numericSlowMoving.Value + (this.comboSlowMoving.SelectedIndex != 0 ? (int)GlobalEnums.ForecastFilterID.SlowMovingNoForecast : 0)).ToString()));
            }

            if (this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsReceiptPivot || this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsIssuePivot || this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsReceiptJournal || this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsIssueJournal)
            {
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("IssueVersusReceipt", (this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsIssuePivot || this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsIssueJournal ? 0 : 1).ToString()));

                if (this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsIssuePivot || this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsIssueJournal)
                    printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("GoodsIssueTypeIDs", (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.SalesIssuePivot || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.SalesIssuePivotbyCustomers || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.SalesIssueJournal ? ((int)GlobalEnums.GoodsIssueTypeID.DeliveryAdvice).ToString() : (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.TransferIssuePivot || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.TransferIssueJournal ? ((int)GlobalEnums.GoodsIssueTypeID.TransferOrder).ToString() : (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.AdjustmentIssuePivot || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.AdjustmentIssueJournal ? ((int)GlobalEnums.GoodsIssueTypeID.WarehouseAdjustment).ToString() : null)))));

                if (this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsReceiptPivot || this.reportViewModel.ReportTypeID == (int)GlobalEnums.ReportTypeID.GoodsReceiptJournal)
                    printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("GoodsReceiptTypeIDs", (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.ProductionReceiptPivot || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.ProductionReceiptJournal ? ((int)GlobalEnums.GoodsReceiptTypeID.Pickup).ToString() : (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.TransferReceiptPivot || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.TransferReceiptJournal ? ((int)GlobalEnums.GoodsReceiptTypeID.GoodsIssueTransfer).ToString() : (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.AdjustmentReceiptPivot || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.AdjustmentReceiptJournal ? ((int)GlobalEnums.GoodsReceiptTypeID.WarehouseAdjustments).ToString() : null)))));

                if (this.buttonDateVersusMonth.Visible)
                {
                    headerTitle = this.comboDateVersusMonth.Text + " " + headerTitle;
                    printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("DateVersusMonth", this.comboDateVersusMonth.ComboBox.SelectedIndex.ToString()));
                }

                if (this.comboSalesVersusPromotion.Visible)
                {
                    if (this.comboSalesVersusPromotion.ComboBox.SelectedIndex != 0) headerTitle = headerTitle + " [" + this.comboSalesVersusPromotion.Text + "]";
                    printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("SalesVersusPromotion", (this.comboSalesVersusPromotion.ComboBox.SelectedIndex - 1).ToString()));
                }
            }

            if (this.comboPalletVersusCartonAndPack.Visible && (!this.comboSummaryVersusDetail.Visible || this.comboSummaryVersusDetail.ComboBox.SelectedIndex == 1))
            {
                if (this.comboPalletVersusCartonAndPack.ComboBox.SelectedIndex != 0) headerTitle = headerTitle + " [" + this.comboPalletVersusCartonAndPack.Text + "]";
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("WithCartons", (this.comboPalletVersusCartonAndPack.ComboBox.SelectedIndex == 1 || this.comboPalletVersusCartonAndPack.ComboBox.SelectedIndex == 2).ToString()));
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("WithPacks", (this.comboPalletVersusCartonAndPack.ComboBox.SelectedIndex == 2).ToString()));
            }

            if (this.comboQuantityVersusVolume.Visible)
            {
                headerTitle = headerTitle + " [REPORT " + this.comboQuantityVersusVolume.Text + "]";
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("QuantityVersusVolume", this.comboQuantityVersusVolume.ComboBox.SelectedIndex.ToString()));
            }

            if (this.comboSummaryVersusDetail.Visible)
            {
                headerTitle = headerTitle + " [" + this.comboSummaryVersusDetail.Text + "]";
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("SummaryVersusDetail", this.comboSummaryVersusDetail.ComboBox.SelectedIndex.ToString()));
            }

            string captionDescriptions = "";

            if (this.customTabMaster.TabPages.Contains(this.tabPageWarehouses))
                this.AddFilterParameters(printViewModel, this.warehouseTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("LocationID", "LocationIDs", "Location", true, false), new FilterParameter("WarehouseID", "WarehouseIDs", "Warehouse", true, false) }, ref captionDescriptions);
            if (this.customTabMaster.TabPages.Contains(this.tabPageCommodities))
            {
                this.AddFilterParameters(printViewModel, this.commodityTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("CommodityCategoryID", "CommodityCategoryIDs", "Category", true, false), new FilterParameter("CommodityID", "CommodityIDs", "Item", true, false) }, ref captionDescriptions);
                this.AddFilterParameters(printViewModel, this.commodityTypeTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("CommodityTypeID", "CommodityTypeIDs", "Item Type", true, false) }, ref captionDescriptions);
            }
            if (this.customTabMaster.TabPages.Contains(this.tabPageCustomers))
            {
                this.AddFilterParameters(printViewModel, this.customerTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("CustomerCategoryID", "CustomerCategoryIDs", "Channel", true, false), new FilterParameter("CustomerID", "CustomerIDs", "Customer", true, true) }, ref captionDescriptions);
                this.AddFilterParameters(printViewModel, this.employeeTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("TeamID", "TeamIDs", "Team", true, false), new FilterParameter("EmployeeID", "EmployeeIDs", "Salesperson", true, false) }, ref captionDescriptions);
            }
            if (this.customTabMaster.TabPages.Contains(this.tabPageWarehouseIssues))
                this.AddFilterParameters(printViewModel, this.warehouseIssueTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("LocationID", "LocationIssueIDs", "Source Location", true, false), new FilterParameter("WarehouseID", "WarehouseIssueIDs", "Source Warehouse", true, false) }, ref captionDescriptions);
            if (this.customTabMaster.TabPages.Contains(this.tabPageWarehouseReceipts))
                this.AddFilterParameters(printViewModel, this.warehouseReceiptTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("LocationID", "LocationReceiptIDs", "Destination Location", true, false), new FilterParameter("WarehouseID", "WarehouseReceiptIDs", "Destination Warehouse", true, false) }, ref captionDescriptions);
            if (this.customTabMaster.TabPages.Contains(this.tabPageWarehouseAdjustmentTypes))
                this.AddFilterParameters(printViewModel, this.warehouseAdjustmentTypeTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("WarehouseAdjustmentTypeID", "WarehouseAdjustmentTypeIDs", "Adjustment Type", true, false) }, ref captionDescriptions);
            if (this.customTabMaster.TabPages.Contains(this.tabPageBatchMasters))
                this.AddFilterParameters(printViewModel, this.batchMasterTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("BatchMasterID", "BatchMasterIDs", "Batch", true, false) }, ref captionDescriptions);
            if (this.customTabMaster.TabPages.Contains(this.tabPageBatchTypes))
                this.AddFilterParameters(printViewModel, this.batchTypeTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("BatchTypeID", "BatchTypeIDs", "Batch Type", true, false) }, ref captionDescriptions);
            if (this.customTabMaster.TabPages.Contains(this.tabPageFillingLines))
                this.AddFilterParameters(printViewModel, this.fillingLineTrees.Cast<IFilterTree>(), new FilterParameter[] { new FilterParameter("FillingLineID", "FillingLineIDs", "Filling Line", true, false) }, ref captionDescriptions);




            if (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.PivotStockDIOH3M || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.PivotStockDRP || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.PivotStockDIOH3MAndDRP || this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.OldAndSlowMoving)
                printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("PrintOptionID", (this.reportViewModel.ReportID == (int)GlobalEnums.ReportID.OldAndSlowMoving ? (int)GlobalEnums.ReportID.PivotStockDIOH3M : this.reportViewModel.ReportID).ToString()));

            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("HeaderTitle", headerTitle.ToUpper()));
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("HeaderTerms", headerTerms.Trim()));
            if (captionDescriptions != "") printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("CaptionDescriptions", captionDescriptions));
        }

        private void AddFilterParameters(PrintViewModel printViewModel, IEnumerable<IFilterTree> filterTrees, FilterParameter[] filterParameters, ref string captionDescriptions)
        {
            this.GetFilterParameters(filterTrees, filterParameters);

            foreach (FilterParameter filterParameter in filterParameters)
            {
                if (filterParameter.PrimaryIDs != null && filterParameter.PrimaryIDs != "")
                {
                    printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter(filterParameter.ParameterSSRS, filterParameter.PrimaryIDs));
                    if (filterParameter.ShouldCaptionCode || filterParameter.ShouldCaptionName)
                        captionDescriptions = captionDescriptions + (captionDescriptions != "" ? "; " : "") + filterParameter.CaptionLabel + ": " + filterParameter.CaptionDescriptions;
                }
            }
        }

        private void GetFilterParameters(IEnumerable<IFilterTree> filterTrees, FilterParameter[] filterParameters)
        {
            if (filterTrees.Count() > 0 && filterParameters.Count() > 0)
            {
                IList<IFilterTree> selectedFilterTrees = filterTrees.Where(w => w.Selected == true).ToList();
                if (selectedFilterTrees.Count > 0)
                {
                    if (selectedFilterTrees.Where(w => w.NodeID == GlobalEnums.RootNode).FirstOrDefault() != null) return;

                    IList<IFilterTree> effectedFilterTrees = null;
                    foreach (FilterParameter filterParameter in filterParameters)
                    {//WE SHOULD SET THE ORDER OF ELEMENT IN filterParameters BY THE HIERARCHY STRUCTURE. THIS IS VERY IMPORTANNT: FOR EXCAMPLE: filterParameters[0] = LocationID, filterParameters[1] = WarehouseID
                        IEnumerable<IFilterTree> enumerableFilterTree = selectedFilterTrees.Where(w => w.ParameterName == filterParameter.ParameterName);
                        if (effectedFilterTrees != null)
                        {//BY THE ORDER OF THE HIERARCHY STRUCTURE, WE CAN USE THE QUERY BELLOW TO EXCLUSIVE (ORMIT) THE CHILD THAT ITS ANCESTORID EXIST IN THE PARENT LIST
                            List<int?> ancestorIDs = effectedFilterTrees.Select(n => n.PrimaryID).ToList();
                            enumerableFilterTree = enumerableFilterTree.Where(w => !ancestorIDs.Contains(w.AncestorID));
                        }
                        effectedFilterTrees = enumerableFilterTree.ToList();

                        filterParameter.PrimaryIDs = string.Join(",", effectedFilterTrees.Select(s => s.PrimaryID));

                        if (filterParameter.ShouldCaptionCode && filterParameter.ShouldCaptionName)
                            filterParameter.CaptionDescriptions = string.Join(", ", effectedFilterTrees.Select(s => s.Code + "-" + s.Name));
                        else
                            if (filterParameter.ShouldCaptionCode)
                                filterParameter.CaptionDescriptions = string.Join(", ", effectedFilterTrees.Select(s => s.Code));
                            else
                                filterParameter.CaptionDescriptions = string.Join(", ", effectedFilterTrees.Select(s => s.Name));
                    }
                }
            }
        }

        private void buttonCollapse_Click(object sender, EventArgs e)
        {
            try
            {
                ToolStripButton senderButton = sender as ToolStripButton;
                if (sender != null)
                {
                    DataTreeListView dataTreeListView = senderButton.Owner.Equals(this.stripCommodityType) ? this.treeCommodityTypeID : (senderButton.Owner.Equals(this.stripCommodity) ? this.treeCommodityID : (senderButton.Owner.Equals(this.stripEmployee) ? this.treeEmployeeID : (senderButton.Owner.Equals(this.stripCustomer) ? this.treeCustomerID : (senderButton.Owner.Equals(this.stripWarehouse) ? this.treeWarehouseID : (senderButton.Owner.Equals(this.stripWarehouseReceipt) ? this.treeWarehouseReceiptID : (senderButton.Owner.Equals(this.stripWarehouseAdjustmentType) ? this.treeWarehouseAdjustmentTypeID : (senderButton.Owner.Equals(this.stripWarehouseIssue) ? this.treeWarehouseIssueID : (senderButton.Owner.Equals(this.stripBatchMaster) ? this.treeBatchMasterID : (senderButton.Owner.Equals(this.stripBatchType) ? this.treeBatchTypeID : (senderButton.Owner.Equals(this.stripFillingLine) ? this.treeFillingLineID : null))))))))));
                    if (dataTreeListView != null)
                    {
                        if (senderButton.Tag.ToString().ToUpper() == "COLLAPSE") { dataTreeListView.CollapseAll(); dataTreeListView.Expand(dataTreeListView.GetModelObject(0)); }
                        if (senderButton.Tag.ToString().ToUpper() == "EXPAND") dataTreeListView.ExpandAll();
                        if (senderButton.Tag.ToString().ToUpper() == "SELECT" || senderButton.Tag.ToString().ToUpper() == "SELECTFILTER" || senderButton.Tag.ToString().ToUpper() == "DESELECT")
                        {
                            List<IFilterTree> filterTrees = dataTreeListView.FilteredObjects.Cast<IFilterTree>().ToList();

                            if (filterTrees != null && filterTrees.Count() > 0)
                            {
                                string textFilter = "";
                                if (senderButton.Tag.ToString().ToUpper() == "SELECTFILTER")
                                    textFilter = (senderButton.Owner.Equals(this.stripCommodityType) ? this.textCommodityType.Text : (senderButton.Owner.Equals(this.stripCommodity) ? this.textCommodity.Text : (senderButton.Owner.Equals(this.stripEmployee) ? this.textEmployee.Text : (senderButton.Owner.Equals(this.stripCustomer) ? this.textCustomer.Text : (senderButton.Owner.Equals(this.stripWarehouse) ? this.textWarehouse.Text : (senderButton.Owner.Equals(this.stripWarehouseReceipt) ? this.textWarehouseReceipt.Text : (senderButton.Owner.Equals(this.stripWarehouseAdjustmentType) ? this.textWarehouseAdjustmentType.Text : (senderButton.Owner.Equals(this.stripWarehouseIssue) ? this.textWarehouseIssue.Text : (senderButton.Owner.Equals(this.stripBatchMaster) ? this.textBatchMaster.Text : (senderButton.Owner.Equals(this.stripBatchType) ? this.textBatchType.Text : (senderButton.Owner.Equals(this.stripFillingLine) ? this.textFillingLine.Text : null))))))))))).ToUpper();

                                bool selected = senderButton.Tag.ToString().ToUpper() != "DESELECT";

                                filterTrees.Each(filterItem =>
                                {
                                    if (senderButton.Tag.ToString().ToUpper() == "SELECTFILTER")
                                        filterItem.Selected = filterItem.Code.ToUpper().IndexOf(textFilter) != -1 || (filterItem.Name != null && filterItem.Name.ToUpper().IndexOf(textFilter) != -1);
                                    else
                                        filterItem.Selected = selected;
                                }); //dataTreeListView.RefreshObject(filterItem); NO NEED TO RefreshObject EVERY OBJECT. 


                                if (!selected) filterTrees[0].Selected = true;
                                dataTreeListView.RefreshObject(filterTrees[0]); //JUST RefreshObject THE FIRST OBJECT WHEN FINISH.
                            }

                        }
                        if (senderButton.Tag.ToString().ToUpper() == "CLEAR")
                        {
                            TextBox textBoxCLEAR = senderButton.Owner.Equals(this.stripCommodityType) ? this.textCommodityType : (senderButton.Owner.Equals(this.stripCommodity) ? this.textCommodity : (senderButton.Owner.Equals(this.stripEmployee) ? this.textEmployee : (senderButton.Owner.Equals(this.stripCustomer) ? this.textCustomer : (senderButton.Owner.Equals(this.stripWarehouse) ? this.textWarehouse : (senderButton.Owner.Equals(this.stripWarehouseReceipt) ? this.textWarehouseReceipt : (senderButton.Owner.Equals(this.stripWarehouseAdjustmentType) ? this.textWarehouseAdjustmentType : (senderButton.Owner.Equals(this.stripWarehouseIssue) ? this.textWarehouseIssue : (senderButton.Owner.Equals(this.stripBatchMaster) ? this.textBatchMaster : (senderButton.Owner.Equals(this.stripBatchType) ? this.textBatchType : (senderButton.Owner.Equals(this.stripFillingLine) ? this.textFillingLine : null))))))))));
                            textBoxCLEAR.Clear();
                        }
                    }
                }
            }
            catch (Exception exception) { ExceptionHandlers.ShowExceptionMessageBox(this, exception); }
        }

        private void textFILTER_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox senderTextBox = sender as TextBox;
                if (senderTextBox != null)
                {
                    DataTreeListView dataTreeListView = sender.Equals(this.textCommodityType) ? this.treeCommodityTypeID : (sender.Equals(this.textCommodity) ? this.treeCommodityID : (sender.Equals(this.textEmployee) ? this.treeEmployeeID : (sender.Equals(this.textCustomer) ? this.treeCustomerID : (sender.Equals(this.textWarehouse) ? this.treeWarehouseID : (sender.Equals(this.textWarehouseReceipt) ? this.treeWarehouseReceiptID : (sender.Equals(this.textWarehouseAdjustmentType) ? this.treeWarehouseAdjustmentTypeID : (sender.Equals(this.textWarehouseIssue) ? this.treeWarehouseIssueID : (sender.Equals(this.textBatchMaster) ? this.treeBatchMasterID : (sender.Equals(this.textBatchType) ? this.treeBatchTypeID : (sender.Equals(this.textFillingLine) ? this.treeFillingLineID : null))))))))));
                    IList<IFilterTree> dataSourceFilterTrees = (sender.Equals(this.textCommodityType) ? this.commodityTypeTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textCommodity) ? this.commodityTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textEmployee) ? this.employeeTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textCustomer) ? this.customerTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textWarehouse) ? this.warehouseTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textWarehouseReceipt) ? this.warehouseReceiptTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textWarehouseAdjustmentType) ? this.warehouseAdjustmentTypeTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textWarehouseIssue) ? this.warehouseIssueTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textBatchMaster) ? this.batchMasterTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textBatchType) ? this.batchTypeTrees.Cast<IFilterTree>().ToList() : (sender.Equals(this.textFillingLine) ? this.fillingLineTrees.Cast<IFilterTree>().ToList() : null)))))))))));

                    if (dataTreeListView != null && dataSourceFilterTrees != null)
                    {
                        if (senderTextBox.Text != null && senderTextBox.Text != "") dataTreeListView.ExpandAll();

                        OLVHelpers.ApplyFilters(dataTreeListView, senderTextBox.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries));


                        List<IFilterTree> filteredFilterTrees = dataTreeListView.FilteredObjects.Cast<IFilterTree>().ToList();
                        if (filteredFilterTrees != null && filteredFilterTrees.Count() > 0)
                        { //SEARCH FOR NOT MATCH ITEMS IN dataSourceFilterTrees FROM filteredFilterTrees  => then Deselect it.
                            List<int?> primaryIDs = filteredFilterTrees.Select(n => n.PrimaryID).ToList(); //FILTERED PrimaryID
                            IList<IFilterTree> notmatchFilterTrees = dataSourceFilterTrees.Where(w => !primaryIDs.Contains(w.PrimaryID)).ToList(); //FIND NOT MATCH ITEMS

                            foreach (IFilterTree c in notmatchFilterTrees) { c.Selected = false; } //DESELECTED NOT MATCH ITEMS


                            dataTreeListView.RefreshObject(filteredFilterTrees[0]); //NO NEED, BUT HERE: FOR SURE: WE RefreshObject THE FIRST OBJECT WHEN FINISH.
                        }
                    }
                }
            }
            catch (Exception exception) { ExceptionHandlers.ShowExceptionMessageBox(this, exception); }
        }
    }

    public class FilterParameter
    {
        public string PrimaryIDs { get; set; } //CARRY THE RETURN VALUES 

        public string CaptionDescriptions { get; set; } //CARRY THE RETURN VALUES 

        public readonly string ParameterName;
        public readonly string ParameterSSRS;

        public readonly string CaptionLabel;
        public readonly bool ShouldCaptionCode;
        public readonly bool ShouldCaptionName;

        public FilterParameter(string parameterName, string parameterSSRS, string captionLabel, bool shouldCaptionCode, bool shouldCaptionName)
        {
            this.ParameterName = parameterName;
            this.ParameterSSRS = parameterSSRS;

            this.CaptionLabel = captionLabel;
            this.ShouldCaptionCode = shouldCaptionCode;
            this.ShouldCaptionName = shouldCaptionName;
        }
    }
}
