using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TotalModel.Helpers;

using TotalBase;
using System.ComponentModel;
using TotalBase.Enums;
using Equin.ApplicationFramework;

namespace TotalDTO.Productions
{
    public class FillingData : NotifyPropertyChangeObject
    {

        public int NoSubQueue { get { return 1; } } //!this.HasPack || (this.PackPerCarton / 4) == 0 ? 1 : (this.PackPerCarton / 4) //////GlobalVariables.NoSubQueue()
        public int ItemPerSubQueue { get { return GlobalVariables.NoItemDiverter(); } }
        public bool RepeatSubQueueIndex { get { return GlobalVariables.RepeatedSubQueueIndex(); } }



        public int CartonsetQueueCount { get; set; }
        public GlobalVariables.ZebraStatus CartonsetQueueZebraStatus { get; set; } //AT INITIALIZE, THIS = 0 (RIGHT AFTER CartonsetQueue IS SET). AFTER PRINT: THIS = 1. WHEN USER PRESS RE-PRINT => THIS = -1



        private int commodityID;
        private string commodityCode;
        private string commodityAPICode;
        private string commodityOfficialCode;
        private string commodityCartonCode;
        private string commodityName;
        private decimal volume;

        private int shelflife;
        private bool isPailLabel;

        private string batchTypeCode;

        private string batchCode;
        private string lotCode;
        private DateTime entryDate;
        private DateTime settingDate;

        private string nextDigitNo;
        private string nextPackNo;
        private string nextCartonNo;
        private string nextPalletNo;

        private string remarks;


        #region Contructor

        public FillingData()
        {
            this.settingDate = DateTime.Now;

            this.BatchRepacks = new BindingList<BatchRepackDTO>();

            this.BatchRepackViews = new BindingListView<BatchRepackDTO>(this.BatchRepacks);
            //this.BatchRepackViews.ApplyFilter(f => f.PrintedTimes == 0);
        }

        #endregion Contructor


        #region Public Properties


        public GlobalVariables.FillingLine FillingLineID { get { return GlobalVariables.FillingLineID; } }
        public string FillingLineCode { get { return GlobalVariables.FillingLineCode; } }
        public string FillingLineName { get { return GlobalVariables.FillingLineName; } }
        public string FillingLineFactoryCode { get { return GlobalVariables.FillingLineFactoryCode; } }

        public bool HasPack { get { return this.FillingLineID == GlobalVariables.FillingLine.Smallpack || this.FillingLineID == GlobalVariables.FillingLine.Pail; } }
        public bool HasCarton { get { return this.FillingLineID == GlobalVariables.FillingLine.Smallpack || this.FillingLineID == GlobalVariables.FillingLine.Pail; } }
        public bool HasPallet { get { return true; } }


        public int CommodityID    //ResetSerialNumber
        {
            get { return this.commodityID; }
            set
            {
                if (this.commodityID != value)
                {
                    ApplyPropertyChange<FillingData, int>(ref this.commodityID, o => o.CommodityID, value);

                    //DataTable dataTableFillingLineData = SQLDatabase.GetDataTable("SELECT BatchNo, LastPackNo, LastPackNo, LastCartonNo, MonthCartonNumber FROM FillingLineData WHERE FillingLineID = " + (int)this.FillingLineID + " AND ProductID = " + this.ProductID + " AND SettingMonthID = " + this.SettingMonthID);
                    //if (dataTableFillingLineData.Rows.Count > 0)
                    //    this.ResetSerialNumber(dataTableFillingLineData.Rows[0]["BatchNo"].ToString() == this.BatchNo ? dataTableFillingLineData.Rows[0]["NextPackNo"].ToString() : "000001", dataTableFillingLineData.Rows[0]["NextPackNo"].ToString(), dataTableFillingLineData.Rows[0]["BatchNo"].ToString() == this.BatchNo ? dataTableFillingLineData.Rows[0]["NextCartonNo"].ToString() : "900001", dataTableFillingLineData.Rows[0]["MonthCartonNumber"].ToString());
                    //else
                    //    this.ResetSerialNumber("000001", "000001", "900001", "900001");
                }
            }
        }


        public string CommodityCode
        {
            get { return this.commodityCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityCode, o => o.CommodityCode, value); }
        }

        public string CommodityAPICode
        {
            get { return this.commodityAPICode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityAPICode, o => o.CommodityAPICode, value); }
        }

        public string CommodityOfficialCode
        {
            get { return this.commodityOfficialCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityOfficialCode, o => o.CommodityOfficialCode, value); }
        }

        public string CommodityCartonCode
        {
            get { return this.commodityCartonCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityCartonCode, o => o.CommodityCartonCode, value); }
        }

        public string CommodityName
        {
            get { return this.commodityName; }
            set { ApplyPropertyChange<FillingData, string>(ref this.commodityName, o => o.CommodityName, value); }
        }

        public decimal Volume
        {
            get { return this.volume; }
            set { ApplyPropertyChange<FillingData, decimal>(ref this.volume, o => o.Volume, value); }
        }



        public int Shelflife
        {
            get { return this.shelflife; }
            set { ApplyPropertyChange<FillingData, int>(ref this.shelflife, o => o.Shelflife, value); }
        }

        public bool IsPailLabel
        {
            get { return this.isPailLabel; }
            set { ApplyPropertyChange<FillingData, bool>(ref this.isPailLabel, o => o.IsPailLabel, value); }
        }

        private bool reprintCarton;
        public bool ReprintCarton
        {
            get { return this.reprintCarton; }
            set { ApplyPropertyChange<FillingData, bool>(ref this.reprintCarton, o => o.ReprintCarton, value); }
        }

        public int BatchTypeID { get; set; }
        public string BatchTypeCode
        {
            get { return this.batchTypeCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.batchTypeCode, o => o.BatchTypeCode, value); }
        }


        //-------------------------
        private int batchID;
        public int BatchID
        {
            get { return this.batchID; }
            set { ApplyPropertyChange<FillingData, int>(ref this.batchID, o => o.BatchID, value); }
        }

        public string BatchCode   //ResetSerialNumber
        {
            get { return this.batchCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.batchCode, o => o.BatchCode, value); }
        }

        public string LotCode   //ResetSerialNumber
        {
            get { return this.lotCode; }
            set { ApplyPropertyChange<FillingData, string>(ref this.lotCode, o => o.LotCode, value); }
        }

        public DateTime EntryDate
        {
            get { return this.entryDate; }
            set { ApplyPropertyChange<FillingData, DateTime>(ref this.entryDate, o => o.EntryDate, value); }
        }

        public DateTime SettingDate
        {
            get { return this.settingDate; }
            set
            {
                if (this.settingDate != value)
                {
                    ApplyPropertyChange<FillingData, DateTime>(ref this.settingDate, o => o.SettingDate, value);
                    NotifyPropertyChanged("SettingDateShortDateFormat");
                    //this.SettingMonthID = GlobalStaticFunction.DateToContinuosMonth(this.SettingDate);
                }
            }
        }

        public string SettingDateShortDateFormat { get { return this.settingDate.ToShortDateString(); } }



        public bool IsFV { get { return CommonExpressions.IsFV(this.CommodityCode); } }

        public bool Is2D_FV
        {
            get { return this.IsFV && this.printerName != GlobalVariables.PrinterName.PalletLabel; }
        }

        public bool Is2D_Repack
        {
            get { return (this.printerName == GlobalVariables.PrinterName.PackInkjet || (this.printerName == GlobalVariables.PrinterName.CartonInkjet && this.ReprintCarton)) && this.BatchTypeID == (int)GlobalEnums.BatchTypeID.Repack; }
        }
        //-------------------------

        public string NextDigitNo
        {
            get { return this.nextDigitNo; }

            set
            {
                if (value != this.nextDigitNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextDigitNo, o => o.NextDigitNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string NextPackNo
        {
            get { return this.nextPackNo; }

            set
            {
                if (value != this.nextPackNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextPackNo, o => o.NextPackNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }

        public string NextCartonNo
        {
            get { return this.nextCartonNo; }

            set
            {
                if (value != this.nextCartonNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextCartonNo, o => o.NextCartonNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }


        public string NextPalletNo
        {
            get { return this.nextPalletNo; }

            set
            {
                if (value != this.nextPalletNo)
                {
                    int intValue = 0;
                    if (int.TryParse(value, out intValue) && value.Length == 6)
                    {
                        ApplyPropertyChange<FillingData, string>(ref this.nextPalletNo, o => o.NextPalletNo, value);
                    }
                    else
                    {
                        throw new System.InvalidOperationException("Lỗi sai định dạng số đếm");
                    }
                }
            }
        }


        //-------------------------



        public string Remarks
        {
            get { return this.remarks; }
            set { ApplyPropertyChange<FillingData, string>(ref this.remarks, o => o.Remarks, value); }
        }













        private int packPerCarton;
        public int PackPerCarton
        {
            get { return this.packPerCarton; }
            set { ApplyPropertyChange<FillingData, int>(ref this.packPerCarton, o => o.PackPerCarton, value); }
        }


        private int cartonPerPallet;
        public int CartonPerPallet
        {
            get { return this.cartonPerPallet; }
            set { ApplyPropertyChange<FillingData, int>(ref this.cartonPerPallet, o => o.CartonPerPallet, value); }
        }









        #endregion Public Properties

        #region Method

        public FillingData ShallowClone()
        {
            return (FillingData)this.MemberwiseClone();
        }

        private void ResetSerialNumber(string batchSerialNumber, string monthSerialNumber, string LastCartonNo, string monthCartonNumber)
        {
            if (this.NextPackNo != monthSerialNumber) this.NextPackNo = monthSerialNumber;
            if (this.NextCartonNo != LastCartonNo) this.NextCartonNo = LastCartonNo;
        }

        public bool DataValidated()
        {
            return this.FillingLineID != 0 && this.CommodityID != 0 && this.BatchCode != "" && this.LotCode != "" && this.NextPackNo != "" && this.NextCartonNo != "" && this.NextPalletNo != "";
        }


        public bool Save()
        {
            return true;
            try
            {
                //int rowsAffected = ADODatabase.ExecuteNonQuery("UPDATE FillingLineData SET IsDefault = 1 WHERE FillingLineID = " + (int)this.FillingLineID + " AND ProductID = " + this.ProductID);


                //if (rowsAffected <= 0) //Add New
                //{
                //    rowsAffected = ADODatabase.ExecuteTransaction("UPDATE FillingLineData SET IsDefault = 0 WHERE FillingLineID = " + (int)this.FillingLineID + "; " +
                //                                                  "INSERT INTO FillingLineData (FillingLineID, ProductID, BatchNo, SettingDate, SettingMonthID, LastPackNo, LastPackNo, LastCartonNo, MonthCartonNumber, Remarks, LastSettingDate, LastSerialDate, IsDefault) " +
                //                                                  "VALUES (" + (int)this.FillingLineID + ", " + this.ProductID + ", N'" + this.BatchNo + "', CONVERT(smalldatetime, '" + this.SettingDate.ToString("dd/MM/yyyy") + "',103), " + this.SettingMonthID.ToString() + ", N'" + this.LastPackNo + "', N'" + this.LastPackNo + "', N'" + this.LastCartonNo + "', N'" + this.MonthCartonNumber + "', N'" + this.Remarks + "', GetDate(), GetDate(), 1) ");

                //    return rowsAffected > 0;
                //}

                //else //Update Only
                //{
                //    return Update();
                //}
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Method















        public BatchRepackDTO printedBatchRepackDTO
        {
            get { return this.BatchRepacks.Where(w => w.LineIndex == (this.RepackSentIndex == this.BatchRepacks.Count ? this.RepackSentIndex : this.RepackSentIndex + 1)).FirstOrDefault(); } //SEND DOUBLE TIME FOR THE LAST
        }

        private DateTime printedEntryDate
        {
            get
            {
                if (this.Is2D_Repack)
                    return (DateTime)this.printedBatchRepackDTO.EntryDate;
                else
                    return this.EntryDate;
            }
        }







        /// <summary>
        /// Cách  tính: ngày/ tháng/ năm sx  + Expiration Months.  
        /// Trong trườn hợp  ngày hết HSD cùa SP vào những ngày không có trong lịch như ngày 30, 31 của tháng 02 hay vào ngày 29/02 của năm không nhuận , hoặc ngày 31 của tháng 4,6,9, 11: thực hiện HSD sẽ là ngày đầu của tháng  tiếp theo.  
        /// Ví dụ  : 
        /// Ngày sx : 29/02/2016  ;   Hạn sử dụng : ( 24 tháng) : 01/03/2018
        /// Ngày sx : 31/05/2016 ;    Hạn sử dụng : (18 tháng) : 01/12/2018
        /// </summary>
        /// <param name="isReadableText"></param>
        /// <param name="yyyy"></param>
        /// <returns></returns>
        public string FirstLineA1(bool isReadableText, bool yyyy)
        {
            return isReadableText ? this.printedEntryDate.AddDays(1 - this.printedEntryDate.Day).AddMonths(this.Shelflife).AddDays(-1 + this.printedEntryDate.Day + (this.IsFV ? -1 : 0)).ToString(yyyy ? "dd.MM.yyyy" : "ddMMyy") : "";
        }

        public string FirstLineA2(bool isReadableText)
        {
            if (this.Is2D_Repack)
                return (!isReadableText || this.Is2D_FV ? this.printedBatchRepackDTO.FillingLineCode : "") + this.FillingLineFactoryCode;
            else
                return (!isReadableText || this.Is2D_FV ? this.FillingLineCode : "") + this.FillingLineFactoryCode;
        }

        ////public string FirstLineA2(bool isReadableText)
        ////{
        ////    if (this.Is2D_Repack)
        ////        return this.printedBatchRepackDTO.FillingLineCode + this.FillingLineFactoryCode;
        ////    else
        ////        return this.FillingLineCode + this.FillingLineFactoryCode;
        ////}

        public string FirstLineA2B(bool isReadableText)
        {
            return this.BatchTypeCode + (isReadableText ? "" : this.CommodityAPICode);
        }


        public string SecondLineA1(bool isReadableText)
        {
            return this.printedEntryDate.ToString("ddMMyy");
        }

        public string SecondLineA2(bool isReadableText)
        {
            return "";
        }

        public string ThirdLineA1(bool isReadableText)
        {
            if (this.Is2D_Repack)
            {
                BatchRepackDTO batchRepackDTO = this.printedBatchRepackDTO;
                return batchRepackDTO.BatchCode + batchRepackDTO.LotCode + (isReadableText ? this.printedBatchRepackDTO.FillingLineCode : "");
            }
            else
                return this.BatchCode + this.LotCode + (isReadableText ? this.FillingLineCode : "");
        }

        public string GetLotCode()
        {
            if (this.Is2D_Repack) //GetLotCode() IS THE SAME AS ThirdLineA1
                return this.printedBatchRepackDTO.LotCode;
            else
                return this.LotCode;
        }

        ////public string ThirdLineA1(bool isReadableText)
        ////{
        ////    if (this.Is2D_Repack)
        ////    {
        ////        BatchRepackDTO batchRepackDTO = this.printedBatchRepackDTO;
        ////        return batchRepackDTO.BatchCode + batchRepackDTO.LotCode;
        ////    }
        ////    else
        ////        return this.BatchCode + this.LotCode;
        ////}




        public GlobalVariables.PrinterName printerName;
        public BindingList<BatchRepackDTO> BatchRepacks { get; set; }
        public BindingListView<BatchRepackDTO> BatchRepackViews { get; private set; }

        public int RepackSentIndex { get; set; }
        public int RepackPrintedIndex { get; set; }


        public int RepackPrintedID
        {
            get { return this.BatchRepacks.Where(w => w.LineIndex == this.RepackPrintedIndex).FirstOrDefault().RepackID; }
        }




    }
}
