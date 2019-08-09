namespace TotalBase.Enums
{
    public static class GlobalEnums
    {
        public static bool PrintLOT = false;

        public static bool OnTestRepackWithoutScanner = false; //4

        public static bool OnTestScanner = false; //1
        public static bool OnTestPalletScanner = false; //3 //WHEN REAL TEST WITHOUT PalletScanner

        public static bool OnTestPrinter = false; //2
        public static bool OnTestDigit = false; //WHEN REAL TEST WITHOUT DIGIT PRINTER
        public static bool OnTestZebra = false; //false: test zebra 1    //false: WILL SEND TEST PRINT WHEN CONNECTED

        public static bool SendToZebra = true; //true: test zebra 2

        public static bool OnTestCartonNoreadNow = false;
        public static bool OnTestPalletReceivedNow = false;


        public static int ReprintCartonID = 0;

        public static OptionSetting GlobalOptionSetting = new OptionSetting();
        public static string stringFieldRequired = "Input not correct. Please make sure all required fields are filled out correctly";



        public static int RootNode = 999999900; //1 billion
        public static int AncestorNode = 1000000000; //1 billion

        public static bool ERPConnected = false;

        public static int CalculatingTypeID = 0;

        public static bool VATbyRow = false;
        public static decimal VATPercent = 10;

        public static int rndQuantity = 0;
        public static int rndVolume = 2;
        public static int rndAmount = 0;
        public static int rndDiscountPercent = 1;

        public const string formatQuantity = "#,##0";
        public const string formatVolume = "#,##0.00";


        public static int rndWeight = 2;

        public enum ActiveOption
        {
            Active = 0,
            InActive = 1,
            Both = -1
        }


        public enum SubmitTypeOption
        {
            Save = 0, //Save and return (keep) current view
            Popup = 1, //Save popup windows
            Create = 3, //Save and the create new
            Closed = 9 //Save and close (return index view)
        };


        public enum NmvnTaskID
        {
            UnKnown = 0,

            Batch = 28106,
            BatchMaster = 28106888,

            Pack = 686868,
            Carton = 686868,
            Pallet = 686868,
            SmartCoding = 888888,
            Repack = 68686811,

            Lavie = 8006,
            Customer = 8001,
            Commodity = 8002,
            Promotion = 8003,
            Employee = 8005,
            BinLocation = 8008,

            PurchaseOrder = 8021,
            PurchaseInvoice = 8022,



            SalesOrder = 8032,
            DeliveryAdvice = 8035,
            GoodsIssue = 8037,



            SalesReturn = 8038,
            CreditNote = 8060,


            GoodsReceipt = 8077,
            WarehouseAdjustment = 8078,
            GoodsReceiptDetailAvailable = 800001,


            TransferOrder = 8071,

            Pickup = 8068,

            Report = 91976999

        };




        public enum MappingTaskID : int
        {
            Default = 0,
            BatchMaster = 1888,
            Lavie = 8006
        }












        public enum TaskID
        {
            EmployeeCategory = 1,
            EmployeeType = 2,
            EmployeeName = 3,



            ItemCategory = 111,
            ItemType = 112,
            ItemClass = 116,
            ItemBrand = 113,
            ItemPM_APM = 117,
            ItemGroup = 114,
            ItemCommodity = 115,


            CustomerCategory = 251,
            CustomerType = 252,
            CustomerChannel = 253,
            CustomerName = 254,
            CustomerGroup = 255,


            ListItemCategory = 111,
            ListItemCategoryVerifiable = 1118888,
            ListItemCategoryUnverifiable = 111888899,

            ListItemType = 112,
            ListItemTypeVerifiable = 1128888,
            ListItemTypeUnverifiable = 112888899,

            ListItemBrand = 113,
            ListItemBrandVerifiable = 1138888,
            ListItemBrandUnverifiable = 113888899,

            ListItemGroup = 114,
            ListItemGroupVerifiable = 1148888,
            ListItemGroupUnverifiable = 114888899,

            ListItemCommodity = 115,
            ListItemCommodityVerifiable = 1158888,
            ListItemCommodityUnverifiable = 115888899,

            ListItemClass = 116,
            ListItemClassVerifiable = 1168888,
            ListItemClassUnverifiable = 116888899,

            ListItemPM_APM = 117,
            ListItemPM_APMVerifiable = 1178888,
            ListItemPM_APMUnverifiable = 117888899,

            ListSalesmenCategory = 151,
            ListSalesmenCategoryVerifiable = 1518888,
            ListSalesmenCategoryUnverifiable = 151888899,

            ListSalesmenType = 152,
            ListSalesmenTypeVerifiable = 1528888,
            ListSalesmenTypeUnverifiable = 152888899,

            ListSalesmenName = 154,
            ListSalesmenNameVerifiable = 1548888,
            ListSalesmenNameUnverifiable = 154888899,

            ListSalesmenTitle = 156,
            ListSalesmenTitleVerifiable = 1568888,
            ListSalesmenTitleUnverifiable = 156888899,

            ListStaffCategory = 162001,
            ListStaffCategoryVerifiable = 1620018888,
            ListStaffCategoryUnverifiable = 1620018899,

            ListStaffName = 162002,
            ListStaffNameVerifiable = 16228888,
            ListStaffNameUnverifiable = 16228899,

            ListDistributorCategory = 211,
            ListDistributorCategoryVerifiable = 2118888,
            ListDistributorCategoryUnverifiable = 211888899,

            ListDistributorType = 212,
            ListDistributorTypeVerifiable = 2128888,
            ListDistributorTypeUnverifiable = 212888899,

            ListDistributorName = 214,
            ListDistributorNameVerifiable = 2148888,
            ListDistributorNameUnverifiable = 214888899,

            ListCustomerCategory = 251,
            ListCustomerCategoryVerifiable = 2518888,
            ListCustomerCategoryUnverifiable = 251888899,

            ListCustomerType = 252,
            ListCustomerTypeVerifiable = 2528888,
            ListCustomerTypeUnverifiable = 252888899,

            ListAddressArea = 253,
            ListAddressAreaVerifiable = 2538888,
            ListAddressAreaUnverifiable = 253888899,

            ListCustomerName = 254,
            ListCustomerNameVerifiable = 2548888,
            ListCustomerNameUnverifiable = 254888899,

            ListCustomerGroup = 255,
            ListCustomerGroupVerifiable = 2558888,
            ListCustomerGroupUnverifiable = 255888899,

            ListCustomerChannel = 256,
            ListCustomerChannelVerifiable = 2568888,
            ListCustomerChannelUnverifiable = 256888899,

            Quota = 314,
            QuotaVerifiable = 3148888,
            QuotaUnverifiable = 314888899,

            InMarket = 312,
            InMarketVerifiable = 3128888,
            InMarketUnverifiable = 312888899,

            InMarketImport = 31200,

            MarketingProgram = 109,
            MarketingProgramVerifiable = 1098888,
            MarketingProgramUnverifiable = 109888899,


            MarketingIncentive = 121,
            MarketingIncentiveVerifiable = 1218888,
            MarketingIncentiveUnverifiable = 121888899,

            MarketingPayment = 131,
            MarketingPaymentVerifiable = 1318888,
            MarketingPaymentUnverifiable = 131888899,

            MarketingMonitoring = 451,
            MarketingMonitoringVerifiable = 4518888,
            MarketingMonitoringUnverifiable = 451888899



        }











        public enum PrintDestination
        {
            PrintPreview = 0,
            Print = 1,
            Export = 2
        }





        public enum ReportTypeID
        {
            GoodsReceiptPivot = 1,
            GoodsIssuePivot = 2,

            GoodsReceiptJournal = 21,
            GoodsIssueJournal = 22,

            WarehouseForecast = 686,

            WarehouseJournal = 800,
            ProductionJournal = 1001
        }

        public enum ReportTabPageID
        {
            TabPageWarehouses = 1,
            TabPageCommodities = 2,
            TabPageCustomers = 3,
            TabPageWarehouseIssues = 4,
            TabPageWarehouseReceipts = 5,
            TabPageWarehouseAdjustmentTypes = 6,
            TabPageBatchMasters = 7,
            TabPageBatchTypes = 8,
            TabPageFillingLines = 9
        }

        public enum OptionBoxID
        {
            FromDate = 1,
            ToDate = 2,
            SummaryVersusDetail = 10,
            QuantityVersusVolume = 20,
            DateVersusMonth = 30,
            SalesVersusPromotion = 50,
            PalletVersusCartonAndPack = 80,
            ForecastFilters = 60,
            SlowMoving = 70
        }//NOTES: OBx(OptionBoxID optionBoxID): MUST BE NOT DUPLICATE ANY PART

        public static string OBx(OptionBoxID optionBoxID)
        {
            return ((int)optionBoxID).ToString() + "X";
        }

        public enum ReportID
        {
            GoodsReceiptPivot = 1,
            ProductionReceiptPivot = 2,
            TransferReceiptPivot = 5,
            AdjustmentReceiptPivot = 6,

            GoodsIssuePivot = 11,
            SalesIssuePivot = 12,
            SalesIssuePivotbyCustomers = 17,
            TransferIssuePivot = 15,
            AdjustmentIssuePivot = 16,

            GoodsReceiptJournal = 21,
            ProductionReceiptJournal = 22,
            TransferReceiptJournal = 25,
            AdjustmentReceiptJournal = 26,

            GoodsIssueJournal = 31,
            SalesIssueJournal = 32,
            TransferIssueJournal = 35,
            AdjustmentIssueJournal = 36,



            PivotStockDIOH3M = 812,
            PivotStockDRP = 815,
            PivotStockDIOH3MAndDRP = 817,

            CurrentWarehouse = 832,


            SaleAndProduction = 868,
            OldAndSlowMoving = 869,

            WarehouseJournal = 888,

            ProductionJournal001 = 1001,
            ProductionJournal002 = 1002,
            ProductionJournal003 = 1003,
            ProductionJournal004 = 1004,
            ProductionJournal005 = 1005,

            ProductionJournal008 = 1008,
            ProductionJournal009 = 1009
        }

        public enum ForecastFilterID
        {
            None = -1,
            SlowMoving = 999999,
            SlowMovingNoForecast = 888888
        }



        public enum TransferPackageTypeID
        {
            All = 999,
            Pallets = 1,
            Cartons = 2
        };

        public enum GoodsReceiptTypeID
        {
            AllGoodsReceipt = 999,
            Pickup = 1,
            PurchaseInvoice = 2,
            GoodsIssueTransfer = 3,
            SalesReturn = 5,
            WarehouseAdjustments = 6
        };

        public enum GoodsIssueTypeID
        {//AT NOW, IN REPORT FILTER, WE CHECK: @LocalGoodsIssueTypeIDs LIKE '%" + (int)GlobalEnums.GoodsIssueTypeID.WarehouseAdjustment + "%'
            //SO: THIS ENUM NUMBER MUST BE SEPARATELY. IT SHOULD NOT CONTAIN EACH OTHERS (EX: SHOULD NOT BE: 1 Vs 199/ 9 Vs 199)
            DeliveryAdvice = 1,
            TransferOrder = 2,

            WarehouseAdjustment = 99
        };

        public enum GoodsIssueTypeID_REPORTONLY
        { //THIS GoodsIssueTypeID_REPORTONLY IS MAPPED FROM GoodsIssueTypeID FOR REPORT FILTER ONLY
            //IMPORTANT: SOMETIME WE USE goodsIssueTypeID_REPORTONLY.ToString().Substring(0, 1) => SO: SHOULD NAME WITH THE FIRST CHAR DIFFERENTLY!!!
            Alls = 1976999,
            CombineSelectedAlls = 1976888,
            GoodsIssues = 19761,
            SelectedGoodsIssues = 19762,
            WarehouseAdjustments = 197699
        };



        public enum WarehouseAdjustmentTypeID
        {
            UnpackPallet = 1,
            ChangeBinLocation = 10,
            HoldUnHold = 20,
            ReturnToProduction = 30,
            OtherIssues = 90
        };


        public enum RoleID
        {
            Production = 1,
            Logistic = 2,
            Saleperson = 3
        };












        public enum BatchTypeID
        {
            New = 1,
            Repack = 2,
            Trial = 3
        }

        public enum SalesInvoiceTypeID
        {
            AllInvoice = 1,
            VehiclesInvoice = 10,
            PartsInvoice = 20,
            ServicesInvoice = 30
        };

        public enum StockTransferTypeID
        {
            VehicleTransfer = 10,
            PartTransfer = 20
        };

        public enum ServiceContractTypeID
        {
            Warranty = 1,
            Repair = 2,
            Maintenance = 3
        };



        public enum CommodityTypeID
        {
            All = 999,
            Vehicles = 1,
            Parts = 2,
            Consumables = 3,
            Services = 6,
            CreditNote = 8,
            Unknown = 99
        };

        public enum WarehouseTaskID
        {
            SalesOrder = 8032,
            DeliveryAdvice = 8035,
            SalesReturn = 8038
        };

        public enum ReceiptTypeID
        {
            ReceiveMoney = 1,
            ApplyCredit = 2
        };


        public enum UpdateWarehouseBalanceOption
        {
            Positive = 1,
            Negative = -1
        };


        public enum AccessLevel
        {
            Deny = 0,
            Readable = 1,
            Editable = 2
        };
    }

}
