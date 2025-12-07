using AutoMapper;
using Sample.ApplicationService.DTOs;
using Sample.Domain.Entities;

namespace Sample.Web
{
    public class WebMappingProfile : Profile
    {
        public WebMappingProfile()
        {
            CreateMap<CustomerDto, Customer>().ReverseMap();
            CreateMap<CustomerImageDto, CustomerImage>().ReverseMap();
            CreateMap<DeliveryReceiptDetailDto, DeliveryReceiptDetail>().ReverseMap();
            CreateMap<DeliveryReceiptMasterDto, DeliveryReceiptMaster>().ReverseMap();
            CreateMap<EmployeeDto, Employee>().ReverseMap();
            CreateMap<EmployeeApproverDto, EmployeeApprover>().ReverseMap();
            CreateMap<EmployeeCheckerDto, EmployeeChecker>().ReverseMap();
            CreateMap<EmployeeDeliveredDto, EmployeeDelivered>().ReverseMap();
            CreateMap<EmployeeImageDto, EmployeeImage>().ReverseMap();
            CreateMap<EmployeeSalesRefDto, EmployeeSalesRef>().ReverseMap();
            CreateMap<ItemDto, Item>().ReverseMap();
            CreateMap<ItemDetailDto, ItemDetail>().ReverseMap();
            CreateMap<ItemImageDto, ItemImage>().ReverseMap();
            CreateMap<ItemSupplierDto, ItemSupplier>().ReverseMap();
            CreateMap<ItemUnitMeasureDto, ItemUnitMeasure>().ReverseMap();
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<OfficialReceiptDetailDto, OfficialReceiptDetail>().ReverseMap();
            CreateMap<OfficialReceiptMasterDto, OfficialReceiptMaster>().ReverseMap();
            CreateMap<PurchaseOrderDetailDto, PurchaseOrderDetail>().ReverseMap();
            CreateMap<PurchaseOrderMasterDto, PurchaseOrderMaster>().ReverseMap();
            CreateMap<PurchaseRequisitionDetailDto, PurchaseRequisitionDetail>().ReverseMap();
            CreateMap<PurchaseRequisitionMasterDto, PurchaseRequisitionMaster>().ReverseMap();
            CreateMap<PurchaseReturnDetailDto, PurchaseReturnDetail>().ReverseMap();
            CreateMap<PurchaseReturnMasterDto, PurchaseReturnMaster>().ReverseMap();
            CreateMap<ReceivingReportDetailDto, ReceivingReportDetail>().ReverseMap();
            CreateMap<ReceivingReportMasterDto, ReceivingReportMaster>().ReverseMap();
            CreateMap<SalesInvoiceDetailDto, SalesInvoiceDetail>().ReverseMap();
            CreateMap<SalesInvoiceMasterDto, SalesInvoiceMaster>().ReverseMap();
            CreateMap<SalesOrderDetailDto, SalesOrderDetail>().ReverseMap();
            CreateMap<SalesOrderMasterDto, SalesOrderMaster>().ReverseMap();
            CreateMap<SalesReturnDetailDto, SalesReturnDetail>().ReverseMap();
            CreateMap<SalesReturnMasterDto, SalesReturnMaster>().ReverseMap();
            CreateMap<SupplierDto, Supplier>().ReverseMap();
            CreateMap<SupplierImageDto, SupplierImage>().ReverseMap();
            CreateMap<WarehouseDto, Warehouse>().ReverseMap();
        }
    }
}
