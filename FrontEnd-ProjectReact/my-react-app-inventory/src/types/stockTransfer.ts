import type { StockTransferDetail } from "./stockTransferDetail";


export interface StockTransfer {
  id: number;
  transferNo: string;
  transferDate: Date;
  companyId: number;
  fromWarehouseId: number;
  toWarehouseId: number;
  remarks?: string | null;
  createdBy: string;
  createdDate: Date;
  recStatus?: string | null;
  stockTransferDetail?: StockTransferDetail[] | null;
}