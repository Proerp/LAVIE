using System;
using System.Linq;
using System.Collections.Generic;

using AutoMapper;

using TotalModel.Models;

using TotalDTO.Commons;
using TotalDTO.Productions;
using TotalDTO.Sales;
using TotalDTO.Inventories;

using TotalSmartCoding.ViewModels.Productions;
using TotalSmartCoding.ViewModels.Sales;
using TotalSmartCoding.ViewModels.Inventories;
using TotalSmartCoding.ViewModels.Commons;
using TotalDTO.Generals;
using TotalSmartCoding.ViewModels.Generals;

namespace TotalSmartCoding.Libraries
{
    public static class AutoMapperConfig
    {
        public static void SetupMappings()
        {
            ////////https://github.com/AutoMapper/AutoMapper/wiki/Static-and-Instance-API

            Mapper.Initialize(cfg =>
            {
               



                cfg.CreateMap<Pickup, PickupViewModel>();
                cfg.CreateMap<Pickup, PickupDTO>();
                cfg.CreateMap<PickupPrimitiveDTO, Pickup>();
                cfg.CreateMap<PickupViewDetail, PickupDetailDTO>();
                cfg.CreateMap<PickupDetailDTO, PickupDetail>();

                cfg.CreateMap<GoodsReceipt, GoodsReceiptViewModel>();
                cfg.CreateMap<GoodsReceipt, GoodsReceiptDTO>();
                cfg.CreateMap<GoodsReceiptPrimitiveDTO, GoodsReceipt>();
                cfg.CreateMap<GoodsReceiptViewDetail, GoodsReceiptDetailDTO>();
                cfg.CreateMap<GoodsReceiptDetailDTO, GoodsReceiptDetail>();


                cfg.CreateMap<SalesOrder, SalesOrderViewModel>();
                cfg.CreateMap<SalesOrder, SalesOrderDTO>();
                cfg.CreateMap<SalesOrderPrimitiveDTO, SalesOrder>();
                cfg.CreateMap<SalesOrderViewDetail, SalesOrderDetailDTO>();
                cfg.CreateMap<SalesOrderDetailDTO, SalesOrderDetail>();


                cfg.CreateMap<DeliveryAdvice, DeliveryAdviceViewModel>();
                cfg.CreateMap<DeliveryAdvice, DeliveryAdviceDTO>();
                cfg.CreateMap<DeliveryAdvicePrimitiveDTO, DeliveryAdvice>();
                cfg.CreateMap<DeliveryAdviceViewDetail, DeliveryAdviceDetailDTO>();
                cfg.CreateMap<DeliveryAdviceDetailDTO, DeliveryAdviceDetail>();


                cfg.CreateMap<TransferOrder, TransferOrderViewModel>();
                cfg.CreateMap<TransferOrder, TransferOrderDTO>();
                cfg.CreateMap<TransferOrderPrimitiveDTO, TransferOrder>();
                cfg.CreateMap<TransferOrderViewDetail, TransferOrderDetailDTO>();
                cfg.CreateMap<TransferOrderDetailDTO, TransferOrderDetail>();


                cfg.CreateMap<GoodsIssue, GoodsIssueViewModel>();
                cfg.CreateMap<GoodsIssue, GoodsIssueDTO>();
                cfg.CreateMap<GoodsIssuePrimitiveDTO, GoodsIssue>();
                cfg.CreateMap<GoodsIssueViewDetail, GoodsIssueDetailDTO>();
                cfg.CreateMap<GoodsIssueDetailDTO, GoodsIssueDetail>();



                cfg.CreateMap<WarehouseAdjustment, WarehouseAdjustmentViewModel>();
                cfg.CreateMap<WarehouseAdjustment, WarehouseAdjustmentDTO>();
                cfg.CreateMap<WarehouseAdjustmentPrimitiveDTO, WarehouseAdjustment>();
                cfg.CreateMap<WarehouseAdjustmentViewDetail, WarehouseAdjustmentDetailDTO>();
                cfg.CreateMap<WarehouseAdjustmentDetailDTO, WarehouseAdjustmentDetail>();



                cfg.CreateMap<Batch, BatchViewModel>();
                cfg.CreateMap<Batch, BatchDTO>();
                cfg.CreateMap<BatchPrimitiveDTO, Batch>();

                cfg.CreateMap<BatchIndex, FillingData>();

                cfg.CreateMap<BatchMaster, BatchMasterViewModel>();
                cfg.CreateMap<BatchMaster, BatchMasterDTO>();
                cfg.CreateMap<BatchMasterPrimitiveDTO, BatchMaster>();

                cfg.CreateMap<BatchRepack, BatchRepackDTO>();

                cfg.CreateMap<Pack, PackViewModel>();
                cfg.CreateMap<Pack, PackDTO>();
                cfg.CreateMap<PackPrimitiveDTO, Pack>();


                cfg.CreateMap<Carton, CartonViewModel>();
                cfg.CreateMap<Carton, CartonDTO>();
                cfg.CreateMap<CartonPrimitiveDTO, Carton>();


                cfg.CreateMap<Pallet, PalletViewModel>();
                cfg.CreateMap<Pallet, PalletDTO>();
                cfg.CreateMap<PalletPrimitiveDTO, Pallet>();


                cfg.CreateMap<Repack, RepackViewModel>();
                cfg.CreateMap<Repack, RepackDTO>();
                cfg.CreateMap<RepackPrimitiveDTO, Repack>();

                cfg.CreateMap<Lavie, LavieViewModel>();
                cfg.CreateMap<Lavie, LavieDTO>();
                cfg.CreateMap<LaviePrimitiveDTO, Lavie>();
                cfg.CreateMap<LavieIndex, LavieIndexDTO>();
                
                cfg.CreateMap<Commodity, CommodityViewModel>();
                cfg.CreateMap<Commodity, CommodityDTO>();
                cfg.CreateMap<CommodityPrimitiveDTO, Commodity>();

                //cfg.CreateMap<Employee, EmployeeBaseDTO>();
                cfg.CreateMap<Customer, CustomerBaseDTO>();
                cfg.CreateMap<Customer, CustomerViewModel>();
                cfg.CreateMap<Customer, CustomerDTO>();
                cfg.CreateMap<CustomerPrimitiveDTO, Customer>();

                cfg.CreateMap<BinLocation, BinLocationViewModel>();
                cfg.CreateMap<BinLocation, BinLocationDTO>();
                cfg.CreateMap<BinLocationPrimitiveDTO, BinLocation>();

                cfg.CreateMap<Warehouse, WarehouseBaseDTO>();
                cfg.CreateMap<WarehouseAdjustmentType, WarehouseAdjustmentTypeBaseDTO>();
                cfg.CreateMap<UserAccessControl, UserAccessControlDTO>();

                cfg.CreateMap<Report, ReportViewModel>();
                cfg.CreateMap<Report, ReportDTO>();
                cfg.CreateMap<ReportPrimitiveDTO, Report>();

                cfg.CreateMap<ColumnMapping, ColumnMappingDTO>();
            });
        }
    }
}