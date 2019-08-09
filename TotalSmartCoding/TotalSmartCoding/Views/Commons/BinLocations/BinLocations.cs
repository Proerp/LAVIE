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


namespace TotalSmartCoding.Views.Commons.BinLocations
{
    public partial class BinLocations : BaseView
    {
        private CustomTabControl customTabCenter;

        private BinLocationAPIs binLocationAPIs;
        private BinLocationViewModel binLocationViewModel { get; set; }

        public BinLocations()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastBinLocationIndex;

            this.binLocationAPIs = new BinLocationAPIs(CommonNinject.Kernel.Get<IBinLocationAPIRepository>());

            this.binLocationViewModel = CommonNinject.Kernel.Get<BinLocationViewModel>();
            this.binLocationViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.binLocationViewModel;
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

                this.customTabCenter.TabPages.Add("tabCenterAA", "Bin Information      ");

                this.customTabCenter.TabPages[0].Controls.Add(this.layoutTop);
                this.customTabCenter.TabPages[0].BackColor = this.panelCenter.BackColor;
                this.layoutTop.Dock = DockStyle.Fill;

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
        
        Binding bindingContactInfo;
        Binding bindingVATCode;
        Binding bindingAttentionName;
        
        Binding bindingRemarks;

        Binding bindingWarehouseID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingCode = this.textexCode.DataBindings.Add("Text", this.binLocationViewModel, CommonExpressions.PropertyName<BinLocationDTO>(p => p.Code), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingName = this.textexName.DataBindings.Add("Text", this.binLocationViewModel, CommonExpressions.PropertyName<BinLocationDTO>(p => p.Name), true, DataSourceUpdateMode.OnPropertyChanged);
            
            this.bindingContactInfo = this.textexContactInfo.DataBindings.Add("Text", this.binLocationViewModel, CommonExpressions.PropertyName<BinLocationDTO>(p => p.ContactInfo), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingVATCode = this.textexVATCode.DataBindings.Add("Text", this.binLocationViewModel, CommonExpressions.PropertyName<BinLocationDTO>(p => p.VATCode), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingAttentionName = this.textexAttentionName.DataBindings.Add("Text", this.binLocationViewModel, CommonExpressions.PropertyName<BinLocationDTO>(p => p.AttentionName), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.binLocationViewModel, CommonExpressions.PropertyName<BinLocationDTO>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            WarehouseAPIs warehouseAPIs = new WarehouseAPIs(CommonNinject.Kernel.Get<IWarehouseAPIRepository>());
            this.combexWarehouseID.DataSource = warehouseAPIs.GetWarehouseBases();
            this.combexWarehouseID.DisplayMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.Name);
            this.combexWarehouseID.ValueMember = CommonExpressions.PropertyName<WarehouseBase>(p => p.WarehouseID);
            this.bindingWarehouseID = this.combexWarehouseID.DataBindings.Add("SelectedValue", this.binLocationViewModel, CommonExpressions.PropertyName<BinLocationViewModel>(p => p.WarehouseID), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingContactInfo.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingVATCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingAttentionName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingWarehouseID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastBinLocationIndex.AboutToCreateGroups += fastBinLocationIndex_AboutToCreateGroups;

            this.fastBinLocationIndex.ShowGroups = true;
            this.olvInActive.Renderer = new MappedImageRenderer(new Object[] { true, Resources.Void_16 });            
        }

        private void fastBinLocationIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Storage32";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString() + " Bin(s)";
                }
            }
        }

        protected override Controllers.BaseController myController
        {
            get { return new BinLocationController(CommonNinject.Kernel.Get<IBinLocationService>(), this.binLocationViewModel); }
        }

        public override void Loading()
        {
            this.fastBinLocationIndex.SetObjects(this.binLocationAPIs.GetBinLocationIndexes());
            
            base.Loading();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastBinLocationIndex.Sort(this.olvLocationName, SortOrder.Descending);
        }
    }
}
