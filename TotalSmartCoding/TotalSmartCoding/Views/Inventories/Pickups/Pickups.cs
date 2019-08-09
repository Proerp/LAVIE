using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BrightIdeasSoftware;

using Ninject;

using TotalSmartCoding.Views.Mains;

using TotalBase.Enums;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;

using TotalSmartCoding.Controllers.Inventories;
using TotalCore.Repositories.Inventories;
using TotalSmartCoding.Controllers.APIs.Inventories;
using TotalCore.Services.Inventories;
using TotalSmartCoding.ViewModels.Inventories;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalCore.Repositories.Commons;
using TotalBase;
using TotalModel.Models;
using TotalSmartCoding.Properties;
using TotalSmartCoding.ViewModels.Helpers;
using TotalDTO.Inventories;

namespace TotalSmartCoding.Views.Inventories.Pickups
{
    public partial class Pickups : BaseView
    {
        private CustomTabControl customTabCenter;

        private PickupAPIs pickupAPIs;
        private PickupViewModel pickupViewModel { get; set; }

        private System.Timers.Timer timerLoadPending;
        private delegate void timerLoadCallback();

        Binding checkTimerEnableVisibleBinding;

        public Pickups()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastPickupIndex;

            this.pickupAPIs = new PickupAPIs(CommonNinject.Kernel.Get<IPickupAPIRepository>());

            this.pickupViewModel = CommonNinject.Kernel.Get<PickupViewModel>();
            this.pickupViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.pickupViewModel;

            this.timerLoadPending = new System.Timers.Timer(10000);
            this.timerLoadPending.Elapsed += new System.Timers.ElapsedEventHandler(timerLoadPending_Elapsed);
            this.timerLoadPending.Enabled = true;

            this.checkTimerEnable.DataBindings.Add("Checked", this.timerLoadPending, "Enabled", true, DataSourceUpdateMode.OnPropertyChanged);
            this.checkTimerEnableVisibleBinding = this.checkTimerEnable.DataBindings.Add("Visible", this.naviPendingItems, "Collapsed", true, DataSourceUpdateMode.OnPropertyChanged);
            this.checkTimerEnableVisibleBinding.Parse += new ConvertEventHandler(checkTimerEnableVisibleBinding_Parse);
            this.checkTimerEnableVisibleBinding.Format += new ConvertEventHandler(checkTimerEnableVisibleBinding_Format);
        }

        private void Pickups_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.timerLoadPending.Enabled = false;
        }

        protected override void InitializeTabControl()
        {
            try
            {
                base.InitializeTabControl();

                this.naviIndex.Bands[0].ClientArea.Controls.Add(this.fastPickupIndex);

                this.customTabCenter = new CustomTabControl();

                this.customTabCenter.DisplayStyle = TabStyle.VisualStudio;

                this.customTabCenter.TabPages.Add("tabDetailPallets", "Pickup pallet list   ");
                this.customTabCenter.TabPages.Add("tabDescription", "Description  ");
                this.customTabCenter.TabPages.Add("tabRemarks", "Remarks    ");
                this.customTabCenter.TabPages[0].Controls.Add(this.gridexPalletDetails);
                this.customTabCenter.TabPages[1].Controls.Add(this.textexDescription);
                this.customTabCenter.TabPages[2].Controls.Add(this.textexRemarks);

                this.customTabCenter.TabPages[1].Padding = new Padding(30, 30, 30, 30);
                this.customTabCenter.TabPages[2].Padding = new Padding(30, 30, 30, 30);

                this.customTabCenter.Dock = DockStyle.Fill;
                this.gridexPalletDetails.Dock = DockStyle.Fill;
                this.textexDescription.Dock = DockStyle.Fill;
                this.textexRemarks.Dock = DockStyle.Fill;
                this.panelMaster.Controls.Add(this.customTabCenter);

                this.naviDetails.ExpandedHeight = this.naviDetails.HeaderHeight + this.textexTotalPalletCounts.Size.Height + this.textexTotalQuantity.Size.Height + this.textexTotalLineVolume.Size.Height + 5 + 4 * 10 + 6;
                this.naviDetails.Expanded = false;

                this.labelFillingLineName.Left = 78; this.labelFillingLineName.Top = 12;
                if (GlobalVariables.ConfigID == (int)GlobalVariables.FillingLine.Pickup) { ViewHelpers.SetFont(this, new Font("Calibri", 11), new Font("Calibri", 11), new Font("Calibri", 11)); ViewHelpers.SetFont(this.MdiParent, new Font("Calibri", 11), new Font("Calibri", 11), new Font("Calibri", 11)); }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        Binding bindingEntryDate;
        Binding bindingReference;
        Binding bindingFillingLineName;
        Binding bindingWarehouseCode;
        Binding bindingDescription;
        Binding bindingRemarks;

        Binding bindingTotalPalletCounts;
        Binding bindingTotalQuantity;
        Binding bindingTotalLineVolume;

        Binding bindingForkliftDriverID;
        Binding bindingStorekeeperID;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingFillingLineName = this.labelFillingLineName.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.Caption));

            this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingReference = this.textexReference.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.Reference), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingWarehouseCode = this.textexWarehouseCode.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.WarehouseName), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingDescription = this.textexDescription.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.Description), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingTotalPalletCounts = this.textexTotalPalletCounts.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.TotalPalletCounts), true, DataSourceUpdateMode.OnValidation, 0, GlobalEnums.formatQuantity);
            this.bindingTotalQuantity = this.textexTotalQuantity.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.TotalQuantity), true, DataSourceUpdateMode.OnValidation, 0, GlobalEnums.formatQuantity);
            this.bindingTotalLineVolume = this.textexTotalLineVolume.DataBindings.Add("Text", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.TotalLineVolume), true, DataSourceUpdateMode.OnValidation, 0, GlobalEnums.formatVolume);


            EmployeeAPIs employeeAPIs = new EmployeeAPIs(CommonNinject.Kernel.Get<IEmployeeAPIRepository>());

            this.combexForkliftDriverID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.pickupViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Production);
            this.combexForkliftDriverID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexForkliftDriverID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingForkliftDriverID = this.combexForkliftDriverID.DataBindings.Add("SelectedValue", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.ForkliftDriverID), true, DataSourceUpdateMode.OnPropertyChanged);


            this.combexStorekeeperID.DataSource = employeeAPIs.GetEmployeeBases(ContextAttributes.User.UserID, (int)this.pickupViewModel.NMVNTaskID, (int)GlobalEnums.RoleID.Logistic);
            this.combexStorekeeperID.DisplayMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.Name);
            this.combexStorekeeperID.ValueMember = CommonExpressions.PropertyName<EmployeeBase>(p => p.EmployeeID);
            this.bindingStorekeeperID = this.combexStorekeeperID.DataBindings.Add("SelectedValue", this.pickupViewModel, CommonExpressions.PropertyName<PickupViewModel>(p => p.StorekeeperID), true, DataSourceUpdateMode.OnPropertyChanged);


            this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingReference.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingFillingLineName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingWarehouseCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingDescription.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingTotalPalletCounts.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingTotalQuantity.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingTotalLineVolume.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.bindingForkliftDriverID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingStorekeeperID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.fastPickupIndex.AboutToCreateGroups += fastPickupIndex_AboutToCreateGroups;

            this.fastPickupIndex.ShowGroups = true;
            this.olvApproved.Renderer = new MappedImageRenderer(new Object[] { false, Resources.Placeholder16 });

            this.tableLayoutMaster.ColumnStyles[this.tableLayoutMaster.ColumnCount - 1].SizeType = SizeType.Absolute; this.tableLayoutMaster.ColumnStyles[this.tableLayoutMaster.ColumnCount - 1].Width = 10;
        }

        private void fastPickupIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
        {
            if (e.Groups != null && e.Groups.Count > 0)
            {
                foreach (OLVGroup olvGroup in e.Groups)
                {
                    olvGroup.TitleImage = "Forklift";
                    olvGroup.Subtitle = "Count: " + olvGroup.Contents.Count.ToString();
                    if ((DateTime)olvGroup.Key < DateTime.Today.AddDays(-1)) olvGroup.Collapsed = true;
                }
            }
        }

        protected override void InitializeDataGridBinding()
        {
            this.gridexPalletDetails.AutoGenerateColumns = false;
            this.gridexPalletDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.gridexPalletDetails.DataSource = this.pickupViewModel.ViewDetails;

            //StackedHeaderDecorator stackedHeaderDecorator = new StackedHeaderDecorator(this.dataGridViewDetails);
        }

        protected override void EditableModeChanged(bool editableMode)
        {
            base.EditableModeChanged(editableMode);
            this.toolStripNaviGroup.Visible = editableMode;
        }

        protected override Controllers.BaseController myController
        {
            get { return new PickupController(CommonNinject.Kernel.Get<IPickupService>(), this.pickupViewModel); }
        }

        public override void Loading()
        {
            this.fastPickupIndex.SetObjects(this.pickupAPIs.GetPickupIndexes());
            this.fastPickupIndex.Sort(this.olvEntryDate, SortOrder.Descending);

            base.Loading();
        }

        protected override void invokeEdit(int? id)
        {
            base.invokeEdit(id);

            this.checkTimerEnable.Checked = true;
            //if (this.pickupViewModel.FillingLineID != (int)GlobalVariables.FillingLine.Drum) this.checkTimerEnable.Checked = true;
            //this.olvIsSelected.Width = this.pickupViewModel.FillingLineID == (int)GlobalVariables.FillingLine.Drum ? 20 : 0;

            this.getPendingItems(true);
        }

        public override void Save(bool escapeAfterSave)
        {
            base.Save(escapeAfterSave);
            this.getPendingItems(true);
        }

        private void callAutoSave()
        {
            try
            {
                if (this.pickupViewModel.IsDirty && this.pickupViewModel.IsValid) this.Save(false);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void getPendingItems() { this.getPendingItems(false); }
        private void getPendingItems(bool forcetoLoad) //THIS MAY ALSO LOAD PENDING PALLET/ CARTON/ PACK
        {
            try
            {
                if (forcetoLoad || this.checkTimerEnable.Checked) //this.pickupViewModel.FillingLineID != (int)GlobalVariables.FillingLine.Drum || 
                {
                    this.fastPendingPallets.SetObjects(this.pickupAPIs.GetPendingPallets(this.pickupViewModel.LocationID, this.pickupViewModel.FillingLineID, this.pickupViewModel.PickupID, string.Join(",", this.pickupViewModel.ViewDetails.Where(w => w.PalletID != null).Select(d => d.PalletID)), false));
                    this.olvPendingPalletCode.Text = "Line " + this.pickupViewModel.FillingLineNickName + "   -   Pending " + this.fastPendingPallets.GetItemCount().ToString("N0") + " pallet" + (this.fastPendingPallets.GetItemCount() > 1 ? "s" : "");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        protected override DialogResult wizardMaster()
        {
            WizardMaster wizardMaster = new WizardMaster(this.pickupViewModel);
            DialogResult dialogResult = wizardMaster.ShowDialog();
            if (dialogResult == System.Windows.Forms.DialogResult.OK) this.callAutoSave();

            wizardMaster.Dispose();
            return dialogResult;
        }

        private PickupDetailDTO InitPickupDetailDTO(PendingPallet pendingPallet)
        {
            return new PickupDetailDTO()
            {
                PickupID = this.pickupViewModel.PickupID,
                WarehouseID = (int)this.pickupViewModel.WarehouseID,

                BatchID = pendingPallet.BatchID,
                BatchEntryDate = pendingPallet.BatchEntryDate,

                PalletID = pendingPallet.PalletID,
                PalletCode = pendingPallet.Code,
                PalletEntryDate = pendingPallet.EntryDate,

                CommodityID = pendingPallet.CommodityID,
                CommodityCode = pendingPallet.CommodityCode,
                CommodityName = pendingPallet.CommodityName,

                PackCounts = pendingPallet.PackCounts,
                CartonCounts = pendingPallet.CartonCounts,
                PalletCounts = pendingPallet.PalletCounts,

                Quantity = (decimal)pendingPallet.QuantityRemains,
                LineVolume = (decimal)pendingPallet.LineVolumeRemains
            };
        }

        private void fastPendingPallets_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (this.EditableMode && this.pickupViewModel.Editable && this.pickupViewModel.IsValid && !this.pickupViewModel.IsDirty)
                {
                    this.timerLoadPending.Enabled = false;
                    PendingPallet pendingPallet = (PendingPallet)this.fastPendingPallets.SelectedObject;
                    if (pendingPallet != null)
                    {
                        PickupDetailDTO pickupDetailDTO = this.InitPickupDetailDTO(pendingPallet);
                        WizardDetail wizardDetail = new WizardDetail(pickupDetailDTO);
                        TabletMDI tabletMDI = new TabletMDI(wizardDetail);

                        if (tabletMDI.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            if (this.fastPendingPallets.CheckedObjects.Count > 0) //this.pickupViewModel.FillingLineID == (int)GlobalVariables.FillingLine.Drum && 
                            {
                                this.pickupViewModel.ViewDetails.RaiseListChangedEvents = false;
                                foreach (var checkedObjects in this.fastPendingPallets.CheckedObjects)
                                {
                                    PickupDetailDTO pDTO = this.InitPickupDetailDTO((PendingPallet)checkedObjects);
                                    pDTO.BinLocationID = pickupDetailDTO.BinLocationID;
                                    pDTO.BinLocationCode = pickupDetailDTO.BinLocationCode;
                                    this.pickupViewModel.ViewDetails.Add(pDTO);
                                }
                                this.pickupViewModel.ViewDetails.RaiseListChangedEvents = true;
                                this.pickupViewModel.ViewDetails.ResetBindings();
                            }
                            else
                                this.pickupViewModel.ViewDetails.Add(pickupDetailDTO);

                            this.callAutoSave();
                        }

                        wizardDetail.Dispose(); tabletMDI.Dispose();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
            finally
            {
                if (!this.pickupViewModel.ViewDetails.RaiseListChangedEvents)
                {
                    this.pickupViewModel.ViewDetails.RaiseListChangedEvents = true;
                    this.pickupViewModel.ViewDetails.ResetBindings();
                }
                this.timerLoadPending.Enabled = true;
            }
        }

        private void buttonRemoveDetailItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.EditableMode && this.pickupViewModel.Editable)
                {
                    PickupDetailDTO pickupDetailDTO = this.gridexPalletDetails.CurrentRow.DataBoundItem as PickupDetailDTO;
                    if (pickupDetailDTO != null && CustomMsgBox.Show(this, "Xác nhận xóa pallet:" + (char)13 + (char)13 + pickupDetailDTO.PalletCode + (char)13 + (char)13 + "Tại vi trí: " + (char)13 + (char)13 + pickupDetailDTO.BinLocationCode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.pickupViewModel.ViewDetails.Remove(pickupDetailDTO);
                        this.callAutoSave();
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void gridexPalletDetails_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
        {
            this.callAutoSave();
        }

        private void timerLoadPending_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                timerLoadCallback loadPendingItemsCallback = new timerLoadCallback(getPendingItems);
                this.Invoke(loadPendingItemsCallback);
            }
            catch { }
        }

        private void checkTimerEnableVisibleBinding_Parse(object sender, ConvertEventArgs e)
        {
            e.Value = !(bool)e.Value;
        }

        private void checkTimerEnableVisibleBinding_Format(object sender, ConvertEventArgs e)
        {
            e.Value = !(bool)e.Value;
        }

        protected override PrintViewModel InitPrintViewModel()
        {
            PrintViewModel printViewModel = base.InitPrintViewModel();
            printViewModel.ReportPath = "PickupSheet";
            printViewModel.ReportParameters.Add(new Microsoft.Reporting.WinForms.ReportParameter("PickupID", this.pickupViewModel.PickupID.ToString()));
            return printViewModel;
        }




    }
}
