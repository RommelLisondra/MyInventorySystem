import type { ItemDetail } from "./itemDetail";
import type { StockCountMaster } from "./stockCountMaster";

export interface StockCountDetail {
  id: number;
  scmno: string;
  itemDetailCode: string;
  countedQty?: number | null;
  recStatus?: string | null;
  itemDetailCodeNavigation: ItemDetail;
  scmnoNavigation: StockCountMaster;
}