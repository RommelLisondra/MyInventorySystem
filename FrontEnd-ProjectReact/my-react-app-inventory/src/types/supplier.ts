//import type { purchaseOrderMaster } from "./purchaseOrderMaster";
import type { ItemDetail } from "./itemDetail";
import type { ItemSupplier } from "./itemSupplier";
import type { PurchaseOrderMaster } from "./purchaseOrderMaster";
import type { PurchaseReturnMaster } from "./purchaseReturnMaster";
import type { ReceivingReportMaster } from "./receivingReportMaster";
import type { SupplierImage } from "./supplierImage";

export interface Supplier {
  id: number;
  guid: string;
  supplierNo: string;
  name: string;
  address?: string;
  address1?: string;
  address2?: string;
  city?: string;
  state?: string;
  country?: string;
  emailAddress?: string;
  faxNo?: string;
  mobileNo?: string;
  postalCode?: string;
  notes?: string;
  contactNo?: string;
  contactPerson?: string;
  recStatus?: string;
  lastPono?: string;
  
  itemDetails?: ItemDetail[];
  itemSuppliers?: ItemSupplier[];
  purchaseOrderMasterFiles?: PurchaseOrderMaster[];
  purchaseReturnMasterFiles?: PurchaseReturnMaster[];
  receivingReportMasterFiles?: ReceivingReportMaster[];
  supplerImages?: SupplierImage[];
}