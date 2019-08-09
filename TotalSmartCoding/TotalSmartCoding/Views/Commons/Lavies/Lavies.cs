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

            //this.ionetSocket = new IONetSocket(IPAddress.Parse("10.208.14.100"), 9100, false);
            //this.ioserialPort = new IOSerialPort(GlobalVariables.ComportName, 115200, Parity.None, 8, StopBits.One, false, "MSERIES");

            //this.ioserialPort.PropertyChanged += new PropertyChangedEventHandler(ioserialPort_PropertyChanged);
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






        private Thread lavieThread;
        delegate void propertyChangedThread(object sender, int lavieID);

        //private IONetSocket ionetSocket;
        //private IOSerialPort ioserialPort;

        private bool OnRunning { get; set; }

        public void ThreadRoutine()
        {
            try
            {
                //this.ionetSocket.Connect();
                //this.ioserialPort.Connect();

                this.OnRunning = true;
                string curFile = @"c:\temp\PrintGo.txt"; string finishFile = @"c:\temp\Finish.txt";

                if (!File.Exists(finishFile)) System.IO.File.WriteAllText(finishFile, "");

                int lavieIndex = this.LavieIndexes.Where(w => w.PrintedTimes == 0).First().LineIndex; int i = 0;

                this.ABC(this.buttonStart, 0);

                while (this.OnRunning)
                {
                    if (!File.Exists(curFile) && File.Exists(finishFile) && lavieIndex < (this.LavieIndexes.Count + 1))
                    {
                        //CHECK PRINTER READY
                        //this.ionetSocket.WritetoStream(GlobalVariables.charESC + "/S/001/" + "stringMessage" + "/" + GlobalVariables.charEOT);
                        //this.ioserialPort.WritetoSerial(GlobalVariables.charSTX + "30/30/30/3F/3F" + GlobalVariables.charCR, true);
                        //this.labelProgress.Text = "SEND OK";
                        //Thread.Sleep(1000);
                        //if (this.waitforMSeries())
                        //{
                            if (i++ <= 1 || this.lavieAPIs.SystemInfoValidate())
                            {
                                File.Delete(finishFile);

                                LavieIndexDTO lavieIndexDTO = this.LavieIndexes[lavieIndex - 1]; lavieIndex++;

                                //"PrinterIP=10.208.14.100:9100", 
                                string[] lines = { "[Label 1]", "Label=LABEL", "Quantity=0", "Dyn01=" + lavieIndexDTO.ItemNumber, "Dyn02=" + lavieIndexDTO.ProductName, "Dyn03=" + lavieIndexDTO.GTIN, "Dyn04=" + lavieIndexDTO.PalletID, "Dyn05=" + lavieIndexDTO.BatchNumber, "Dyn06=" + ((DateTime)lavieIndexDTO.ExpirationDate).ToString("dd/MM/yyyy"), "Dyn07=" + ((decimal)lavieIndexDTO.Qty).ToString("N0"), "Dyn08=" + ((decimal)lavieIndexDTO.Layers).ToString("N0"), "Dyn09=" + lavieIndexDTO.GTINBarcode, "Dyn10=" + lavieIndexDTO.Barcode, "Dyn11=" + lavieIndexDTO.SerialID };
                                //string[] lines = { "[Label 1]", "PrinterIP=101.208.14.100:9100", "Label=LABEL", "Quantity=0", "Dyn01=" + lavieIndexDTO.ItemNumber, "Dyn02=" + lavieIndexDTO.ProductName, "Dyn03=" + lavieIndexDTO.GTIN, "Dyn04=" + lavieIndexDTO.PalletID, "Dyn05=" + lavieIndexDTO.BatchNumber, "Dyn06=" + ((DateTime)lavieIndexDTO.ExpirationDate).ToString("dd/MM/yyyy"), "Dyn07=" + ((decimal)lavieIndexDTO.Qty).ToString("N0"), "Dyn08=" + ((decimal)lavieIndexDTO.Layers).ToString("N0"), "Dyn09=" + lavieIndexDTO.GTINBarcode, "Dyn10=" + lavieIndexDTO.Barcode, "Dyn11=" + lavieIndexDTO.SerialID, SystemInfos.GetSystemInfos(true) };

                                System.IO.File.WriteAllLines(curFile, lines);

                                this.lavieAPIs.LavieUpdate(lavieIndexDTO.LavieID);
                                this.ABC(this, lavieIndexDTO.LavieID);
                            }
                            else
                            { throw new Exception(); }
                        //}
                    }

                    Thread.Sleep(1000);
                }
            }
            catch (Exception exception)
            {
                this.labelProgress.Text = exception.Message;
                this.buttonStop_Click(this, new EventArgs());
            }
            finally
            {
                //this.ionetSocket.Disconnect();
                //this.ioserialPort.Disconnect();
                this.ABC(this.buttonStop, 0);
            }
        }


        private bool waitforMSeries()
        {
            try
            {
                //string receivedFeedback = this.ionetSocket.ReadoutStream();
                string stringReadFrom = "";
                //this.ioserialPort.ReadoutSerial(false, ref stringReadFrom, "Autonis", 0);
                this.labelProgress.Text = "M:" + stringReadFrom;
                if (stringReadFrom == "A")
                    return true;
                else return false;
            }

            catch (Exception exception)
            {
                throw exception;
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
                    this.labelProgress.Text = e.PropertyName + ": " + sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null).ToString();
            }
            catch (Exception exception)
            {
                this.labelProgress.Text = exception.Message;
            }
        }







        private void ABC(object sender, int lavieID)
        {
            try
            {
                propertyChangedThread propertyChangedDelegate = new propertyChangedThread(propertyChangedHandler);
                this.Invoke(propertyChangedDelegate, new object[] { sender, lavieID });
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
        }

        private void propertyChangedHandler(object sender, int lavieID)
        {
            try
            {
                this.buttonStart.Enabled = !this.OnRunning;
                this.buttonStop.Enabled = this.OnRunning;

                if (!sender.Equals(this.buttonStart) && !sender.Equals(this.buttonStop))
                {
                    this.LavieIndexes.Where(w => w.LavieID == lavieID).Each(batchRepackDTO =>
                        {
                            batchRepackDTO.PrintedTimes = 1;
                            this.labelProgress.Text = "Printing: No: " + batchRepackDTO.SerialID + "; Pallet ID: " + batchRepackDTO.PalletID;
                        });
                }
                return;
            }
            catch (Exception exception)
            {
                ExceptionHandlers.ShowExceptionMessageBox(this, exception);
            }
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


    }
}
