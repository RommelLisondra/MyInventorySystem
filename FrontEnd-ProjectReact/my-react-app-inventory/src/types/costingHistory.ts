import type { Branch } from "./branch";
import type { Item } from "./item";

export interface CostingHistory {
  id: number;
  itemId: number;
  cost: number;
  effectiveDate: string; // or Date
  remarks?: string;
  createdDateTime: string; // or Date
  branchId: number;

  branch: Branch; // you should have this interface defined
  item: Item; // define this interface as well
}