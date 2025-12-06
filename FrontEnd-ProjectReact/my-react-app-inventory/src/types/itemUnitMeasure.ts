import type { ItemDetail } from "./itemDetail";

export interface ItemUnitMeasure {
  id: number;
  itemDetailCode: string;
  unitCode: string;
  conversionRate?: number;
  recStatus?: string;

  itemDetailCodeNavigation: ItemDetail;
}