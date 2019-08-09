using System;
using System.Windows.Forms;

using TotalBase;
using TotalDTO.Sales;
using TotalModel.Models;
using TotalSmartCoding.Controllers.APIs.Sales;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.ViewModels.Sales;

namespace TotalSmartCoding.Views.Sales.DeliveryAdvices
{
    public partial class OptionBatches : Form
    {
        private DeliveryAdviceAPIs deliveryAdviceAPIs;
        private DeliveryAdviceDetailDTO deliveryAdviceDetailDTO;

        Binding bindingBatchID;

        Binding bindingBatchEntryDate;
        Binding bindingQuantityBatchAvailable;
        Binding bindingLineVolumeBatchAvailable;

        public OptionBatches(DeliveryAdviceAPIs deliveryAdviceAPIs, DeliveryAdviceDetailDTO deliveryAdviceDetailDTO)
        {
            InitializeComponent();

            this.deliveryAdviceAPIs = deliveryAdviceAPIs;
            this.deliveryAdviceDetailDTO = deliveryAdviceDetailDTO;
        }

        private void OptionBatches_Load(object sender, EventArgs e)
        {
            try
            {
                this.combexBatchID.DataSource = this.deliveryAdviceAPIs.GetBatchAvailables(this.deliveryAdviceDetailDTO.LocationID, this.deliveryAdviceDetailDTO.DeliveryAdviceID, this.deliveryAdviceDetailDTO.CommodityID, true);
                this.combexBatchID.DisplayMember = CommonExpressions.PropertyName<BatchAvailable>(p => p.Code);
                this.combexBatchID.ValueMember = CommonExpressions.PropertyName<BatchAvailable>(p => p.BatchID);
                this.bindingBatchID = this.combexBatchID.DataBindings.Add("SelectedValue", this.deliveryAdviceDetailDTO, CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.BatchID), true, DataSourceUpdateMode.OnPropertyChanged);

                this.bindingBatchEntryDate = this.textexBatchEntryDate.DataBindings.Add("Text", this.deliveryAdviceDetailDTO, CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.BatchEntryDate), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingQuantityBatchAvailable = this.textexQuantityBatchAvailable.DataBindings.Add("Text", this.deliveryAdviceDetailDTO, CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.QuantityBatchAvailable), true, DataSourceUpdateMode.OnPropertyChanged);
                this.bindingLineVolumeBatchAvailable = this.textexLineVolumeBatchAvailable.DataBindings.Add("Text", this.deliveryAdviceDetailDTO, CommonExpressions.PropertyName<DeliveryAdviceDetailDTO>(p => p.LineVolumeBatchAvailable), true, DataSourceUpdateMode.OnPropertyChanged);

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
                    this.deliveryAdviceDetailDTO.BatchCode = batchAvailable.Code;
                    this.deliveryAdviceDetailDTO.BatchEntryDate = batchAvailable.EntryDate;
                    this.deliveryAdviceDetailDTO.QuantityBatchAvailable = (decimal) batchAvailable.QuantityAvailable;
                    this.deliveryAdviceDetailDTO.LineVolumeBatchAvailable = (decimal)batchAvailable.LineVolumeAvailable;
                }
            }
        }

        private void buttonOKESC_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}
