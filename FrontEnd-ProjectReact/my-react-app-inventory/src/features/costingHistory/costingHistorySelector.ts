import type { RootState } from "../../app/store";

export const selectCostingHistory = (state: RootState) => state.costingHistory.costingHistory;
export const selectCostingHistoryLoading = (state: RootState) => state.costingHistory.loading;
export const selectCostingHistoryError = (state: RootState) => state.costingHistory.error;

// Optional: selector para sa isang customer by id
export const selectCostingHistoryById = (id: number) => (state: RootState) =>
  state.costingHistory.costingHistory.find((c) => c.id === id);
