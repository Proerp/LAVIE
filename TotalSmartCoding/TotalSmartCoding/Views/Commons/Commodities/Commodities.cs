using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ninject;



using TotalSmartCoding.Views.Mains;



using TotalBase.Enums;
using TotalSmartCoding.Properties;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalSmartCoding.Controllers.Commons;
using TotalCore.Repositories.Commons;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;
using TotalBase;
using TotalModel.Models;
using TotalDTO.Commons;
using BrightIdeasSoftware;
using TotalSmartCoding.Libraries.StackedHeaders;


namespace TotalSmartCoding.Views.Commons.Commodities
{
    public partial class Commodities : BaseView
    {
        private CustomTabControl customTabCenter;

        private CommodityAPIs commodityAPIs;
        private CommodityViewModel commodityViewModel { get; set; }

        public Commodities()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastCommodityIndex;

            this.commodityAPIs = new CommodityAPIs(CommonNinject.Kernel.Get<ICommodityAPIRepository>());

            this.commodityViewModel = CommonNinject.Kernel.Get<CommodityViewModel>();
            this.commodityViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.commodityViewModel;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();
                
                #region TabCenter
                this.customTabCenter = new CustomTabControl();
                this.customTabCenter.DisplayStyle = TabStyle.VisualStudio;
                this.customTabCenter.Font = this.panelCenter.Font;

                this.customTabCenter.TabPages.Add("tabCenterAA", "Details           ");
                this.customTabCenter.TabPages.Add("tabCenterBB", "Remarks      ");

                this.customTabCenter.TabPages[0].Controls.Add(this.layoutTop);
                this.customTabCenter.TabPages[1].Controls.Add(this.layoutRight);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.customTabCenter.TabPages[1].BackColor = this.panelCenter.BackColor;
                this.layoutTop.Dock = DockStyle.Fill;
                this.layoutRight.Dock = DockStyle.Fill;

                this.panelCenter.Controls.Add(this.customTabCenter);
                this.customTabCenter.Dock = DockStyle.Fill;
                #endregion TabCenter

                this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].SizeType = SizeType.Absolute; this.layoutTop.ColumnStyles[this.layoutTop.ColumnCount - 1].Width = 15;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        Binding bindingCode;
        Binding bindingName;
        Binding bindingOfficialName;

        Binding bindingPackageSize;
        Binding bindingAPICode;
        Binding bindingCartonCode;
        Binding bindingFillingLineIDs;
        Binding bindingRemarks;

        Binding bindingVolume;
        Binding bindingPackageVolume;
        Binding bindingPackPerCarton;
        Binding bindingCartonPerPallet;
        Binding bindingShelflife;

        Binding bindingCommodityCategoryID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingCode = this.textexCode.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.Code), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingName = this.textexName.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingOfficialName = this.textexOfficialName.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.OfficialName), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingPackageSize = this.textexPackageSize.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.PackageSize), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingAPICode = this.textexAPICode.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.APICode), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCartonCode = this.textexCartonCode.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.CartonCode), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingFillingLineIDs = this.textexFillingLineIDs.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.FillingLineIDs), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingVolume = this.numericVolume.DataBindings.Add("Value", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.Volume), false, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingPackageVolume = this.numericPackageVolume.DataBindings.Add("Value", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.PackageVolume), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingPackPerCarton = this.numericPackPerCarton.DataBindings.Add("Value", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.PackPerCarton), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingCartonPerPallet = this.numericCartonPerPallet.DataBindings.Add("Value", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.CartonPerPallet), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingShelflife = this.numericShelflife.DataBindings.Add("Value", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.Shelflife), true, DataSourceUpdateMode.OnPropertyChanged);

            CommodityCategoryAPIs commodityCategoryAPIs = new CommodityCategoryAPIs(CommonNinject.Kernel.Get<ICommodityCategoryAPIRepository>());
            this.combexCommodityCategoryID.DataSource = commodityCategoryAPIs.GetCommodityCategoryBases();
            this.combexCommodityCategoryID.DisplayMember = CommonExpressions.PropertyName<CommodityCategoryBase>(p => p.Name);
            this.combexCommodityCategoryID.ValueMember = CommonExpressions.PropertyName<CommodityCategoryBase>(p => p.CommodityCategoryID);
            this.bindingCommodityCategoryID = this.combexCommodityCategoryID.DataBindings.Add("SelectedValue", this.commodityViewModel, CommonExpressions.PropertyName<CommodityDTO>(p => p.CommodityCategoryID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingOfficialName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingPackageSize.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingAPICode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCartonCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingFillingLineIDs.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingVolume.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingPackageVolume.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingPackPerCarton.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingCartonPerPallet.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingShelflife.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingCommodityCategoryID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastCommodityIndex.AboutToCreateGroups += fastCommodityIndex_AboutToCreateGroups;

            this.fastCommodityIndex.ShowGroups = true;
            this.olvInActive.Renderer = new MappedImageRenderer(new Object[] { true, Resources.Void_16 });            
        }

        private void fastCommodityIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Storage32";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Item(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new CommodityController(CommonNinject.Kernel.Get<ICommodityService>(), this.commodityViewModel); }
        }

        public override void Loading()
        {
            this.fastCommodityIndex.SetObjects(this.commodityAPIs.GetCommodityIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastCommodityIndex.Sort(this.olvCommodityCategoryName, SortOrder.Descending);
        }
    }
}
