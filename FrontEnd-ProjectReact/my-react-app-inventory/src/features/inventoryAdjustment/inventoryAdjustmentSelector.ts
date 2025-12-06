import type { RootState } from "../../app/store";

export const selectInventoryAdjustments = (state: RootState) => state.inventoryAdjustment.inventoryAdjustments;
export const selectInventoryAdjustmentLoading = (state: RootState) => state.inventoryAdjustment.loading;
export const selectInventoryAdjustmentError = (state: RootState) => state.inventoryAdjustment.error;

// Optional: selector para sa isang InventoryAdjustment by id
export const selectInventoryAdjustmentById = (id: number) => (state: RootState) =>
  state.inventoryAdjustment.inventoryAdjustments.find((c) => c.id === id);
