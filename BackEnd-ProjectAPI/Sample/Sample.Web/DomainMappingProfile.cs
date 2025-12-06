using AutoMapper;
//using Sample.Domain.Entities;
//using Sample.Infrastructure.EntityFramework;

using DomainEntity = Sample.Domain.Entities;
using Entity = Sample.Infrastructure.EntityFramework;

namespace Sample.Web
{
    public class DomainMappingProfile : Profile
    {
        public DomainMappingProfile()
        {

            CreateMap<DomainEntity.Employee, Entity.Employee>().ReverseMap();
            CreateMap<DomainEntity.Customer, Entity.Customer>().ReverseMap();
            CreateMap<DomainEntity.CustomerImage, Entity.CustomerImage>().ReverseMap();
            CreateMap<DomainEntity.DeliveryReceiptDetail, Entity.DeliveryReceiptDetailFile>().ReverseMap();
            CreateMap<DomainEntity.DeliveryReceiptMaster, Entity.DeliveryReceiptMasterFile>().ReverseMap();
            CreateMap<DomainEntity.EmployeeApprover, Entity.Approver>().ReverseMap();
            CreateMap<DomainEntity.EmployeeChecker, Entity.Checker>().ReverseMap();
            CreateMap<DomainEntity.EmployeeDelivered, Entity.Deliverer>().ReverseMap();
            CreateMap<DomainEntity.EmployeeImage, Entity.EmployeeImage>().ReverseMap();
            CreateMap<DomainEntity.EmployeeSalesRef, Entity.SalesRef>().ReverseMap();
            CreateMap<DomainEntity.Item, Entity.Item>().ReverseMap();
            CreateMap<DomainEntity.ItemDetail, Entity.ItemDetail>().ReverseMap();
            CreateMap<DomainEntity.ItemImage, Entity.ItemImage>().ReverseMap();
            CreateMap<DomainEntity.ItemSupplier, Entity.ItemSupplier>().ReverseMap();
            CreateMap<DomainEntity.ItemUnitMeasure, Entity.ItemUnitMeasure>().ReverseMap();
            CreateMap<DomainEntity.Location, Entity.Location>().ReverseMap();
            CreateMap<DomainEntity.OfficialReceiptDetail, Entity.OfficialReceiptDetailFile>().ReverseMap();
            CreateMap<DomainEntity.OfficialReceiptMaster, Entity.OfficialReceiptMasterFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseOrderDetail, Entity.PurchaseOrderDetailFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseOrderMaster, Entity.PurchaseOrderMasterFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseRequisitionDetail, Entity.PurchaseRequisitionDetailFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseRequisitionMaster, Entity.PurchaseRequisitionMasterFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseReturnDetail, Entity.PurchaseReturnDetailFile>().ReverseMap();
            CreateMap<DomainEntity.PurchaseReturnMaster, Entity.PurchaseReturnMasterFile>().ReverseMap();
            CreateMap<DomainEntity.ReceivingReportDetail, Entity.ReceivingReportDetailFile>().ReverseMap();
            CreateMap<DomainEntity.ReceivingReportMaster, Entity.ReceivingReportMasterFile>().ReverseMap();
            CreateMap<DomainEntity.SalesInvoiceDetail, Entity.SalesInvoiceDetailFile>().ReverseMap();
            CreateMap<DomainEntity.SalesInvoiceMaster, Entity.SalesInvoiceMasterFile>().ReverseMap();
            CreateMap<DomainEntity.SalesOrderDetail, Entity.SalesOrderDetailFile>().ReverseMap();
            CreateMap<DomainEntity.SalesOrderMaster, Entity.SalesOrderMasterFile>().ReverseMap();
            CreateMap<DomainEntity.SalesReturnDetail, Entity.SalesReturnDetailFile>().ReverseMap();
            CreateMap<DomainEntity.SalesReturnMaster, Entity.SalesReturnMasterFile>().ReverseMap();
            CreateMap<DomainEntity.Supplier, Entity.Supplier>().ReverseMap();
            CreateMap<DomainEntity.SupplierImage, Entity.SupplierImage>().ReverseMap();
            CreateMap<DomainEntity.Warehouse, Entity.Warehouse>().ReverseMap();
        }
    }
}
