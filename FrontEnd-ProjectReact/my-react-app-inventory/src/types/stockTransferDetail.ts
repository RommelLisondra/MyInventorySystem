import type { StockTransfer } from "./stockTransfer";

export interface StockTransferDetail {
  id: number;
  transferId: number;
  itemDetailNo: string;
  quantity: number;
  recStatus?: string | null;
  transfer: StockTransfer;
}