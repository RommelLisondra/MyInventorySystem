import type { InventoryTransactionModel } from "./inventoryTransaction";

export interface Account {
  id: number;
  accountCode: string;
  accountName: number;
  description: number;
  isActive: boolean;
  createdDateTime: Date;
  modifiedDateTime: Date;

  inventoryTransactions?: InventoryTransactionModel | null;
}