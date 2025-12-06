import type { RootState } from "../../app/store";

export const selectItems = (state: RootState) => state.item.items;
export const selectItemsLoading = (state: RootState) => state.item.loading;
export const selectItemError = (state: RootState) => state.item.error;

// Optional: selector para sa isang customer by id
export const selectItemById = (id: number) => (state: RootState) =>
  state.item.items.find((c) => c.id === id);
