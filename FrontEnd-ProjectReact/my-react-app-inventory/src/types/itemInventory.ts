import type { ItemDetail } from "./itemDetail";
import type { Warehouse } from "./warehouse";
import type { Location } from "./location";

export interface ItemInventory {
  id: number;
  itemDetailCode: string;
  warehouseCode: string;
  LocationCode: string;
  QtyOnHand?: number | null;
  QtyOnOrder?: number | null;
  QtyReserved?: number | null;
  ExtendedQtyOnHand?: number | null;
  SalesReturnItemQty?: number | null;
  PurchaseReturnItemQty?: number | null;
  LastStockCount?: Date;
  recStatus?: string | null;
  
  itemDetailNavigation: ItemDetail;
  warehouseNavigation: Warehouse;
  LocationCodeNavigation: Location
}