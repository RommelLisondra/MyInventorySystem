import type { RootState } from "../../app/store";

export const selectPurchaseRequisition = (state: RootState) => state.purchaseRequisition.purchaseRequisition;
export const selectPurchaseRequisitionLoading = (state: RootState) => state.purchaseRequisition.loading;
export const selectPurchaseRequisitionError = (state: RootState) => state.purchaseRequisition.error;

// Optional: selector para sa isang customer by id
export const selectPurchaseRequisitionById = (id: number) => (state: RootState) =>
  state.purchaseRequisition.purchaseRequisition.find((c) => c.id === id);
