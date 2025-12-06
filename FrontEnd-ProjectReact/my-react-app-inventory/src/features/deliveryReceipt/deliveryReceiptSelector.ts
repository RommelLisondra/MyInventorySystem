import type { RootState } from "../../app/store";

export const selectDeliveryReceipts = (state: RootState) => state.deliveryReceipt.deliveryReceipt;
export const selectDeliveryReceiptLoading = (state: RootState) => state.deliveryReceipt.loading;
export const selectDeliveryReceiptError = (state: RootState) => state.deliveryReceipt.error;

// Optional: selector para sa isang DeliveryReceipt by id
export const selectDeliveryReceiptById = (id: number) => (state: RootState) =>
  state.deliveryReceipt.deliveryReceipt.find((c) => c.id === id);
