import type { Item } from "./item";

export interface ItemBarcode {
  id: number;
  itemId: number;
  barcode: string;
  description?: string;
  isActive: boolean;
  createdDateTime: string; // or Date

  // Navigation property
  item: Item; // Ensure ItemModel is defined elsewhere
}