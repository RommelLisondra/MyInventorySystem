import type { RootState } from "../../app/store";

export const selectPurchaseOrder = (state: RootState) => state.purchaseOrder.purchaseOrder;
export const selectPurchaseOrderLoading = (state: RootState) => state.purchaseOrder.loading;
export const selectPurchaseOrderError = (state: RootState) => state.purchaseOrder.error;

// Optional: selector para sa isang customer by id
export const selectPurchaseOrderById = (id: number) => (state: RootState) =>
  state.purchaseOrder.purchaseOrder.find((c) => c.id === id);
