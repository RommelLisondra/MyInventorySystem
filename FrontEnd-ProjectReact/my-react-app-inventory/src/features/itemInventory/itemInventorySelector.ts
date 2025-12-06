import type { RootState } from "../../app/store";

export const selectItemInventory = (state: RootState) => state.itemInventory.itemInventory;
export const selectItemInventoryLoading = (state: RootState) => state.itemInventory.loading;
export const selectItemInventoryError = (state: RootState) => state.itemInventory.error;

// Optional: selector para sa isang customer by id
export const selectItemInventoryById = (id: number) => (state: RootState) =>
  state.itemInventory.itemInventory.find((c) => c.id === id);
