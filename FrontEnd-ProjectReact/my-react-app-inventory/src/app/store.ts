import { configureStore } from "@reduxjs/toolkit";
import customerReducer from "../features/customers/customerSlice";
import employeeReducer from "../features/employee/employeeSlice";
import employeeApproverReducer from "../features/employeeApprover/employeeApproverSlice";
import employeeCheckerReducer from "../features/employeeChecker/employeeCheckerSlice";
import employeeDelivererReducer from "../features/employeeDeliverer/employeeDelivererSlice";
import employeeSalesRefReducer from "../features/employeeSalesRef/employeeSalesRefSlice";
import supplierReducer from "../features/supplier/supplierSlice";
import itemReducer from "../features/item/itemSlice";
import itemDetailReducer from "../features/itemDetail/itemDetailSlice";
import itemImageReducer from "../features/itemImage/itemImageSlice";
import itemSupplierReducer from "../features/itemSupplier/itemSupplierSlice";
import itemUnitMeasureReducer from "../features/itemUnitMeasure/itemUnitMeasureSlice";
import inventoryAdjustmentReducer from "../features/inventoryAdjustment/inventoryAdjustmentSlice";
import warehouseReducer from "../features/warehouse/wareHouseSlice";
import approvalFlowReducer from "../features/approvalFlow/approvalFlowSlice";
import approvalHistoryReducer from "../features/approvalHistory/approvalHistorySlice";
import auditTrailReducer from "../features/auditTrail/auditTrailSlice";
import deliveryReceiptReducer from "../features/deliveryReceipt/deliveryReceiptSlice";
import documentReferenceReducer from "../features/documentReference/documentReferenceSlice";
import documentSeriesReducer from "../features/documentSeries/documentSeriesSlice";
import locationsReducer from "../features/locations/locationsSlice";
import officialReceiptReducer from "../features/officialReceipt/officialReceiptSlice";
import purchaseOrderReducer from "../features/purchaseOrder/purchaseOrderSlice";
import purchaseRequisitionReducer from "../features/purchaseRequisitions/purchaseRequisitionsSlice";
import purchaseRetrunReducer from "../features/purchaseRetrun/purchaseRetrunSlice";
import receivingReportReducer from "../features/receivingReport/receivingReportSlice";
import roleReducer from "../features/roles/rolesSlice";
import rolePermissionReportReducer from "../features/rolesPermission/rolesPermissionSlice";
import salesInvoiceReducer from "../features/salesInvoice/salesInvoiceSlice";
import salesOrderReducer from "../features/salesOrder/salesOrderSlice";
import salesReturnReducer from "../features/salesReturn/salesReturnSlice";
import stockTransferReducer from "../features/stockTransfer/stockTransferSlice";
import systemLogsReducer from "../features/systemLogs/systemLogsSlice";
import systemSettingsReducer from "../features/systemSettings/systemSettingsSlice";
import userAccountsReducer from "../features/userAccounts/userAccountsSlice";

import brandReducer from "../features/brand/brandSlice";
import categoryReducer from "../features/category/categorySlice";
import classificationReducer from "../features/classification/classificationSlice";
import expenseReducer from "../features/expense/expenseSlice";
import expenseCategoryReducer from "../features/expenseCategory/expenseCategorySlice";
import subCategoryReducer from "../features/subCategory/subCategorySlice";
import itemInventoryReducer from "../features/itemInventory/itemInventorySlice";

import accountReducer from "../features/account/accountSlice";
import branchReducer from "../features/branch/branchSlice";
import companyReducer from "../features/company/companySlice";
import costingHistoryReducer from "../features/costingHistory/costingHistorySlice";
import holidayReducer from "../features/holiday/holidaySlice";
import itemBarcodeReducer from "../features/itemBarcode/itemBarcodeSlice";
import itemPriceHistoryReducer from "../features/itemPriceHistory/itemPriceHistorySlice";
import itemWarehouseMappingReducer from "../features/itemWarehouseMapping/itemWarehouseMappingSlice";

export const store = configureStore({
    reducer: {
        customer: customerReducer, 
        employee: employeeReducer, 
        employeeApprover: employeeApproverReducer, 
        employeeChecker: employeeCheckerReducer,
        employeeDeliverer: employeeDelivererReducer,
        employeeSalesRef: employeeSalesRefReducer,
        supplier: supplierReducer, 
        item: itemReducer, 
        itemDetail: itemDetailReducer, 
        itemImage: itemImageReducer, 
        itemSupplier: itemSupplierReducer, 
        itemUnitMeasure: itemUnitMeasureReducer, 
        inventoryAdjustment: inventoryAdjustmentReducer,
        warehouse: warehouseReducer, 
        approvalFlow: approvalFlowReducer, 
        approvalHistory: approvalHistoryReducer, 
        auditTrail: auditTrailReducer, 
        deliveryReceipt: deliveryReceiptReducer, 
        documentReference: documentReferenceReducer, 
        documentSeries: documentSeriesReducer, 
        locations: locationsReducer, 
        officialReceipt: officialReceiptReducer, 
        purchaseOrder: purchaseOrderReducer, 
        purchaseRequisition: purchaseRequisitionReducer, 
        purchaseRetrun: purchaseRetrunReducer, 
        receivingReport: receivingReportReducer, 
        role: roleReducer, 
        rolePermission: rolePermissionReportReducer, 
        salesInvoice: salesInvoiceReducer, 
        salesOrder: salesOrderReducer, 
        salesReturn: salesReturnReducer, 
        stockTransfer: stockTransferReducer, 
        systemLogs: systemLogsReducer, 
        systemSettings: systemSettingsReducer, 
        userAccounts: userAccountsReducer, 
        brand: brandReducer,
        category: categoryReducer,
        classification: classificationReducer,
        expense: expenseReducer,
        expenseCategory: expenseCategoryReducer,
        subCategory: subCategoryReducer,
        itemInventory: itemInventoryReducer,
        account: accountReducer,
        branch: branchReducer,
        company: companyReducer,
        costingHistory: costingHistoryReducer,
        holiday: holidayReducer,
        itemBarcode: itemBarcodeReducer,
        itemPriceHistory: itemPriceHistoryReducer,
        itemWarehouseMapping: itemWarehouseMappingReducer,
    },
    middleware: (getDefaultMiddleware) => getDefaultMiddleware(),
});

// Types for TypeScript
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;