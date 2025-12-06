import type { Item } from "./item";
import type { Branch } from "./branch";

export interface ItemPriceHistory {
  id: number;
  itemId: number;
  price: number;
  effectiveDate: string; // or Date
  remarks?: string;
  createdDateTime: string; // or Date
  branchId: number;

  branch: Branch; // Ensure BranchModel is defined
  item: Item;   
}