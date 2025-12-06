import type { RootState } from "../../app/store";

export const selectSalesReturn = (state: RootState) => state.salesReturn.salesReturn;
export const selectSalesReturnLoading = (state: RootState) => state.salesReturn.loading;
export const selectSalesReturnError = (state: RootState) => state.salesReturn.error;

// Optional: selector para sa isang customer by id
export const selectSalesReturnById = (id: number) => (state: RootState) =>
  state.salesReturn.salesReturn.find((c) => c.id === id);
