import type { RootState } from "../../app/store";

export const selectStockTransfer = (state: RootState) => state.stockTransfer.stockTransfer;
export const selectStockTransferLoading = (state: RootState) => state.stockTransfer.loading;
export const selectStockTransferError = (state: RootState) => state.stockTransfer.error;

// Optional: selector para sa isang customer by id
export const selectStockTransferById = (id: number) => (state: RootState) =>
  state.stockTransfer.stockTransfer.find((c) => c.id === id);
