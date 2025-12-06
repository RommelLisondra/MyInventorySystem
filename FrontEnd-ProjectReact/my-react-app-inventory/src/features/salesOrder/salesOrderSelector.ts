import type { RootState } from "../../app/store";

export const selectSalesOrder = (state: RootState) => state.salesOrder.salesOrder;
export const selectSalesOrderLoading = (state: RootState) => state.salesOrder.loading;
export const selectSalesOrderError = (state: RootState) => state.salesOrder.error;

// Optional: selector para sa isang customer by id
export const selectSalesOrderById = (id: number) => (state: RootState) =>
  state.salesOrder.salesOrder.find((c) => c.id === id);
