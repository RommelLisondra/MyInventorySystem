import type { RootState } from "../../app/store";

export const selectItemSuppliers = (state: RootState) => state.itemSupplier.itemSuppliers;
export const selectItemSuppliersLoading = (state: RootState) => state.itemSupplier.loading;
export const selectItemSupplierError = (state: RootState) => state.itemSupplier.error;

// Optional: selector para sa isang customer by id
export const selectItemSupplierById = (id: number) => (state: RootState) =>
  state.itemSupplier.itemSuppliers.find((c) => c.id === id);
