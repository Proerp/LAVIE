using System;
using System.Windows.Forms;

using Ninject;

using TotalBase;
using TotalCore.Repositories.Productions;
using TotalDTO.Sales;
using TotalModel.Models;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Sales;
using TotalDTO.Helpers.Interfaces;

namespace TotalSmartCoding.Views.Sales
{
    public partial class OptionBatches : Form
    {
        private IBatchQuantityDetailDTO batchQuantityDetailDTO;

        Binding bindingBatchID;

        Binding bindingBatchEntryDate;
        Binding bindingQuantityBatchAvailable;
        Binding bindingLineVolumeBatchAvailable;

        public OptionBatches(IBatchQuantityDetailDTO batchQuantityDetailDTO)
        {
            InitializeComponent();

            this.batchQuantityDetailDTO = batchQuantityDetailDTO;
        }

        private void OptionBatches_Load(object sender, EventArgs e)
        {
            try
            {
                BatchAPIs batchAPIs = new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>());

                this.combexBatchID.DataSource = batchAPIs.GetBatchAvailables(this.batchQuantityDetailDTO.LocationID, this.batchQuantityDetailDTO.DeliveryAdviceID, this.batchQuantityDetailDTO.TransferOrderID, this.batchQuantityDetailDTO.CommodityID, true);
                this.combexBatchID.DisplayMember = CommonExpressions.PropertyName<BatchAvailable>(p => p.Code);
                this.combexBatchID.ValueMember = CommonExpressions.PropertyName<BatchAvailable>(p => p.BatchID);
                this.bindingBatchID = this.combexBatchID.DataBindings.Add("SelectedValue", this.batchQuantityDetailDTO, CommonExpressions.PropertyName<IBatchQuantityDetailDTO>(p => p.BatchID), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingBatchEntryDate = this.textexBatchEntryDate.DataBindings.Add("Text", this.batchQuantityDetailDTO, CommonExpressions.PropertyName<IBatchQuantityDetailDTO>(p => p.BatchEntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingQuantityBatchAvailable = this.textexQuantityBatchAvailable.DataBindings.Add("Text", this.batchQuantityDetailDTO, CommonExpressions.PropertyName<IBatchQuantityDetailDTO>(p => p.QuantityBatchAvailable), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingLineVolumeBatchAvailable = this.textexLineVolumeBatchAvailable.DataBindings.Add("Text", this.batchQuantityDetailDTO, CommonExpressions.PropertyName<IBatchQuantityDetailDTO>(p => p.LineVolumeBatchAvailable), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingBatchID.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

                this.bindingBatchEntryDate.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingQuantityBatchAvailable.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
                this.bindingLineVolumeBatchAvailable.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void CommonControl_BindingComplete(object sender, BindingCompleteEventArgs e)
        {
            if (e.BindingCompleteState == BindingCompleteState.Exception) { ExceptionHandlers.ShowExceptionMessageBox(this, e.ErrorText); e.Cancel = true; }
            if (sender.Equals(this.bindingBatchID))
            {
                if (this.combexBatchID.SelectedItem != null)
                {
                    BatchAvailable batchAvailable = (BatchAvailable)this.combexBatchID.SelectedItem;
                    this.batchQuantityDetailDTO.BatchCode = batchAvailable.Code;
                    this.batchQuantityDetailDTO.BatchEntryDate = batchAvailable.EntryDate;
                    this.batchQuantityDetailDTO.QuantityBatchAvailable = (decimal) batchAvailable.QuantityAvailable;
                    this.batchQuantityDetailDTO.LineVolumeBatchAvailable = (decimal)batchAvailable.LineVolumeAvailable;
                }
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
