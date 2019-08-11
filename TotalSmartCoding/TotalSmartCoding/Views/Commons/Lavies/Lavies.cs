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
using TotalSmartCoding.Controllers.APIs.Generals;
using TotalCore.Repositories.Generals;
using TotalModel.Helpers;
using AutoMapper;
using System.Threading;
using System.IO;
using TotalCore.Helpers;
using TotalSmartCoding.Libraries.Communications;
using System.Net;
using System.IO.Ports;
using System.Reflection;


namespace TotalSmartCoding.Views.Commons.Lavies
{
    public partial class Lavies : BaseView
    {
        private CustomTabControl customTabCenter;

        private LavieAPIs lavieAPIs;
        private LavieViewModel lavieViewModel { get; set; }
        private BindingList<LavieIndexDTO> LavieIndexes { get; set; }

        private List<Mstatus> Mstatuses;

        public Lavies()
            : base()
        {
            InitializeComponent();

            this.toolstripChild = this.toolStripChildForm;
            this.fastListIndex = this.fastLavieIndex;

            this.lavieAPIs = new LavieAPIs(CommonNinject.Kernel.Get<ILavieAPIRepository>());

            this.lavieViewModel = CommonNinject.Kernel.Get<LavieViewModel>();
            this.lavieViewModel.PropertyChanged += new PropertyChangedEventHandler(ModelDTO_PropertyChanged);
            this.baseDTO = this.lavieViewModel;

            this.LavieIndexes = new BindingList<LavieIndexDTO>();

            this.ionetSocket = new IONetSocket(IPAddress.Parse("10.208.14.100"), 9100, false);
            //this.ioserialPort = new IOSerialPort(GlobalVariables.ComportName, 115200, Parity.None, 8, StopBits.One, false, "MSERIES");

            //this.ioserialPort.PropertyChanged += new PropertyChangedEventHandler(ioserialPort_PropertyChanged);


            this.InitMstatuses();
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

        Binding bindingItemNumber;
        Binding bindingProductName;

        protected override void InitializeCommonControlBinding()
        {
            base.InitializeCommonControlBinding();

            this.bindingItemNumber = this.textexItemNumber.DataBindings.Add("Text", this.lavieViewModel, CommonExpressions.PropertyName<LavieDTO>(p => p.ItemNumber), true, DataSourceUpdateMode.OnPropertyChanged);
            this.bindingProductName = this.textexProductName.DataBindings.Add("Text", this.lavieViewModel, CommonExpressions.PropertyName<LavieDTO>(p => p.ProductName), true, DataSourceUpdateMode.OnPropertyChanged);

            this.bindingItemNumber.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);
            this.bindingProductName.BindingComplete += new BindingCompleteEventHandler(CommonControl_BindingComplete);

            this.fastLavieIndex.AboutToCreateGroups += fastLavieIndex_AboutToCreateGroups;

            this.fastLavieIndex.ShowGroups = true;
            //this.olvInActive.Renderer = new MappedImageRenderer(new Object[] { true, Resources.Void_16 });

            this.dgvRepacks.AutoGenerateColumns = false;
            this.dgvRepacks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRepacks.DataSource = this.LavieIndexes;
        }

        private void fastLavieIndex_AboutToCreateGroups(object sender, CreateGroupsEventArgs e)
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

        private LavieController lavieController
        {
            get { return new LavieController(CommonNinject.Kernel.Get<ILavieService>(), this.lavieViewModel); }
        }

        protected override Controllers.BaseController myController
        {
            get { return new LavieController(CommonNinject.Kernel.Get<ILavieService>(), this.lavieViewModel); }
        }



        public override void Loading()
        {
            ICollection<LavieIndex> lavieIndexes = this.lavieAPIs.GetLavieIndexes();
            //this.fastLavieIndex.SetObjects(lavieIndexes); REMARK HERE

            base.Loading();



            this.LavieIndexes.RaiseListChangedEvents = false;
            this.LavieIndexes.Clear();

            if (lavieIndexes.Count > 0)
            {
                int lineIndex = 0;
                lavieIndexes.Each(lavieIndex =>
                {
                    LavieIndexDTO lavieIndexDTO = Mapper.Map<LavieIndex, LavieIndexDTO>(lavieIndex);
                    lavieIndexDTO.LineIndex = ++lineIndex;
                    this.LavieIndexes.Add(lavieIndexDTO);
                });
            }

            this.LavieIndexes.RaiseListChangedEvents = true;
            this.LavieIndexes.ResetBindings();
        }

        protected override void DoAfterLoad()
        {
            base.DoAfterLoad();
            this.fastLavieIndex.Sort(this.olvItemNumber, SortOrder.Descending);
        }


        public override void Import()
        {
            this.ImportExcel(GlobalEnums.MappingTaskID.Lavie);
        }

        protected override void DoImportExcel(string fileName, string sheetName)
        {
            base.DoImportExcel(fileName, sheetName);

            this.lavieAPIs.LavieDoEmpty();
            this.LavieIndexes.Clear();

            this.ImportExcel(fileName, sheetName);
            this.Loading();
        }




        #region Import Excel

        public bool ImportExcel(string fileName, string sheetName)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                OleDbAPIs oleDbAPIs = new OleDbAPIs(CommonNinject.Kernel.Get<IOleDbAPIRepository>(), GlobalEnums.MappingTaskID.Lavie);

                CommodityViewModel commodityViewModel = CommonNinject.Kernel.Get<CommodityViewModel>();
                CommodityController commodityController = new CommodityController(CommonNinject.Kernel.Get<ICommodityService>(), commodityViewModel);


                int intValue; decimal decimalValue; DateTime dateTimeValue;
                ExceptionTable exceptionTable = new ExceptionTable(new string[2, 2] { { "ExceptionCategory", "System.String" }, { "Description", "System.String" } });

                //////////TimeSpan timeout = TimeSpan.FromMinutes(90);
                //////////using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required, timeout))
                //////////{
                //////////if (!this.Editable(this.)) throw new System.ArgumentException("Import", "Permission conflict");


                DataTable excelDataTable = oleDbAPIs.OpenExcelSheet(fileName, sheetName);
                if (excelDataTable != null && excelDataTable.Rows.Count > 0)
                {
                    foreach (DataRow excelDataRow in excelDataTable.Rows)
                    {
                        exceptionTable.ClearDirty();

                        this.lavieController.Create();

                        this.lavieViewModel.EntryDate = new DateTime(2000, 1, 1);
                        this.lavieViewModel.PrintedTimes = 0;

                        if (int.TryParse(excelDataRow["SerialID"].ToString(), out intValue)) this.lavieViewModel.SerialID = intValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu No", "No: " + excelDataRow["SerialID"].ToString() });

                        if (DateTime.TryParse(excelDataRow["ExpirationDate"].ToString(), out dateTimeValue)) this.lavieViewModel.ExpirationDate = dateTimeValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu ExpirationDate", "No: " + this.lavieViewModel.SerialID + ": " + excelDataRow["ExpirationDate"].ToString() });
                        if (decimal.TryParse(excelDataRow["Qty"].ToString(), out decimalValue)) this.lavieViewModel.Qty = decimalValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu Qty", "No: " + this.lavieViewModel.SerialID + ": " + excelDataRow["Qty"].ToString() });
                        if (decimal.TryParse(excelDataRow["Layers"].ToString(), out decimalValue)) this.lavieViewModel.Layers = decimalValue; else exceptionTable.AddException(new string[] { "Lỗi cột dữ liệu Layers", "No: " + this.lavieViewModel.SerialID + ": " + excelDataRow["Layers"].ToString() });

                        this.lavieViewModel.ItemNumber = excelDataRow["ItemNumber"].ToString();
                        this.lavieViewModel.ProductName = excelDataRow["ProductName"].ToString();
                        this.lavieViewModel.GTIN = excelDataRow["GTIN"].ToString();
                        this.lavieViewModel.PalletID = excelDataRow["PalletID"].ToString();
                        this.lavieViewModel.BatchNumber = excelDataRow["BatchNumber"].ToString();
                        this.lavieViewModel.GTINBarcode = excelDataRow["GTINBarcode"].ToString();
                        this.lavieViewModel.Barcode = excelDataRow["Barcode"].ToString();


                        if (!this.lavieViewModel.IsValid) exceptionTable.AddException(new string[] { "Lỗi dữ liệu không hợp lệ, dòng No: ", this.lavieViewModel.SerialID + ": " + this.lavieViewModel.Error }); ;
                        if (!exceptionTable.IsDirty)
                            if (this.lavieViewModel.IsDirty && !this.lavieController.Save())
                                exceptionTable.AddException(new string[] { "Lỗi lưu dữ liệu, dòng No: ", this.lavieViewModel.SerialID + this.lavieController.BaseService.ServiceTag });

                    }
                }

                Cursor.Current = Cursors.WaitCursor;

                if (exceptionTable.Table.Rows.Count <= 0)
                    return true;
                else
                    throw new CustomException("Lỗi import file excel. Vui lòng xem danh sách đính kèm. Click vào từng nội dung để xem chi tiết.", exceptionTable.Table);

            }
            catch (System.Exception exception)
            {
                Cursor.Current = Cursors.WaitCursor;
                throw exception;
            }
        }


        #endregion Import Excel




        #region Thread

        private void buttonStart_Click(object sender, EventArgs e)
        {
            if (lavieThread != null && lavieThread.IsAlive) lavieThread.Abort();
            lavieThread = new Thread(new ThreadStart(this.ThreadRoutine));

            lavieThread.Start();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.OnRunning = false;
        }

        private void Lavies_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (lavieThread != null && lavieThread.IsAlive) { e.Cancel = true; return; }
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }






        private Thread lavieThread;
        delegate void propertyChangedThread(object sender, int lavieID, string connectStatus, string showMessage, string cycleStatus);

        private IONetSocket ionetSocket;
        //private IOSerialPort ioserialPort;

        private bool OnRunning { get; set; }

        public void ThreadRoutine()
        {
            try
            {
                this.showStatus(this.buttonStart, 0, "Connecting ...", "", "");
                this.ionetSocket.Connect(); //this.ioserialPort.Connect();
                //////string curFile = @"c:\temp\PrintGo.txt"; string finishFile = @"c:\temp\Finish.txt";
                //////if (!File.Exists(finishFile)) System.IO.File.WriteAllText(finishFile, "");

                this.OnRunning = true;
                this.showStatus(this.buttonStart, 0, "Connected!", "#", "#");

                int lavieIndex = this.LavieIndexes.Where(w => w.PrintedTimes == 0).First().LineIndex; int i = 0;
                while (this.OnRunning)
                {
                    if (lavieIndex < (this.LavieIndexes.Count + 1))
                    {
                        LavieIndexDTO lavieIndexDTO = this.LavieIndexes[lavieIndex - 1];

                        if (this.waitforReady())
                        {
                            if (i++ <= 20 || this.lavieAPIs.SystemInfoValidate())
                            {
                                //string[] lines = { "[Label 1]", "Label=LABEL", "Quantity=0", "Dyn01=" + lavieIndexDTO.ItemNumber, "Dyn02=" + lavieIndexDTO.ProductName, "Dyn03=" + lavieIndexDTO.GTIN, "Dyn04=" + lavieIndexDTO.PalletID, "Dyn05=" + lavieIndexDTO.BatchNumber, "Dyn06=" + ((DateTime)lavieIndexDTO.ExpirationDate).ToString("dd/MM/yyyy"), "Dyn07=" + ((decimal)lavieIndexDTO.Qty).ToString("N0"), "Dyn08=" + ((decimal)lavieIndexDTO.Layers).ToString("N0"), "Dyn09=" + lavieIndexDTO.GTINBarcode, "Dyn10=" + lavieIndexDTO.Barcode, "Dyn11=" + lavieIndexDTO.SerialID };
                                //string[] lines = { "[Label 1]", "PrinterIP=101.208.14.100:9100", "Label=LABEL", "Quantity=0", "Dyn01=" + lavieIndexDTO.ItemNumber, "Dyn02=" + lavieIndexDTO.ProductName, "Dyn03=" + lavieIndexDTO.GTIN, "Dyn04=" + lavieIndexDTO.PalletID, "Dyn05=" + lavieIndexDTO.BatchNumber, "Dyn06=" + ((DateTime)lavieIndexDTO.ExpirationDate).ToString("dd/MM/yyyy"), "Dyn07=" + ((decimal)lavieIndexDTO.Qty).ToString("N0"), "Dyn08=" + ((decimal)lavieIndexDTO.Layers).ToString("N0"), "Dyn09=" + lavieIndexDTO.GTINBarcode, "Dyn10=" + lavieIndexDTO.Barcode, "Dyn11=" + lavieIndexDTO.SerialID, SystemInfos.GetSystemInfos(true) };
                                //System.IO.File.WriteAllLines(curFile, lines);

                                this.writeToBuffer(lavieIndexDTO); ; Thread.Sleep(500);
                                this.showStatus(this, lavieIndexDTO.LavieID, "#", "No: " + lavieIndexDTO.SerialID + "; Pallet ID: " + lavieIndexDTO.PalletID, "Printing");
                            }
                            else
                            { throw new Exception("Invalid version. Please contact your vendor for more information."); }
                        }

                        if (this.waitforPrinted()) { this.lavieAPIs.LavieUpdate(lavieIndexDTO.LavieID); lavieIndexDTO.PrintedTimes = 1; lavieIndex++; }
                    }
                    else
                        this.showStatus(this, -1, "#", "Finished!", "#"); //REACH THE LAST ROW => TO FINISH
                    Thread.Sleep(500);
                }
            }
            catch (Exception exception)
            {
                this.showStatus(this, 0, "Error", "#", exception.Message);
                this.buttonStop_Click(this, new EventArgs());
            }
            finally
            {
                this.ionetSocket.Disconnect(); //this.ioserialPort.Disconnect();
                this.showStatus(this.buttonStop, 0, "Disconnected!", "#", "#");
            }
        }

        private bool writeToBuffer(LavieIndexDTO lavieIndexDTO)
        {
            try
            {
                this.ionetSocket.WritetoStream(GlobalVariables.charSTX + "/0/41/C1/E1/" + GlobalVariables.charETB + "/D/" + lavieIndexDTO.ItemNumber + GlobalVariables.charLF + lavieIndexDTO.ProductName + GlobalVariables.charLF + lavieIndexDTO.GTIN + GlobalVariables.charLF + lavieIndexDTO.PalletID + GlobalVariables.charLF + lavieIndexDTO.BatchNumber + GlobalVariables.charLF + ((DateTime)lavieIndexDTO.ExpirationDate).ToString("dd/MM/yyyy") + GlobalVariables.charLF + ((decimal)lavieIndexDTO.Qty).ToString("N0") + GlobalVariables.charLF + ((decimal)lavieIndexDTO.Layers).ToString("N0") + GlobalVariables.charLF + lavieIndexDTO.GTINBarcode + GlobalVariables.charLF + lavieIndexDTO.Barcode + GlobalVariables.charLF + lavieIndexDTO.SerialID + "/??" + GlobalVariables.charCR); Thread.Sleep(20); string receivedFeedback = "";
                if (this.waitforMSeries(ref receivedFeedback) && receivedFeedback != null & receivedFeedback != "")
                {
                    if (receivedFeedback.Length >= 8 && receivedFeedback.ElementAt(0) == GlobalVariables.charSOH && receivedFeedback.ElementAt(7) == GlobalVariables.charCR && receivedFeedback.Substring(1, 4) == "0A41")
                        return true;
                    else
                        this.showStatus(this, 0, "#", "#", "Error: " + (receivedFeedback.Substring(1, 2) == "0D" ? receivedFeedback.Substring(2, 4) : receivedFeedback.Substring(2, 3))); //Show ERROR
                }
                return false;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private bool waitforReady()
        {
            try
            {
                //this.ioserialPort.WritetoSerial(GlobalVariables.charSTX + "30/30/30/3F/3F" + GlobalVariables.charCR, true);
                this.ionetSocket.WritetoStream(GlobalVariables.charSTX + "/00/0/??/" + GlobalVariables.charCR); string receivedFeedback = "";
                if (this.waitforMSeries(ref receivedFeedback) && receivedFeedback != null & receivedFeedback != "" && receivedFeedback.Length >= 11)
                {

                    if (receivedFeedback.ElementAt(0) == GlobalVariables.charSOH && receivedFeedback.ElementAt(10) == GlobalVariables.charCR && receivedFeedback.Substring(1, 4) == "0A00")
                        if (receivedFeedback.Substring(5, 3) == "999" || receivedFeedback.Substring(5, 3) == "803" || receivedFeedback.Substring(5, 3) == "804")
                        { this.showStatus(this, 0, "#", "#", "Idle"); return true; }
                        else
                            this.showStatus(this, 0, "#", "#", "ERROR"); //Show ERROR
                }
                return false;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private bool waitforPrinted()
        {
            try
            {
                //this.ioserialPort.WritetoSerial(GlobalVariables.charSTX + "30/30/30/3F/3F" + GlobalVariables.charCR, true);
                this.ionetSocket.WritetoStream(GlobalVariables.charSTX + "/00/0/??/" + GlobalVariables.charCR); string receivedFeedback = "";
                if (this.waitforMSeries(ref receivedFeedback) && receivedFeedback != null & receivedFeedback != "" && receivedFeedback.Length >= 11)
                {

                    if (receivedFeedback.ElementAt(0) == GlobalVariables.charSOH && receivedFeedback.ElementAt(10) == GlobalVariables.charCR && receivedFeedback.Substring(1, 4) == "0A00")
                        if (receivedFeedback.Substring(5, 3) == "998" || receivedFeedback.Substring(5, 3) == "991" || receivedFeedback.Substring(5, 3) == "994")
                        { this.showStatus(this, 0, "#", "#", "Idle"); return true; } //Show STATUS ON SEPARATE STATUS LABLE
                        else
                            this.showStatus(this, 0, "#", "#", "Show ERROR");
                }
                return false;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private bool waitforMSeries(ref string receivedFeedback)
        {
            try
            {
                receivedFeedback = this.ionetSocket.ReadoutStream();
                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }




        #region Handle Status
        private void showStatus(object sender, int lavieID, string connectStatus, string progressStatus, string cycleStatus)
        {
            try
            {
                propertyChangedThread propertyChangedDelegate = new propertyChangedThread(propertyChangedHandler);
                this.Invoke(propertyChangedDelegate, new object[] { sender, lavieID, connectStatus, progressStatus, cycleStatus });
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void propertyChangedHandler(object sender, int lavieID, string connectStatus, string progressStatus, string cycleStatus)
        {
            try
            {
                this.buttonStart.Enabled = !this.OnRunning;
                this.buttonStop.Enabled = this.OnRunning;

                if (connectStatus != "#") this.connectStatus.Text = connectStatus;
                if (cycleStatus != "#") this.cycleStatus.Text = cycleStatus;
                if (progressStatus != "#") this.progressStatus.Text = progressStatus;


                //if (!sender.Equals(this.buttonStart) && !sender.Equals(this.buttonStop))
                //{
                //    this.LavieIndexes.Where(w => w.LavieID == lavieID).Each(batchRepackDTO =>
                //        {
                //            batchRepackDTO.PrintedTimes = 1;
                //            this.progressStatus.Text = "Printing: No: " + batchRepackDTO.SerialID + "; Pallet ID: " + batchRepackDTO.PalletID;
                //        });
                //}

                return;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void ioserialPort_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                PropertyInfo prop = this.GetType().GetProperty(e.PropertyName, BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(this, sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null), null);
                else
                    this.progressStatus.Text = e.PropertyName + ": " + sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null).ToString();
            }
            catch (Exception exception)
            {
                this.progressStatus.Text = exception.Message;
            }
        }


        private void InitMstatuses()
        {
            this.Mstatuses = new List<Mstatus>();

            this.Mstatuses.Add(new Mstatus() { Code = "600", Status = "No contact with cpu3 ACC Status" });
            this.Mstatuses.Add(new Mstatus() { Code = "601", Status = "Print ACC" });
            this.Mstatuses.Add(new Mstatus() { Code = "602", Status = "Print Old" });
            this.Mstatuses.Add(new Mstatus() { Code = "603", Status = "Get queue status from cpu1" });
            this.Mstatuses.Add(new Mstatus() { Code = "604", Status = "Get extern label from ACC" });
            this.Mstatuses.Add(new Mstatus() { Code = "605", Status = "Get length from ACC (special)" });
            this.Mstatuses.Add(new Mstatus() { Code = "609", Status = "Soft Reset" });
            this.Mstatuses.Add(new Mstatus() { Code = "700", Status = "No contact with cpu2" });
            this.Mstatuses.Add(new Mstatus() { Code = "701", Status = "Print button" });
            this.Mstatuses.Add(new Mstatus() { Code = "702", Status = "Test button" });
            this.Mstatuses.Add(new Mstatus() { Code = "703", Status = "Start calibrate ribbon, label or length" });
            this.Mstatuses.Add(new Mstatus() { Code = "704", Status = "Stop calibrate ribbon, label or length" });
            this.Mstatuses.Add(new Mstatus() { Code = "705", Status = "Menu active. Offline" });
            this.Mstatuses.Add(new Mstatus() { Code = "706", Status = "Menu inactive. Online" });
            this.Mstatuses.Add(new Mstatus() { Code = "707", Status = "Read label length" });
            this.Mstatuses.Add(new Mstatus() { Code = "708", Status = "System reset" });
            this.Mstatuses.Add(new Mstatus() { Code = "709", Status = "Soft reset" });
            this.Mstatuses.Add(new Mstatus() { Code = "710", Status = "Receive and set EEPROM variables" });
            this.Mstatuses.Add(new Mstatus() { Code = "711", Status = "Receive and set Label number" });
            this.Mstatuses.Add(new Mstatus() { Code = "712", Status = "Receive ACC var. and trans cpu3" });
            this.Mstatuses.Add(new Mstatus() { Code = "713", Status = "Receive CPU1 Constants" });
            this.Mstatuses.Add(new Mstatus() { Code = "714", Status = "Receive and set real time clock" });
            this.Mstatuses.Add(new Mstatus() { Code = "715", Status = "Receive and set counters to zero" });
            this.Mstatuses.Add(new Mstatus() { Code = "716", Status = "Receive, set EEPROM var. Eth. Reset" });
            this.Mstatuses.Add(new Mstatus() { Code = "717", Status = "Reset Ethernet" });
            this.Mstatuses.Add(new Mstatus() { Code = "720", Status = "Transmit EEPROM variables" });
            this.Mstatuses.Add(new Mstatus() { Code = "721", Status = "Transmit Label number" });
            this.Mstatuses.Add(new Mstatus() { Code = "722", Status = "Transmit ACC variables" });
            this.Mstatuses.Add(new Mstatus() { Code = "723", Status = "Transmit Program versions" });
            this.Mstatuses.Add(new Mstatus() { Code = "724", Status = "Transmit real clock times" });
            this.Mstatuses.Add(new Mstatus() { Code = "725", Status = "Transmit label counters" });
            this.Mstatuses.Add(new Mstatus() { Code = "726", Status = "Transmit languages" });
            this.Mstatuses.Add(new Mstatus() { Code = "730", Status = "Read settings from Ethernet" });
            this.Mstatuses.Add(new Mstatus() { Code = "731", Status = "Test print selected label" });
            this.Mstatuses.Add(new Mstatus() { Code = "801", Status = "Printhead up" });
            this.Mstatuses.Add(new Mstatus() { Code = "802", Status = "Printhead overheated" });
            this.Mstatuses.Add(new Mstatus() { Code = "803", Status = "Label low" });
            this.Mstatuses.Add(new Mstatus() { Code = "804", Status = "Ribbon low" });
            this.Mstatuses.Add(new Mstatus() { Code = "805", Status = "No labels" });
            this.Mstatuses.Add(new Mstatus() { Code = "806", Status = "No ribbon" });
            this.Mstatuses.Add(new Mstatus() { Code = "807", Status = "5 volt to printhead is missing" });
            this.Mstatuses.Add(new Mstatus() { Code = "808", Status = "24 volt to printhead is missing" });
            this.Mstatuses.Add(new Mstatus() { Code = "809", Status = "36 volt to printhead is missing" });
            this.Mstatuses.Add(new Mstatus() { Code = "810", Status = "Applicator error" });
            this.Mstatuses.Add(new Mstatus() { Code = "811", Status = "Label control" });
            this.Mstatuses.Add(new Mstatus() { Code = "812", Status = "Barcode not readable" });
            this.Mstatuses.Add(new Mstatus() { Code = "814", Status = "The printhead is down (film printer)" });
            this.Mstatuses.Add(new Mstatus() { Code = "815", Status = "The printhead is up (film printer)" });
            this.Mstatuses.Add(new Mstatus() { Code = "816", Status = "No power to printer" });
            this.Mstatuses.Add(new Mstatus() { Code = "817", Status = "Door control" });
            this.Mstatuses.Add(new Mstatus() { Code = "818", Status = "Emergency control" });
            this.Mstatuses.Add(new Mstatus() { Code = "821", Status = "Pneumatic Door" });
            this.Mstatuses.Add(new Mstatus() { Code = "822", Status = "No Air Pressure" });
            this.Mstatuses.Add(new Mstatus() { Code = "823", Status = "Tag Not Writable" });
            this.Mstatuses.Add(new Mstatus() { Code = "824", Status = "(Special for Tetra Pak)" });
            this.Mstatuses.Add(new Mstatus() { Code = "825", Status = "ACC Test Mode" });
            this.Mstatuses.Add(new Mstatus() { Code = "826", Status = "Warning Product hit control" });
            this.Mstatuses.Add(new Mstatus() { Code = "827", Status = "Error Product hit control" });
            this.Mstatuses.Add(new Mstatus() { Code = "828", Status = "Apply Timeout" });
            this.Mstatuses.Add(new Mstatus() { Code = "829", Status = "Part Pallet (if offline setup = 989)**" });
            this.Mstatuses.Add(new Mstatus() { Code = "830", Status = "Bad applicator type" });
            this.Mstatuses.Add(new Mstatus() { Code = "831", Status = "Peel roller down" });
            this.Mstatuses.Add(new Mstatus() { Code = "832", Status = "Cover off" });
            this.Mstatuses.Add(new Mstatus() { Code = "949", Status = "Waiting for label positioning" });
            this.Mstatuses.Add(new Mstatus() { Code = "950", Status = "Dynamic queue empty" });
            this.Mstatuses.Add(new Mstatus() { Code = "951", Status = "Dynamic / Bitmap queue full" });
            this.Mstatuses.Add(new Mstatus() { Code = "952", Status = "Dynamic queue calc. bitmap empty" });
            this.Mstatuses.Add(new Mstatus() { Code = "989", Status = "Offline (if setup = 989)**" });
            this.Mstatuses.Add(new Mstatus() { Code = "990", Status = "Calculation in progress" });
            this.Mstatuses.Add(new Mstatus() { Code = "991", Status = "Label printed" });
            this.Mstatuses.Add(new Mstatus() { Code = "992", Status = "Cylinder out" });
            this.Mstatuses.Add(new Mstatus() { Code = "993", Status = "Get label in PC" });
            this.Mstatuses.Add(new Mstatus() { Code = "994", Status = "Application in progress" });
            this.Mstatuses.Add(new Mstatus() { Code = "997", Status = "Reset in progress" });
            this.Mstatuses.Add(new Mstatus() { Code = "998", Status = "Printing in progress" });
            this.Mstatuses.Add(new Mstatus() { Code = "999", Status = "Ok, idle" });
        }

        private string GetMstatus(string code)
        {
            Mstatus mstatus = this.Mstatuses.Where(w => w.Code == code).First();
            if (mstatus != null) return mstatus.Status; else return "Unknow!";
        }
        #endregion Handle Status

        #endregion Thread
    }


    public class  Mstatus
    {
        public string Code {get; set;}
        public string Status { get; set; }
    }
}
