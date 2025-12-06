import type { ItemDetail } from "./itemDetail";
import type { Supplier } from "./supplier";

export interface ItemSupplier {
  id: number;
  itemDetailCode: string;
  supplierNo: string;
  unitPrice?: number;
  leadTime?: string;
  terms?: string;
  modifiedDateTime: Date;
  createdDateTime: Date;
  recStatus?: string;

  supplier: Supplier;
  itemDetail: ItemDetail
}