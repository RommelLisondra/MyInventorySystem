import type { RootState } from "../../app/store";

export const selectItemPriceHistory = (state: RootState) => state.itemPriceHistory.itemPriceHistory;
export const selectItemPriceHistoryLoading = (state: RootState) => state.itemPriceHistory.loading;
export const selectItemPriceHistoryError = (state: RootState) => state.itemPriceHistory.error;

// Optional: selector para sa isang customer by id
export const selectItemPriceHistoryById = (id: number) => (state: RootState) =>
  state.itemPriceHistory.itemPriceHistory.find((c) => c.id === id);
