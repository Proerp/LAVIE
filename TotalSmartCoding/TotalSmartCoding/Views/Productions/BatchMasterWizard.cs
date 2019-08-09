using System;
using System.ComponentModel;
using System.Windows.Forms;

using TotalBase;
using TotalSmartCoding.Controllers.Productions;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Productions;

namespace TotalSmartCoding.Views.Productions
{
    public partial class BatchMasterWizard : Form
    {
        private BatchMasterController batchMasterController;
        private BatchMasterViewModel batchMasterViewModel;

        Binding bindingEntryDate;
        Binding bindingCode;
        Binding bindingCommodityCode;
        Binding bindingCommodityAPICode;
        Binding bindingCommodityName;
        Binding bindingRemarks;

        public BatchMasterWizard(BatchMasterController batchMasterController, BatchMasterViewModel batchMasterViewModel)
        {
            InitializeComponent();

            this.batchMasterController = batchMasterController;
            this.batchMasterViewModel = batchMasterViewModel;
        }

        private void Wizard_Load(object sender, EventArgs e)
        {
            try
            {
                this.batchMasterViewModel.PropertyChanged += batchMasterDTO_PropertyChanged;

                this.bindingEntryDate = this.dateTimexEntryDate.DataBindings.Add("Value", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.EntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingCode = this.textexCode.DataBindings.Add("Text", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.Code), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingCommodityCode = this.textexCommodityCode.DataBindings.Add("Text", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.CommodityCode), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingCommodityAPICode = this.textexCommodityAPICode.DataBindings.Add("Text", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.CommodityAPICode), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingCommodityName = this.textexCommodityName.DataBindings.Add("Text", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.CommodityName), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingRemarks = this.textexRemarks.DataBindings.Add("Text", this.batchMasterViewModel, CommonExpressions.PropertyName<BatchMasterViewModel>(p => p.Remarks), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingCommodityCode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingCommodityAPICode.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingCommodityName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingRemarks.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.errorProviderMaster.DataSource = this.batchMasterViewModel;

                if (this.batchMasterViewModel.Editable) { this.dateTimexEntryDate.ReadOnly = false; this.textexRemarks.ReadOnly = false; this.batchMasterController.Edit(this.batchMasterViewModel.BatchMasterID); } else { this.dateTimexEntryDate.ReadOnly = !this.batchMasterViewModel.IsFV; this.textexRemarks.ReadOnly = true; }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void batchMasterDTO_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.buttonOK.Enabled = this.batchMasterViewModel.IsValid && this.checkOK.Checked;
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception) { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
        }

        private void checkOK_CheckedChanged(object sender, EventArgs e)
        {
            batchMasterDTO_PropertyChanged(sender, null);
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            try
            {
                if (sender.Equals(this.buttonOK))
                {
                    if (this.batchMasterViewModel.IsDirty && (this.batchMasterViewModel.Editable || !this.batchMasterViewModel.IsFV)) this.batchMasterController.Save();
                    if ((!this.batchMasterViewModel.IsDirty || (!this.batchMasterViewModel.Editable && this.batchMasterViewModel.IsFV)) && this.batchMasterController.AddLot(this.batchMasterViewModel.BatchMasterID, this.batchMasterViewModel.EntryDate)) this.DialogResult = DialogResult.OK;
                }

                if (sender.Equals(this.buttonESC))
                    this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }
        
    }
}
