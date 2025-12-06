import type { Item } from "./item";
import type { Branch } from "./branch";
import type { Warehouse } from "./warehouse";


export interface ItemWarehouseMapping {
  id: number;
  itemId: number;
  warehouseId: number;
  branchId?: number;
  isActive: boolean;
  createdDateTime: string; // or Date
  modifiedDateTime?: string; // or Date

  // Navigation properties
  item: Item; // Ensure ItemModel is defined
  warehouse: Warehouse; // Ensure WarehouseModel is defined
  branch?: Branch; // Optional, can be undefined
}