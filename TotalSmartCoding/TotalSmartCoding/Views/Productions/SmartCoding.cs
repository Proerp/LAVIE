using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Threading;

using Equin.ApplicationFramework;

using Ninject;
using AutoMapper;

using TotalBase;
using TotalBase.Enums;
using TotalCore.Repositories.Commons;
using TotalCore.Repositories.Productions;
using TotalCore.Services.Productions;
using TotalModel.Models;
using TotalDTO.Productions;

using TotalSmartCoding.Controllers.Productions;
using TotalSmartCoding.Controllers.APIs.Commons;
using TotalSmartCoding.Controllers.APIs.Productions;
using TotalSmartCoding.Libraries;
using TotalSmartCoding.Libraries.Helpers;
using TotalSmartCoding.Views.Commons;
using TotalSmartCoding.Views.Mains;
using System.Collections.Generic;
using TotalSmartCoding.ViewModels.Productions;
//using System.Diagnostics;


namespace TotalSmartCoding.Views.Productions
{
    public partial class SmartCoding : Form, IToolstripMerge
    {
        #region Declaration

        private ScannerAPIs scannerAPIs;

        private readonly FillingData fillingData;

        private IBatchService batchService;

        private PrinterController digitController;
        private PrinterController packController;
        private PrinterController cartonController;
        private PrinterController palletController;

        private ScannerController scannerController;



        private Thread digitThread;
        private Thread packThread;
        private Thread cartonThread;
        private Thread palletThread;

        private Thread scannerThread;

        private Thread backupDataThread;



        delegate void SetTextCallback(string text);
        delegate void propertyChangedThread(object sender, PropertyChangedEventArgs e);

        private IList<string> voidTypeNames;

        #endregion Declaration

        #region Contructor & Implement Interface

        public SmartCoding()
        {
            InitializeComponent();

            try
            {
                this.fillingData = new FillingData();

                this.Initialize();

                this.scannerAPIs = new ScannerAPIs(CommonNinject.Kernel.Get<IPackRepository>(), CommonNinject.Kernel.Get<ICartonRepository>(), CommonNinject.Kernel.Get<IPalletRepository>());

                this.batchService = CommonNinject.Kernel.Get<IBatchService>();//ALL PrinterController MUST SHARE THE SAME IBatchService, BECAUSE WE NEED TO LOCK IBatchService IN ORDER TO CORRECTED UPDATE DATA BY IBatchService

                digitController = new PrinterController(this.batchService, this.fillingData, GlobalVariables.PrinterName.DigitInkjet);
                packController = new PrinterController(this.batchService, this.fillingData, GlobalVariables.PrinterName.PackInkjet);
                cartonController = new PrinterController(this.batchService, this.fillingData, GlobalVariables.PrinterName.CartonInkjet);
                palletController = new PrinterController(this.batchService, this.fillingData, GlobalVariables.PrinterName.PalletLabel);

                this.scannerController = new ScannerController(this.fillingData);

                digitController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
                packController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
                cartonController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);
                palletController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);

                scannerController.PropertyChanged += new PropertyChangedEventHandler(controller_PropertyChanged);


                this.textBoxFillingLineName.TextBox.DataBindings.Add("Text", this.fillingData, "FillingLineName");
                this.textBoxSettingDate.TextBox.DataBindings.Add("Text", this.fillingData, "SettingDateShortDateFormat");
                this.textBoxCommodityCode.TextBox.DataBindings.Add("Text", this.fillingData, "CommodityCode");
                this.textBoxCommodityAPICode.TextBox.DataBindings.Add("Text", this.fillingData, "CommodityAPICode");
                this.textBoxCommodityOfficialCode.TextBox.DataBindings.Add("Text", this.fillingData, "CommodityOfficialCode");
                this.textBoxBatchCode.TextBox.DataBindings.Add("Text", this.fillingData, "BatchCode");
                this.textNextDigitNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextDigitNo");
                this.textNextPackNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextPackNo");
                this.textNextCartonNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextCartonNo");
                this.textNextPalletNo.TextBox.DataBindings.Add("Text", this.fillingData, "NextPalletNo");

                this.dgvRepacks.AutoGenerateColumns = false;
                this.dgvRepacks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                this.dgvRepacks.DataSource = this.fillingData.BatchRepackViews;


                this.comboBoxEmptyCarton.ComboBox.Items.AddRange(new string[] { "Ignore empty carton", "Keep empty carton" });
                this.comboBoxEmptyCarton.ComboBox.SelectedIndex = GlobalVariables.IgnoreEmptyCarton ? 0 : 1;

                this.comboBoxSendToZebra.ComboBox.Items.AddRange(new string[] { "Stop print label", "Print new label" });
                this.comboBoxSendToZebra.ComboBox.SelectedIndex = GlobalEnums.SendToZebra ? 1 : 0;
                this.comboBoxSendToZebra.Visible = this.fillingData.FillingLineID == GlobalVariables.FillingLine.Drum;
                this.separatorSendToZebra.Visible = this.fillingData.FillingLineID == GlobalVariables.FillingLine.Drum;
                this.buttonSendToZebra.Visible = this.fillingData.FillingLineID == GlobalVariables.FillingLine.Drum;

                this.buttonCartonNoreadNow.Visible = GlobalEnums.OnTestScanner;
                this.buttonPalletReceivedNow.Visible = GlobalEnums.OnTestScanner;




                this.dgvPackQueue.RowTemplate.Height = 216; this.dgvPacksetQueue.RowTemplate.Height = 216;


                if (!fillingData.HasPack) { this.labelNextDigitNo.Visible = false; this.textNextDigitNo.Visible = false; this.labelNextPackNo.Visible = false; this.textNextPackNo.Visible = false; this.dgvCartonPendingQueue.RowTemplate.Height = 280; this.dgvCartonQueue.RowTemplate.Height = 280; this.dgvCartonsetQueue.RowTemplate.Height = 280; this.labelLEDPack.Visible = false; this.labelLEDCartonIgnore.Visible = false; }
                if (!fillingData.HasCarton) { this.labelNextCartonNo.Visible = false; this.textNextCartonNo.Visible = false; this.dgvPalletQueue.RowTemplate.Height = 280; this.dgvPalletPickupQueue.RowTemplate.Height = 280; this.labelLEDCarton.Visible = false; this.labelLEDCartonPending.Visible = false; }




                this.labelNextDigitNo.Visible = false; this.textNextDigitNo.Visible = false;

                VoidTypeAPIs voidTypeAPIs = new VoidTypeAPIs(CommonNinject.Kernel.Get<IVoidTypeRepository>());
                this.voidTypeNames = voidTypeAPIs.GetVoidTypeNames();


            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        public void Initialize()
        { this.Initialize(null, null); }
        public void Initialize(BatchIndex recartonBatchIndex, BatchRepack recartonbatchRepack)
        {
            try
            {
                BatchIndex batchIndex;
                if (recartonBatchIndex != null)
                    batchIndex = recartonBatchIndex;
                else
                {
                    BatchAPIs batchAPIs = new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>());
                    batchIndex = batchAPIs.GetActiveBatchIndex();
                }

                if (batchIndex != null)
                {
                    Mapper.Map<BatchIndex, FillingData>(batchIndex, this.fillingData);
                    if (this.scannerController != null) this.scannerController.InitializePallet(); //WHEN USER PRESS BUTTON buttonBatches => TO OPEN Batches VIEW => THEN APPLY NEW BATCH FOR PRODUCTION

                    this.Text = this.fillingData.CommodityName + " [Carton Code: " + this.fillingData.CommodityCartonCode + "] " + "     [Pack per Carton: " + this.fillingData.PackPerCarton + ". Carton per Pallet: " + this.fillingData.CartonPerPallet + "]"; this.labelCommodityName.Text = "";

                    this.InitializeRepack(this.AnyLoopRoutine(), recartonbatchRepack);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public void InitializeRepack(bool notPrintedOnly)
        { this.InitializeRepack(notPrintedOnly, null); }
        public void InitializeRepack(bool notPrintedOnly, BatchRepack recartonbatchRepack)
        {
            try
            {
                this.panelRepack.Visible = this.fillingData.BatchTypeID == (int)GlobalEnums.BatchTypeID.Repack;

                this.fillingData.BatchRepacks.RaiseListChangedEvents = false;
                this.fillingData.BatchRepacks.Clear();
                this.fillingData.ReprintCarton = false;

                if (this.fillingData.BatchTypeID == (int)GlobalEnums.BatchTypeID.Repack)
                {
                    if (recartonbatchRepack != null)
                    {
                        BatchRepackDTO batchRepackDTO = Mapper.Map<BatchRepack, BatchRepackDTO>(recartonbatchRepack);
                        batchRepackDTO.LineIndex = 1;
                        this.fillingData.BatchRepacks.Add(batchRepackDTO);
                        this.fillingData.ReprintCarton = true;
                    }
                    else
                    {
                        BatchAPIs batchAPIs = new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>());
                        List<BatchRepack> batchRepacks = batchAPIs.GetBatchRepacks(this.fillingData.BatchID, notPrintedOnly);
                        if (batchRepacks.Count > 0)
                        {
                            int lineNo = 0;
                            batchRepacks.Each(batchRepack =>
                            {
                                BatchRepackDTO batchRepackDTO = Mapper.Map<BatchRepack, BatchRepackDTO>(batchRepack);
                                batchRepackDTO.LineIndex = ++lineNo;
                                this.fillingData.BatchRepacks.Add(batchRepackDTO);
                            });
                        }
                    }
                }

                this.fillingData.BatchRepacks.RaiseListChangedEvents = true;
                this.fillingData.BatchRepacks.ResetBindings();

                this.buttonRepackImport.Visible = !this.fillingData.ReprintCarton;
                this.buttonRepackRemove.Visible = !this.fillingData.ReprintCarton;
                this.buttonRepackReprint.Visible = !this.fillingData.ReprintCarton;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private void SmartCoding_Load(object sender, EventArgs e)
        {
            try
            {
                this.scannerController.Initialize();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void splitContainerMaster_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                this.splitContainerPack.SplitterDistance = this.SplitterDistanceQuality();
                this.splitContainerCarton.SplitterDistance = this.SplitterContainerCarton();
                this.splitContainerPallet.SplitterDistance = this.SplitterContainerPallet();



                this.splitDigit.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
                this.splitPack.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
                this.splitCarton.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
                this.splitPallet.SplitterDistance = this.splitContainerMaster.Panel1.Width / 5;
            }
            catch { }
        }

        private void SmartCoding_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (digitThread != null && digitThread.IsAlive) { e.Cancel = true; return; }
                if (packThread != null && packThread.IsAlive) { e.Cancel = true; return; }
                if (cartonThread != null && cartonThread.IsAlive) { e.Cancel = true; return; }
                if (palletThread != null && palletThread.IsAlive) { e.Cancel = true; return; }

                if (scannerThread != null && scannerThread.IsAlive) { e.Cancel = true; return; }

                if (backupDataThread != null && backupDataThread.IsAlive) { e.Cancel = true; return; }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void comboBoxEmptyCarton_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalVariables.IgnoreEmptyCarton = this.comboBoxEmptyCarton.ComboBox.SelectedIndex == 0;
                GlobalRegistry.Write("IgnoreEmptyCarton", GlobalVariables.IgnoreEmptyCarton ? "1" : "0");
            }
            catch
            { }
        }


        private void comboBoxSendToZebra_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GlobalEnums.SendToZebra = this.comboBoxSendToZebra.ComboBox.SelectedIndex == 1;
            }
            catch
            { }
        }


        public ToolStrip toolstripChild { get { return this.toolStripChildForm; } }

        private int SplitterDistanceQuality()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case GlobalVariables.FillingLine.Smallpack:
                    return 280; //346 
                case GlobalVariables.FillingLine.Pail:
                    return 280; //0
                case GlobalVariables.FillingLine.Drum:
                    return 0; //86;
                default:
                    return 1;
            }
        }

        private int SplitterContainerCarton()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case GlobalVariables.FillingLine.Smallpack:
                    return 225;
                case GlobalVariables.FillingLine.Pail:
                    return 225; //458
                case GlobalVariables.FillingLine.Drum:
                    return 347; //86;
                default:
                    return 1;
            }
        }

        private int SplitterContainerPallet()
        {
            switch (GlobalVariables.FillingLineID)
            {
                case GlobalVariables.FillingLine.Smallpack:
                    return 111;
                case GlobalVariables.FillingLine.Pail:
                    return 111; //344
                case GlobalVariables.FillingLine.Drum:
                    return 0; //86;
                default:
                    return 1;
            }
        }

        #endregion Contructor & Implement Interface

        #region Toolstrip bar

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.cutStatusBox(true);

                if (backupDataThread == null || !backupDataThread.IsAlive)
                {
                    if (digitThread != null && digitThread.IsAlive) digitThread.Abort();
                    digitThread = new Thread(new ThreadStart(digitController.ThreadRoutine));

                    if (packThread != null && packThread.IsAlive) packThread.Abort();
                    packThread = new Thread(new ThreadStart(packController.ThreadRoutine));

                    if (cartonThread != null && cartonThread.IsAlive) cartonThread.Abort();
                    cartonThread = new Thread(new ThreadStart(cartonController.ThreadRoutine));

                    if (palletThread != null && palletThread.IsAlive) palletThread.Abort();
                    palletThread = new Thread(new ThreadStart(palletController.ThreadRoutine));

                    if (scannerThread != null && scannerThread.IsAlive) scannerThread.Abort();
                    scannerThread = new Thread(new ThreadStart(scannerController.ThreadRoutine));


                    digitThread.Start();
                    packThread.Start();
                    cartonThread.Start();
                    palletThread.Start();
                    scannerThread.Start();

                    if (!this.fillingData.ReprintCarton)
                        this.InitializeRepack(true);
                    else
                        if (this.fillingData.BatchRepacks.Count > 0)
                            this.fillingData.BatchRepacks[0].PrintedTimes = 0;
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonDisconnect_Click(object sender, EventArgs e)
        {
            try
            {
                digitController.LoopRoutine = false;
                packController.LoopRoutine = false;
                cartonController.LoopRoutine = false;
                palletController.LoopRoutine = false;
                scannerController.LoopRoutine = false;

                this.setToolStripActive();

                if (GlobalEnums.OnTestRepackWithoutScanner)
                {
                    GlobalEnums.OnTestRepackWithoutScanner = false;
                    this.Initialize();
                }
                else
                    if (!this.fillingData.ReprintCarton) this.InitializeRepack(false);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                this.cutStatusBox(true);

                this.digitController.StartPrint();
                this.packController.StartPrint();
                this.cartonController.StartPrint();
                this.palletController.StartPrint();
                this.scannerController.StartScanner();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }

        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomMsgBox.Show(this, "Phần mềm đang kết nối hệ thống máy in và đầu đọc mã vạch." + (char)13 + (char)13 + "Bạn có muốn dừng phần mềm không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    if (CustomMsgBox.Show(this, "Bạn thật sự muốn dừng phần mềm?" + (char)13 + (char)13 + "Vui lòng nhấn Yes để dừng phần mềm ngay lập tức.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    {
                        this.StopPrint();
                        this.scannerController.StopScanner();
                    }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void StopPrint()
        {
            this.StopPrint(true, true, true, true);
        }

        private void StopPrint(bool stopDigitInkjet, bool stopPackInkjet, bool stopCartonInkjet, bool stopPalletLabel)
        {
            if (stopDigitInkjet) this.digitController.StopPrint();
            if (stopPackInkjet) this.packController.StopPrint();
            if (stopCartonInkjet) this.cartonController.StopPrint();
            if (stopPalletLabel) this.palletController.StopPrint();
        }


        private void buttonBatches_Click(object sender, EventArgs e)
        {
            try
            {
                //Debug.Print(this.splitContainerPack.SplitterDistance.ToString());
                //Debug.Print(this.splitContainerCarton.SplitterDistance.ToString());
                //Debug.Print(this.splitContainerPallet.SplitterDistance.ToString());

                MasterMDI masterMDI = new MasterMDI(GlobalEnums.NmvnTaskID.Batch, new Batches(this, this.scannerController.AllQueueEmpty));

                masterMDI.ShowDialog();
                masterMDI.Dispose();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonReprint_Click(object sender, EventArgs e)
        {
            this.scannerController.Reprint();
        }


        private void buttonPalletReceivedNow_Click(object sender, EventArgs e)
        {
            GlobalEnums.OnTestPalletReceivedNow = true;
        }


        private void buttonCartonNoreadNow_Click(object sender, EventArgs e)
        {
            GlobalEnums.OnTestCartonNoreadNow = true;
        }

        private void timerEverySecond_Tick(object sender, EventArgs e)
        {
            try
            {
                //this.textBoxSettingDate.TextBox.Text = DateTime.Now.ToString("dd/MM/yy");
                //if (this.fillingData != null)
                //{
                //if (this.fillingData.SettingMonthID != 1) //GlobalStaticFunction.DateToContinuosMonth()
                //{
                //    this.toolStripButtonWarningNewMonth.Visible = !this.toolStripButtonWarningNewMonth.Visible; this.toolStripLabelWarningNewMonth.Visible = !this.toolStripLabelWarningNewMonth.Visible;
                //}
                //else
                //{
                //    this.toolStripButtonWarningNewMonth.Visible = false; this.toolStripLabelWarningNewMonth.Visible = false;
                //}
                //}
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Toolstrip bar

        #region Handler

        private void controller_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                propertyChangedThread propertyChangedDelegate = new propertyChangedThread(propertyChangedHandler);
                this.Invoke(propertyChangedDelegate, new object[] { sender, e });
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void propertyChangedHandler(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                this.setToolStripActive();

                if (e.PropertyName == "RepackPrintedIndex")
                {
                    this.fillingData.BatchRepacks.RaiseListChangedEvents = false;

                    int repackPrintedIndex = sender.Equals(this.packController) ? this.packController.RepackPrintedIndex : (sender.Equals(this.cartonController) ? this.cartonController.RepackPrintedIndex : 0);

                    this.fillingData.BatchRepacks.Where(w => w.LineIndex <= repackPrintedIndex && w.PrintedTimes == 0).Each(batchRepackDTO =>
                    {
                        batchRepackDTO.PrintedTimes = 1;
                    });

                    this.fillingData.BatchRepacks.RaiseListChangedEvents = true;
                    this.fillingData.BatchRepacks.ResetBindings();

                    return;
                }

                if (sender.Equals(this.digitController))
                {
                    if (e.PropertyName == "MainStatus") { this.digitStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.digitController.MainStatus + "\r\n" + this.digitStatusbox.Text; this.cutStatusBox(false); return; }
                    if (e.PropertyName == "LedStatus") { this.digitLEDGreen.Enabled = this.digitController.LedGreenOn; this.digitLEDAmber.Enabled = this.digitController.LedAmberOn; this.digitLEDRed.Enabled = this.digitController.LedRedOn; if (this.digitController.LedRedOn) this.StopPrint(true, true, false, false); return; }

                    if (e.PropertyName == "NextDigitNo") { this.fillingData.NextDigitNo = this.packController.NextDigitNo; return; }
                }
                else if (sender.Equals(this.packController))
                {
                    if (e.PropertyName == "MainStatus") { this.packStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.packController.MainStatus + "\r\n" + this.packStatusbox.Text; this.cutStatusBox(false); return; }
                    if (e.PropertyName == "LedStatus") { this.packLEDGreen.Enabled = this.packController.LedGreenOn; this.packLEDAmber.Enabled = this.packController.LedAmberOn; this.packLEDRed.Enabled = this.packController.LedRedOn; if (this.packController.LedRedOn) this.StopPrint(true, true, false, false); return; }

                    if (e.PropertyName == "NextPackNo") { this.fillingData.NextPackNo = this.packController.NextPackNo; return; }
                }
                else if (sender.Equals(this.cartonController))
                {
                    if (e.PropertyName == "MainStatus") { this.cartonStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.cartonController.MainStatus + "\r\n" + this.cartonStatusbox.Text; this.cutStatusBox(false); return; }
                    if (e.PropertyName == "LedStatus") { this.cartonLEDGreen.Enabled = this.cartonController.LedGreenOn; this.cartonLEDAmber.Enabled = this.cartonController.LedAmberOn; this.cartonLEDRed.Enabled = this.cartonController.LedRedOn; if (this.cartonController.LedRedOn) this.StopPrint(false, false, true, false); return; }

                    if (e.PropertyName == "NextCartonNo") { this.fillingData.NextCartonNo = this.cartonController.NextCartonNo; return; }
                }

                else if (sender.Equals(this.palletController))
                {
                    if (e.PropertyName == "MainStatus") { this.palletStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.palletController.MainStatus + "\r\n" + this.palletStatusbox.Text; this.cutStatusBox(false); return; }
                    if (e.PropertyName == "LedStatus") { this.palletLEDGreen.Enabled = this.palletController.LedGreenOn; this.palletLEDAmber.Enabled = this.palletController.LedAmberOn; this.palletLEDRed.Enabled = this.palletController.LedRedOn; return; }

                    if (e.PropertyName == "NextPalletNo") { this.fillingData.NextPalletNo = this.palletController.NextPalletNo; return; }
                }

                else if (sender.Equals(this.scannerController))
                {
                    if (e.PropertyName == "MainStatus") { this.scannerStatusbox.Text = "[" + DateTime.Now.ToString("hh:mm:ss") + "] " + this.scannerController.MainStatus + "\r\n" + this.scannerStatusbox.Text; this.cutStatusBox(false); return; }
                    if (e.PropertyName == "LedStatus") { this.scannerLEDGreen.Enabled = this.scannerController.LedGreenOn; this.scannerLEDAmber.Enabled = this.scannerController.LedAmberOn; this.scannerLEDRed.Enabled = this.scannerController.LedRedOn; if (this.scannerController.LedRedOn) this.StopPrint(); return; }

                    if (e.PropertyName == "LedMCU") { this.toolStripMCUQuanlity.Enabled = this.scannerController.LedMCUQualityOn; this.toolStripMCUMatching.Enabled = this.scannerController.LedMCUMatchingOn; this.toolStripMCUCarton.Enabled = this.scannerController.LedMCUCartonOn; return; }



                    if (e.PropertyName == "PackQueue")
                    {
                        int currentRowIndex = -1; int currentColumnIndex = -1;
                        if (this.dgvPackQueue.CurrentCell != null) { currentRowIndex = this.dgvPackQueue.CurrentCell.RowIndex; currentColumnIndex = this.dgvPackQueue.CurrentCell.ColumnIndex; }

                        this.dgvPackQueue.DataSource = this.scannerController.GetPackQueue();

                        if (currentRowIndex >= 0 && currentRowIndex < this.dgvPackQueue.Rows.Count && currentColumnIndex >= 0 && currentColumnIndex < this.dgvPackQueue.ColumnCount) this.dgvPackQueue.CurrentCell = this.dgvPackQueue[currentColumnIndex, currentRowIndex]; //Keep current cell

                        this.buttonDeleteAllPack.Text = "[" + this.scannerController.PackQueueCount.ToString("N0") + "]";
                        this.buttonPackQueueCount.Text = this.scannerController.NextPackQueueDescription;
                        this.labelLEDPack.Text = this.scannerController.PackQueueCount.ToString("N0");
                    }

                    if (e.PropertyName == "PacksetQueue") { this.dgvPacksetQueue.DataSource = this.scannerController.GetPacksetQueue(); this.buttonToggleLastPackset.Text = "[" + this.scannerController.PacksetQueueCount.ToString("N0") + "]"; }

                    if (e.PropertyName == "PackIgnoreCount") { this.labelLEDPackIgnore.Text = this.scannerController.PackIgnoreCount != 0 ? this.scannerController.PackIgnoreCount.ToString("N0") : "   "; }
                    if (e.PropertyName == "CartonIgnoreCount") { this.labelLEDCartonIgnore.Text = this.scannerController.CartonIgnoreCount != 0 ? this.scannerController.CartonIgnoreCount.ToString("N0") : "   "; }

                    if (e.PropertyName == "CartonPendingQueue")
                    {
                        this.dgvCartonPendingQueue.DataSource = this.scannerController.GetCartonPendingQueue();
                        if (this.dgvCartonPendingQueue.Rows.Count > 1) this.dgvCartonPendingQueue.CurrentCell = this.dgvCartonPendingQueue.Rows[0].Cells[0];

                        this.buttonCartonPendingQueueCount.Text = "[" + this.scannerController.CartonPendingQueueCount.ToString("N0") + "]";
                        this.labelLEDCartonPending.Text = this.scannerController.CartonPendingQueueCount != 0 ? this.scannerController.CartonPendingQueueCount.ToString("N0") : "    ";
                    }

                    if (e.PropertyName == "CartonQueue")
                    {
                        this.dgvCartonQueue.DataSource = this.scannerController.GetCartonQueue();
                        if (this.dgvCartonQueue.Rows.Count > 1) this.dgvCartonQueue.CurrentCell = this.dgvCartonQueue.Rows[0].Cells[0];

                        this.buttonCartonQueueCount.Text = "[" + this.scannerController.CartonQueueCount.ToString("N0") + "]";
                        this.labelLEDCarton.Text = this.scannerController.CartonQueueCount.ToString("N0");
                    }

                    if (e.PropertyName == "CartonsetQueue") { this.dgvCartonsetQueue.DataSource = this.scannerController.GetCartonsetQueue(); this.buttonCartonsetQueueCount.Text = "[" + this.scannerController.CartonsetQueueCount.ToString("N0") + "]"; }

                    if (e.PropertyName == "PalletQueue")
                    {
                        this.dgvPalletQueue.DataSource = this.scannerController.GetPalletQueue();
                        this.buttonPalletQueueCount.Text = "[" + this.scannerController.PalletQueueCount.ToString("N0") + "]";
                        this.labelLEDPallet.Text = this.scannerController.PalletQueueCount.ToString("N0");
                    }

                    if (e.PropertyName == "PalletPickupQueue")
                    {
                        this.dgvPalletPickupQueue.DataSource = this.scannerController.GetPalletPickupQueue();
                        this.buttonPalletPickupQueueCount.Text = "[" + this.scannerController.PalletPickupQueueCount.ToString("N0") + "]";
                    }
                }

            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private bool AnyLoopRoutine()
        {
            if (digitController != null && packController != null && cartonController != null && palletController != null && scannerController.LoopRoutine != null)
                return digitController.LoopRoutine | packController.LoopRoutine | cartonController.LoopRoutine | palletController.LoopRoutine | scannerController.LoopRoutine;
            else
                return false;
        }

        private void setToolStripActive()
        {
            bool anyLoopRoutine = this.AnyLoopRoutine();
            bool allLoopRoutine = digitController.LoopRoutine && packController.LoopRoutine && cartonController.LoopRoutine && palletController.LoopRoutine && scannerController.LoopRoutine;

            bool anyOnPrinting = digitController.OnPrinting | packController.OnPrinting | cartonController.OnPrinting | palletController.OnPrinting | scannerController.OnScanning;
            //bool allOnPrinting = digitInkjetDominoPrinter.OnPrinting && barcodeInkjetDominoPrinter.OnPrinting && cartonInkjetDominoPrinter.OnPrinting  && palletInkjetDominoPrinter.OnPrinting && barcodeScannerMCU.OnPrinting;

            bool allLedGreenOn = digitController.LedGreenOn && packController.LedGreenOn && cartonController.LedGreenOn && palletController.LedGreenOn && scannerController.LedGreenOn;

            this.buttonConnect.Enabled = !anyLoopRoutine;
            this.buttonDisconnect.Enabled = anyLoopRoutine && !anyOnPrinting;
            this.buttonStart.Enabled = allLoopRoutine && !anyOnPrinting && allLedGreenOn;
            this.buttonStop.Enabled = anyOnPrinting;

            this.buttonReprint.Enabled = this.palletController.LedGreenOn && palletController.OnPrinting;

            this.buttonBatches.Enabled = !anyLoopRoutine;
            this.buttonRepackImport.Enabled = !anyLoopRoutine;


            this.digitLEDGreen.Enabled = digitController.LoopRoutine && this.digitController.LedGreenOn;
            this.packLEDGreen.Enabled = packController.LoopRoutine && this.packController.LedGreenOn;
            this.cartonLEDGreen.Enabled = cartonController.LoopRoutine && this.cartonController.LedGreenOn;
            this.palletLEDGreen.Enabled = palletController.LoopRoutine && this.palletController.LedGreenOn;
            this.scannerLEDGreen.Enabled = scannerController.LoopRoutine && this.scannerController.LedGreenOn;


            this.digitLEDPrinting.Enabled = digitController.OnPrinting && this.digitController.LedGreenOn;
            this.packLEDPrinting.Enabled = packController.OnPrinting && this.packController.LedGreenOn;
            this.cartonLEDPrinting.Enabled = cartonController.OnPrinting && this.cartonController.LedGreenOn;
            this.palletLEDPrinting.Enabled = palletController.OnPrinting && this.palletController.LedGreenOn;
            this.scannerLEDScanning.Enabled = scannerController.OnScanning && this.scannerController.LedGreenOn;
        }

        private void cutStatusBox(bool clearStatusBox)
        {
            if (clearStatusBox)
            {
                this.digitStatusbox.Text = "";
                this.packStatusbox.Text = "";
                this.cartonStatusbox.Text = "";
                this.palletStatusbox.Text = "";
                this.scannerStatusbox.Text = "";
            }
            else
            {
                if (this.digitStatusbox.TextLength > 1000) this.digitStatusbox.Text = this.digitStatusbox.Text.Substring(0, 1000);
                if (this.packStatusbox.TextLength > 1000) this.packStatusbox.Text = this.packStatusbox.Text.Substring(0, 1000);
                if (this.cartonStatusbox.TextLength > 1000) this.cartonStatusbox.Text = this.cartonStatusbox.Text.Substring(0, 1000);
                if (this.palletStatusbox.TextLength > 1000) this.palletStatusbox.Text = this.palletStatusbox.Text.Substring(0, 1000);
                if (this.scannerStatusbox.TextLength > 1000) this.scannerStatusbox.Text = this.scannerStatusbox.Text.Substring(0, 1000);
            }
        }


        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                e.Value = this.GetSerialNumber(e.Value.ToString(), sender);
            }
            catch
            { }
        }

        private void dataGridView_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)//e.RowIndex == -1 &&
                {
                    e.PaintBackground(e.CellBounds, true);
                    e.Graphics.TranslateTransform(e.CellBounds.Left, e.CellBounds.Bottom);
                    e.Graphics.RotateTransform(270);
                    e.Graphics.DrawString(e.FormattedValue.ToString(), e.CellStyle.Font, Brushes.Black, 5, 5);
                    e.Graphics.ResetTransform();
                    e.Handled = true;
                }
            }
            catch
            { }
        }

        private void dataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }

        private void dataGridView_Enter(object sender, EventArgs e)
        {
            this.dgvPackQueue.ScrollBars = ScrollBars.Horizontal;
        }

        private void dataGridView_Leave(object sender, EventArgs e)
        {
            this.dgvPackQueue.ScrollBars = ScrollBars.None;
        }

        private string GetSerialNumber(string printedBarcode)
        { return this.GetSerialNumber(printedBarcode, null); }
        private string GetSerialNumber(string printedBarcode, object sender)
        {
            int indexOfDoubleTabChar = printedBarcode.IndexOf(GlobalVariables.doubleTabChar.ToString());
            if (indexOfDoubleTabChar == 0)
                printedBarcode = ""; //10-AUG-2017: WHAT IS GlobalVariables.doubleTabChar.ToString()???
            //else if (printedBarcode.Length > 6) printedBarcode = printedBarcode.Substring(printedBarcode.Length - 7, 6); //Char[3][4][5]...[9]: Serial Number
            else
            {
                //if (this.fillingData.HasPack && printedBarcode.Length >= 29)
                //    printedBarcode = printedBarcode.Substring(indexOfDoubleTabChar - 6, 6);

                if (printedBarcode.Length >= 22) //!this.fillingData.HasPack && 
                {
                    if (sender != null && (sender.Equals(this.dgvPackQueue) || sender.Equals(this.dgvPacksetQueue)))
                        printedBarcode = printedBarcode.Substring(0, indexOfDoubleTabChar);
                    else
                        printedBarcode = printedBarcode.Substring(indexOfDoubleTabChar - 6, 6);

                    //if (sender != null && (this.fillingData.HasCarton && sender.Equals(this.dgvPalletQueue) || sender.Equals(this.dgvPalletPickupQueue)))
                    //    printedBarcode = printedBarcode.Substring(indexOfDoubleTabChar - 6, 6);
                    //else
                    //    printedBarcode = printedBarcode.Substring(0, indexOfDoubleTabChar);
                }
            }

            //////--BP CASTROL
            ////if (printedBarcode.IndexOf(GlobalVariables.doubleTabChar.ToString()) == 0) printedBarcode = "";
            //////else if (printedBarcode.Length > 6) printedBarcode = printedBarcode.Substring(printedBarcode.Length - 7, 6); //Char[3][4][5]...[9]: Serial Number
            ////else
            ////    if (printedBarcode.Length >= 29) printedBarcode = printedBarcode.Substring(23, 6); //Char[3][4][5]...[9]: Serial Number
            ////    else if (printedBarcode.Length >= 12) printedBarcode = printedBarcode.Substring(6, 5);

            return printedBarcode;
        }

        #endregion Handler


        #region Exception Handler

        /// <summary>
        /// Find a specific pack number in matching queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPackQueue_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string cellValue = "";
                if (CustomInputBox.Show("BP Filling System", "Please input pack number", ref cellValue) == System.Windows.Forms.DialogResult.OK)
                {
                    for (int rowIndex = 0; rowIndex < this.dgvPackQueue.Rows.Count; rowIndex++)
                    {
                        for (int columnIndex = 0; columnIndex < this.dgvPackQueue.Rows[rowIndex].Cells.Count; columnIndex++)
                        {
                            if (this.GetSerialNumber(this.dgvPackQueue[columnIndex, rowIndex].Value.ToString()).IndexOf(cellValue) != -1)
                            {
                                if (rowIndex >= 0 && rowIndex < this.dgvPackQueue.Rows.Count && columnIndex >= 0 && columnIndex < this.dgvPackQueue.ColumnCount)
                                    this.dgvPackQueue.CurrentCell = this.dgvPackQueue[columnIndex, rowIndex];
                                else
                                    this.dgvPackQueue.CurrentCell = null;
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                this.dgvPackQueue.CurrentCell = null;
            }
        }

        /// <summary>
        /// Remove a specific pack in matching queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeletePack_Click(object sender, EventArgs e)
        {
            if (this.dgvPackQueue.CurrentCell != null)
            {
                try
                {                //Handle exception for PackInOneCarton
                    string selectedBarcode = ""; string remarks = "";
                    int packID = this.getBarcodeID(this.dgvPackQueue.CurrentCell, out selectedBarcode);
                    if (packID > 0 && CustomMsgBox.Show(this, (sender.Equals(this.buttonDeleteAllPack) ? "Xóa toàn bộ chai đang trên chuyền" : "Xóa chai này:" + (char)13 + (char)13 + selectedBarcode) + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2, "Vui lòng nhập lý do", voidTypeNames, ref remarks) == System.Windows.Forms.DialogResult.Yes)
                        if (this.scannerController.RemovePackInPackQueue(packID, remarks, sender.Equals(this.buttonDeleteAllPack))) CustomMsgBox.Show(this, (sender.Equals(this.buttonDeleteAllPack) ? "Toàn bộ chai đang trên chuyền" : "Pack: " + selectedBarcode) + "\r\n\r\nĐã được xóa thành công.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        /// <summary>
        /// Move Packset To Carton Pending Queue (THE SAME AS NoRead, BUT: WITHOUT READ BY SCANNER)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvPackset_Remove(object sender, EventArgs e)
        {
            if (this.dgvPacksetQueue.CurrentCell != null)
            {
                try
                {
                    string selectedBarcode = "";
                    int packID = this.getBarcodeID(this.dgvPacksetQueue.CurrentCell, out selectedBarcode);
                    if (packID > 0 && CustomMsgBox.Show(this, "Thùng carton này chuẩn bị đóng, bạn muốn bỏ ra khỏi chuyền xử lý sau:" + (char)13 + (char)13 + selectedBarcode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        if (this.scannerController.MovePacksetToCartonPendingQueue(packID, null)) CustomMsgBox.Show(this, "Thùng carton (chuẩn bị đóng) chứa chai: " + selectedBarcode + "\r\nđã bỏ ra khỏi chuyền.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }


        private void dgvCarton_Remove(object sender, EventArgs e)
        {
            try
            {
                DataGridView dataGridView = sender.Equals(this.buttonRemoveCarton) ? this.dgvCartonQueue : this.dgvCartonsetQueue;
                if (dataGridView != null && dataGridView.CurrentCell != null)
                {
                    string selectedBarcode = "";
                    int barcodeID = this.getBarcodeID(dataGridView.CurrentCell, out selectedBarcode);
                    if (barcodeID > 0 && CustomMsgBox.Show(this, "Are you sure you want to remove this carton:" + (char)13 + (char)13 + selectedBarcode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        if (this.scannerController.MoveCartonToPendingQueue(barcodeID, dataGridView.Equals(this.dgvCartonsetQueue))) CustomMsgBox.Show(this, "Carton: " + selectedBarcode + "\r\nHas been removed successfully.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        /// <summary>
        /// REMOVE: Unwrap a Noread || Pending carton
        /// DELETE: DELETE ALL PACK IN a Noread || Pending carton => USER MUST TO SCAN BY MATCHING SCANNER AGAIN IN ORDER TO MAKE CARTON
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCartonPending_RemoveDelete(object sender, EventArgs e)
        {
            if (this.dgvCartonPendingQueue.CurrentCell != null)
            {
                try
                {
                    string selectedBarcode = ""; string remarks = "";
                    int cartonID = this.getBarcodeID(this.dgvCartonPendingQueue.CurrentCell, out selectedBarcode);
                    if (cartonID > 0 && CustomMsgBox.Show(this, "Bạn có muốn " + (sender.Equals(this.buttonRemoveCartonPending) ? "xả thùng carton này ra và đóng lại không:" : "xóa toàn bộ thùng carton, bao gồm các chai bên trong,: ") + (char)13 + (char)13 + selectedBarcode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2, "Vui lòng nhập lý do", voidTypeNames, ref remarks) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (sender.Equals(this.buttonRemoveCartonPending))
                            if ((this.fillingData.HasPack && this.scannerController.UnwrapCartontoPack(cartonID, remarks)) || (!this.fillingData.HasPack && this.scannerController.TakebackCartonFromPendingQueue(cartonID))) CustomMsgBox.Show(this, "Carton: " + selectedBarcode + "\r\nĐã được xả thành công.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (sender.Equals(this.buttonDeleteCartonPending))
                            if (this.scannerController.DeleteCarton(cartonID, remarks)) CustomMsgBox.Show(this, "Carton: " + selectedBarcode + "\r\nĐã được xóa, bao gồm các chai bên trong." + "\r\nVui lòng đọc lại từng chai nếu muốn đóng lại carton.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }


        private void dgvPalletQueue_Remove(object sender, EventArgs e)
        {
            if (this.dgvPalletQueue.CurrentCell != null && this.dgvCartonsetQueue.ColumnCount <= 0)
            {
                try
                {
                    string selectedBarcode = ""; string remarks = "";
                    int palletID = this.getBarcodeID(this.dgvPalletQueue.CurrentCell, out selectedBarcode);
                    if (this.fillingData.HasCarton && palletID > 0 && CustomMsgBox.Show(this, "Bạn có muốn xả pallet này ra và đóng lại không:" + (char)13 + (char)13 + selectedBarcode, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2, "Vui lòng nhập lý do", voidTypeNames, ref remarks) == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (this.scannerController.UnwrapPallettoCarton(palletID, remarks)) CustomMsgBox.Show(this, "Pallet: " + selectedBarcode + "\r\nĐã được xả thành công.", "Handle exception", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        private void dgvQueue_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                try
                {
                    DataGridView dataGridView = sender as DataGridView;
                    if (dataGridView != null && dataGridView.CurrentCell != null)
                    {
                        string selectedBarcode = "";
                        int barcodeID = this.getBarcodeID(dataGridView.CurrentCell, out selectedBarcode);
                        if (barcodeID > 0)
                        {
                            int cartonID = sender.Equals(this.dgvCartonPendingQueue) || sender.Equals(this.dgvCartonQueue) || sender.Equals(this.dgvCartonsetQueue) ? barcodeID : 0;
                            int palletID = sender.Equals(this.dgvCartonPendingQueue) || sender.Equals(this.dgvCartonQueue) || sender.Equals(this.dgvCartonsetQueue) ? 0 : barcodeID;

                            this.ShowQuickView(selectedBarcode, cartonID, palletID, 0);
                        }
                    }
                }
                catch (Exception exception)
                {
                    ExceptionHandlers.ShowExceptionMessageBox(this, exception);
                }
            }
        }

        private void ShowQuickView(string selectedBarcode, int cartonID, int palletID, int batchID)
        {
            QuickView quickView = new QuickView(this.scannerAPIs.GetBarcodeList(this.fillingData.FillingLineID, cartonID, palletID, batchID), (cartonID != 0 ? "Carton: " : (palletID != 0 ? "Pallet: " : "Batch: ")) + selectedBarcode);
            quickView.ShowDialog(); quickView.Dispose();
        }

        private int getBarcodeID(DataGridViewCell dataGridViewCell, out string selectedBarcode)
        {
            int barcodeID;
            if (dataGridViewCell != null)
            {
                selectedBarcode = dataGridViewCell.Value as string;
                if (selectedBarcode != null)
                {
                    int startIndexOfPackID = selectedBarcode.IndexOf(GlobalVariables.doubleTabChar.ToString() + GlobalVariables.doubleTabChar.ToString());
                    if (startIndexOfPackID >= 0 && int.TryParse(selectedBarcode.Substring(startIndexOfPackID + 2), out barcodeID))
                    {
                        selectedBarcode = this.GetSerialNumber(selectedBarcode) + ": " + selectedBarcode.Substring(0, startIndexOfPackID);
                        return barcodeID;
                    }
                }
            }
            selectedBarcode = null;
            return -1;
        }

        private void buttonPackQueueCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (CustomMsgBox.Show(this, "Bạn có muốn reset số thứ tự chia làn auto packer về vị trí gốc [1, 1].?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    this.scannerController.ResetNextPackQueueID();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonPacksetQueueCount_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.scannerController.PackQueueCount > 0 && this.scannerController.PacksetQueueCount == 0 && CustomMsgBox.Show(this, "Bạn có muốn chia đều chai vào các làn auto packer không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    this.scannerController.ReAllocationPack();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonToggleLastPackset_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.scannerController.PackQueueCount > 0 && this.scannerController.PackQueueCount < this.fillingData.PackPerCarton && this.scannerController.PacksetQueueCount == 0 && CustomMsgBox.Show(this, "Số lượng hộp ít hơn số lượng cần đóng carton.\r\n\r\nBạn có muốn đóng carton ngay bây giờ không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    this.scannerController.ToggleLastPackset(true);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonCartonQueueCount_Click(object sender, EventArgs e)
        {
            try
            {//HERE: WE CHECK CONDITION BEFORE CALL ToggleLastCartonset (TO PROTEST ACCIDENTAL PRESS BY WORKER). SURELY, WE CAN OMIT THESE CONDITION  ============> NOW, 09-OCT-2017: WE OMIT SOME CONDITIONS!!! LATER: WE CAN RE-ADD THESE CONDITIONS FOR CHECKING AGAIN, IF NEEDED!!!         this.scannerController.PackQueueCount == 0 && this.scannerController.PacksetQueueCount == 0 && this.scannerController.CartonPendingQueueCount == 0 && 
                if (this.scannerController.CartonQueueCount > 0 && this.scannerController.CartonQueueCount < this.fillingData.CartonPerPallet && this.scannerController.CartonsetQueueCount == 0 && CustomMsgBox.Show(this, "Số lượng carton ít hơn số lượng cần đóng pallet.\r\n\r\nBạn có muốn đóng pallet ngay bây giờ không?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    this.scannerController.ToggleLastCartonset(true);
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonPalletQueueCount_Click(object sender, EventArgs e)
        {
            if (this.fillingData.BatchID >= 0)
            {
                //NEED TO REFRESH scannerAPIs
                this.scannerAPIs = new ScannerAPIs(CommonNinject.Kernel.Get<IPackRepository>(), CommonNinject.Kernel.Get<ICartonRepository>(), CommonNinject.Kernel.Get<IPalletRepository>());
                this.ShowQuickView(this.fillingData.BatchCode, 0, 0, this.fillingData.BatchID);
            }
        }

        #endregion Exception Handler




        #region Backup

        private void timerNmvnBackup_Tick(object sender, EventArgs e)
        {
            try
            {
                this.BackupDataHandler();
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void BackupDataHandler()
        {
            try
            {
                if (digitThread == null || !digitThread.IsAlive)
                {
                    if (packThread == null || !packThread.IsAlive)
                    {
                        if (cartonThread == null || !cartonThread.IsAlive)
                        {
                            if (palletThread == null || !palletThread.IsAlive)
                            {
                                if (scannerThread == null || !scannerThread.IsAlive)
                                {
                                    if (backupDataThread == null || !backupDataThread.IsAlive)
                                    {
                                        backupDataThread = new Thread(new ThreadStart(this.scannerController.BackupData));
                                        backupDataThread.Start();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Backup


        #region Repacks
        private void buttonRepackImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.AnyLoopRoutine())
                {
                    string fileName = null;

                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Filter = "Text Document (.txt)|*.txt";
                    if (openFileDialog.ShowDialog() == DialogResult.OK && openFileDialog.FileName != "") fileName = openFileDialog.FileName;

                    if (fileName != null)
                    {
                        bool dialogResultOK;
                        BatchRepackWizard batchRepackWizard = new BatchRepackWizard(this.fillingData, fileName);

                        dialogResultOK = batchRepackWizard.ShowDialog() == System.Windows.Forms.DialogResult.OK;
                        batchRepackWizard.Dispose();

                        if (dialogResultOK) this.InitializeRepack(this.AnyLoopRoutine());
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void buttonRepackRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.AnyLoopRoutine() && CustomMsgBox.Show(this, "Xóa toàn bộ mã vạch lon đã import?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                    if (CustomMsgBox.Show(this, "Vui lòng xác nhận xóa toàn bộ mã vạch lon đã import!", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                        if (this.batchService.RepackDelete(this.fillingData.BatchID)) this.InitializeRepack(this.AnyLoopRoutine());
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }


        private void buttonRepackReprint_Click(object sender, EventArgs e)
        {
            try
            {
                ObjectView<BatchRepackDTO> batchRepackDTOView = this.dgvRepacks.CurrentRow.DataBoundItem as ObjectView<BatchRepackDTO>;
                if (batchRepackDTOView != null)
                {
                    BatchRepackDTO batchRepackDTO = (BatchRepackDTO)batchRepackDTOView;

                    if (batchRepackDTO != null && batchRepackDTO.PrintedTimes != 0)
                    {
                        if (!this.AnyLoopRoutine() && CustomMsgBox.Show(this, "In lại mã vạch này: " + "\r\n" + "\r\n" + batchRepackDTO.Code + "?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                            if (CustomMsgBox.Show(this, "Vui lòng xác nhận sẽ in lại mã vạch này: " + "\r\n" + "\r\n" + batchRepackDTO.Code, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.Yes)
                                if (this.batchService.RepackReprint(batchRepackDTO.RepackID)) batchRepackDTO.PrintedTimes = 0;
                    }
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        public void ReprintCarton(int cartonID)
        {
            try
            {
                if (this.buttonConnect.Enabled && this.scannerController.AllQueueEmpty)
                {
                    RepackController repackController = new RepackController(CommonNinject.Kernel.Get<IRepackService>(), CommonNinject.Kernel.Get<RepackViewModel>());

                    IList<BatchRepack> lookupRepacks = repackController.repackService.LookupRecartons(cartonID);
                    if (lookupRepacks != null && lookupRepacks.Count > 0)
                    {
                        BatchRepack batchRepack = lookupRepacks.First();

                        BatchAPIs batchAPIs = new BatchAPIs(CommonNinject.Kernel.Get<IBatchAPIRepository>());
                        BatchIndex batchIndex = batchAPIs.GetBatchByID(batchRepack.BatchID);

                        if (batchIndex != null)
                        {
                            batchIndex.BatchTypeID = (int)GlobalEnums.BatchTypeID.Repack;

                            ////////GlobalEnums.OnTestRepackWithoutScanner = true;

                            this.Initialize(batchIndex, batchRepack);
                        }
                        else
                            throw new Exception("Không tìm thấy batch của carton cần preprint");
                    }
                    else
                        throw new Exception("Không tìm thấy carton cần preprint");
                }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        #endregion Repacks

    }
}


