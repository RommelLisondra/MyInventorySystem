import type { Employee } from "./employee";
import type { Warehouse } from "./warehouse";
import type { StockCountDetail } from "./stockCountDetail";


export interface StockCountMaster {
  id: number;
  scmno: string;
  date?: Date | null;
  warehouseCode: string;
  preparedBy?: string | null;
  recStatus?: string | null;
  preparedByNavigation?: Employee | null;
  warehouseCodeNavigation: Warehouse;
  stockCountDetail?: StockCountDetail[] | null;
}