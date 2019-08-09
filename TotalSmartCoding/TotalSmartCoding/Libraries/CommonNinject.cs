using Ninject;

using TotalCore.Repositories;
using TotalModel.Models;

//using TotalModel.Models;

//using TotalCore.Repositories;
//using TotalCore.Repositories.Inventories;

//using TotalCore.Services.Inventories;

using TotalDAL.Repositories;
using TotalCore.Repositories.Commons;
using TotalDAL.Repositories.Commons;
using TotalService.Inventories;
using TotalCore.Services.Inventories;
using TotalDAL.Repositories.Inventories;
using TotalCore.Repositories.Inventories;
using TotalSmartCoding.ViewModels.Inventories;
using TotalCore.Services.Productions;
using TotalService.Productions;
using TotalDAL.Repositories.Productions;
using TotalCore.Repositories.Productions;
using TotalDTO.Productions;
using TotalSmartCoding.ViewModels.Productions;
using TotalCore.Services.Sales;
using TotalService.Sales;
using TotalDAL.Repositories.Sales;
using TotalCore.Repositories.Sales;
using TotalSmartCoding.ViewModels.Sales;
using TotalCore.Repositories.Generals;
using TotalDAL.Repositories.Generals;
using TotalCore.Services.Commons;
using TotalSmartCoding.ViewModels.Commons;
using TotalService.Commons;
using TotalDTO.Inventories;
using TotalCore.Services.Generals;
using TotalService.Generals;
using TotalSmartCoding.ViewModels.Generals;
//using TotalDAL.Repositories.Inventories;

//using TotalService.Inventories;


namespace TotalSmartCoding.Libraries
{
    public static class CommonNinject
    {
        public static readonly IKernel Kernel;

        /// <summary>
        ///  static constructor NinjectCommon is called automatically before the first instance is created or any static members are referenced
        /// </summary>
        static CommonNinject()
        {
            Kernel = new StandardKernel();
            try
            {
                //Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                //Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();


                Kernel.Bind<TotalSmartCodingEntities>().ToSelf();// NOW: WE USE DEFAULT: .InTransientScope(); //.InRequestScope()

                Kernel.Bind<IBaseRepository>().To<BaseRepository>();





                Kernel.Bind<IPickupService>().To<PickupService>();
                Kernel.Bind<IPickupRepository>().To<PickupRepository>();
                Kernel.Bind<IPickupAPIRepository>().To<PickupAPIRepository>();
                Kernel.Bind<PickupViewModel>().ToSelf();

                Kernel.Bind<IGoodsReceiptService>().To<GoodsReceiptService>();
                Kernel.Bind<IGoodsReceiptRepository>().To<GoodsReceiptRepository>();
                Kernel.Bind<IGoodsReceiptAPIRepository>().To<GoodsReceiptAPIRepository>();
                Kernel.Bind<GoodsReceiptViewModel>().ToSelf();
                Kernel.Bind<GoodsReceiptDetailAvailableDTO>().ToSelf();

                Kernel.Bind<ISalesOrderService>().To<SalesOrderService>();
                Kernel.Bind<ISalesOrderRepository>().To<SalesOrderRepository>();
                Kernel.Bind<ISalesOrderAPIRepository>().To<SalesOrderAPIRepository>();
                Kernel.Bind<SalesOrderViewModel>().ToSelf();

                Kernel.Bind<IDeliveryAdviceService>().To<DeliveryAdviceService>();
                Kernel.Bind<IDeliveryAdviceRepository>().To<DeliveryAdviceRepository>();
                Kernel.Bind<IDeliveryAdviceAPIRepository>().To<DeliveryAdviceAPIRepository>();
                Kernel.Bind<DeliveryAdviceViewModel>().ToSelf();

                Kernel.Bind<ITransferOrderService>().To<TransferOrderService>();
                Kernel.Bind<ITransferOrderRepository>().To<TransferOrderRepository>();
                Kernel.Bind<ITransferOrderAPIRepository>().To<TransferOrderAPIRepository>();
                Kernel.Bind<TransferOrderViewModel>().ToSelf();

                Kernel.Bind<IGoodsIssueService>().To<GoodsIssueService>();
                Kernel.Bind<IGoodsIssueRepository>().To<GoodsIssueRepository>();
                Kernel.Bind<IGoodsIssueAPIRepository>().To<GoodsIssueAPIRepository>();
                Kernel.Bind<GoodsIssueViewModel>().ToSelf();

                Kernel.Bind<IWarehouseAdjustmentService>().To<WarehouseAdjustmentService>();
                Kernel.Bind<IWarehouseAdjustmentRepository>().To<WarehouseAdjustmentRepository>();
                Kernel.Bind<IWarehouseAdjustmentAPIRepository>().To<WarehouseAdjustmentAPIRepository>();
                Kernel.Bind<WarehouseAdjustmentViewModel>().ToSelf();






                //Kernel.Bind<IOleDbService>().To<OleDbService>();
                //Kernel.Bind<IOleDbRepository>().To<OleDbRepository>();
                Kernel.Bind<IOleDbAPIRepository>().To<OleDbAPIRepository>();
                //Kernel.Bind<OleDbViewModel>().ToSelf();



                //Kernel.Bind<IUserService>().To<UserService>();
                Kernel.Bind<IUserRepository>().To<UserRepository>();
                Kernel.Bind<IUserAPIRepository>().To<UserAPIRepository>();
                //Kernel.Bind<UserViewModel>().ToSelf();


                //Kernel.Bind<IModuleService>().To<ModuleService>();
                Kernel.Bind<IModuleRepository>().To<ModuleRepository>();
                Kernel.Bind<IModuleAPIRepository>().To<ModuleAPIRepository>();
                //Kernel.Bind<ModuleViewModel>().ToSelf();

                Kernel.Bind<IReportService>().To<ReportService>();
                Kernel.Bind<IReportRepository>().To<ReportRepository>();
                Kernel.Bind<IReportAPIRepository>().To<ReportAPIRepository>();
                Kernel.Bind<ReportViewModel>().ToSelf();

                Kernel.Bind<ICustomerService>().To<CustomerService>();
                Kernel.Bind<ICustomerRepository>().To<CustomerRepository>();
                Kernel.Bind<ICustomerAPIRepository>().To<CustomerAPIRepository>();
                Kernel.Bind<CustomerViewModel>().ToSelf();

                Kernel.Bind<IBinLocationService>().To<BinLocationService>();
                Kernel.Bind<IBinLocationRepository>().To<BinLocationRepository>();
                Kernel.Bind<IBinLocationAPIRepository>().To<BinLocationAPIRepository>();
                Kernel.Bind<BinLocationViewModel>().ToSelf();

                Kernel.Bind<ILavieService>().To<LavieService>();
                Kernel.Bind<ILavieRepository>().To<LavieRepository>();
                Kernel.Bind<ILavieAPIRepository>().To<LavieAPIRepository>();
                Kernel.Bind<LavieViewModel>().ToSelf();

                Kernel.Bind<ICommodityService>().To<CommodityService>();
                Kernel.Bind<ICommodityRepository>().To<CommodityRepository>();
                Kernel.Bind<ICommodityAPIRepository>().To<CommodityAPIRepository>();
                Kernel.Bind<CommodityViewModel>().ToSelf();

                //Kernel.Bind<ICommodityCategoryService>().To<CommodityCategoryService>();
                Kernel.Bind<ICommodityCategoryRepository>().To<CommodityCategoryRepository>();
                Kernel.Bind<ICommodityCategoryAPIRepository>().To<CommodityCategoryAPIRepository>();
                //Kernel.Bind<CommodityCategoryViewModel>().ToSelf();

                //Kernel.Bind<ICommodityTypeService>().To<CommodityTypeService>();
                Kernel.Bind<ICommodityTypeRepository>().To<CommodityTypeRepository>();
                Kernel.Bind<ICommodityTypeAPIRepository>().To<CommodityTypeAPIRepository>();
                //Kernel.Bind<CommodityTypeViewModel>().ToSelf();

                //Kernel.Bind<IEmployeeService>().To<EmployeeService>();
                Kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
                Kernel.Bind<IEmployeeAPIRepository>().To<EmployeeAPIRepository>();
                //Kernel.Bind<EmployeeViewModel>().ToSelf();

                //Kernel.Bind<ITerritoryService>().To<TerritoryService>();
                Kernel.Bind<ITerritoryRepository>().To<TerritoryRepository>();
                Kernel.Bind<ITerritoryAPIRepository>().To<TerritoryAPIRepository>();
                //Kernel.Bind<TerritoryViewModel>().ToSelf();

                //Kernel.Bind<ICustomerCategoryService>().To<CustomerCategoryService>();
                Kernel.Bind<ICustomerCategoryRepository>().To<CustomerCategoryRepository>();
                Kernel.Bind<ICustomerCategoryAPIRepository>().To<CustomerCategoryAPIRepository>();
                //Kernel.Bind<CustomerCategoryViewModel>().ToSelf();

                //Kernel.Bind<ICustomerTypeService>().To<CustomerTypeService>();
                Kernel.Bind<ICustomerTypeRepository>().To<CustomerTypeRepository>();
                Kernel.Bind<ICustomerTypeAPIRepository>().To<CustomerTypeAPIRepository>();
                //Kernel.Bind<CustomerTypeViewModel>().ToSelf();

                //Kernel.Bind<IBatchTypeService>().To<BatchTypeService>();
                Kernel.Bind<IBatchTypeRepository>().To<BatchTypeRepository>();
                Kernel.Bind<IBatchTypeAPIRepository>().To<BatchTypeAPIRepository>();
                //Kernel.Bind<BatchTypeViewModel>().ToSelf();

                //Kernel.Bind<IBatchStatusService>().To<BatchStatusService>();
                Kernel.Bind<IBatchStatusRepository>().To<BatchStatusRepository>();
                Kernel.Bind<IBatchStatusAPIRepository>().To<BatchStatusAPIRepository>();
                //Kernel.Bind<BatchStatusViewModel>().ToSelf();

                Kernel.Bind<IVoidTypeRepository>().To<VoidTypeRepository>();

                //Kernel.Bind<ILocationService>().To<LocationService>();
                //Kernel.Bind<ILocationRepository>().To<LocationRepository>();
                Kernel.Bind<ILocationAPIRepository>().To<LocationAPIRepository>();
                //Kernel.Bind<LocationViewModel>().ToSelf();

                //Kernel.Bind<IWarehouseService>().To<WarehouseService>();
                Kernel.Bind<IWarehouseRepository>().To<WarehouseRepository>();
                Kernel.Bind<IWarehouseAPIRepository>().To<WarehouseAPIRepository>();
                //Kernel.Bind<WarehouseViewModel>().ToSelf();

                //Kernel.Bind<IWarehouseAdjustmentTypeService>().To<WarehouseAdjustmentTypeService>();
                Kernel.Bind<IWarehouseAdjustmentTypeRepository>().To<WarehouseAdjustmentTypeRepository>();
                Kernel.Bind<IWarehouseAdjustmentTypeAPIRepository>().To<WarehouseAdjustmentTypeAPIRepository>();
                //Kernel.Bind<WarehouseAdjustmentTypeViewModel>().ToSelf();

                //Kernel.Bind<ITransferOrderTypeService>().To<TransferOrderTypeService>();
                Kernel.Bind<ITransferOrderTypeRepository>().To<TransferOrderTypeRepository>();
                Kernel.Bind<ITransferOrderTypeAPIRepository>().To<TransferOrderTypeAPIRepository>();
                //Kernel.Bind<TransferOrderTypeViewModel>().ToSelf();


                

                //Kernel.Bind<IFillingLineService>().To<FillingLineService>();
                Kernel.Bind<IFillingLineRepository>().To<FillingLineRepository>();
                Kernel.Bind<IFillingLineAPIRepository>().To<FillingLineAPIRepository>();
                //Kernel.Bind<FillingLineViewModel>().ToSelf();






                Kernel.Bind<IBatchService>().To<BatchService>();
                Kernel.Bind<IBatchRepository>().To<BatchRepository>();
                Kernel.Bind<IBatchAPIRepository>().To<BatchAPIRepository>();
                Kernel.Bind<BatchViewModel>().ToSelf();

                Kernel.Bind<IBatchMasterService>().To<BatchMasterService>();
                Kernel.Bind<IBatchMasterRepository>().To<BatchMasterRepository>();
                Kernel.Bind<IBatchMasterAPIRepository>().To<BatchMasterAPIRepository>();
                Kernel.Bind<BatchMasterViewModel>().ToSelf();





                Kernel.Bind<IPackService>().To<PackService>();
                Kernel.Bind<IPackRepository>().To<PackRepository>();
                Kernel.Bind<PackViewModel>().ToSelf();

                Kernel.Bind<ICartonService>().To<CartonService>();
                Kernel.Bind<ICartonRepository>().To<CartonRepository>();
                Kernel.Bind<CartonViewModel>().ToSelf();

                Kernel.Bind<IPalletService>().To<PalletService>();
                Kernel.Bind<IPalletRepository>().To<PalletRepository>();
                Kernel.Bind<PalletViewModel>().ToSelf();


                Kernel.Bind<IRepackService>().To<RepackService>();
                Kernel.Bind<IRepackRepository>().To<RepackRepository>();
                Kernel.Bind<RepackViewModel>().ToSelf();


                Kernel.Bind<FillingData>().ToSelf();




            }
            catch
            {
                Kernel.Dispose();
                throw;
            }
        }
    }
}
