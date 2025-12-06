export interface InventoryTransactionModel {
  id: number;
  transactionDate: Date;
  companyId: number;
  warehouseId: number;
  itemDetailNo: string;
  refModule?: string | null;
  refNo?: string | null;
  refId?: number | null;
  quantity: number;
  unitCost: number;
  movementType: string;
  remarks?: string | null;
  createdBy: string;
  createdDate: Date;
  recStatus?: string | null;
}