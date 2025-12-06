import type { RootState } from "../../app/store";

export const selectSuppliers = (state: RootState) => state.supplier.suppliers;
export const selectSupplierLoading = (state: RootState) => state.supplier.loading;
export const selectSupplierError = (state: RootState) => state.supplier.error;

// Optional: selector para sa isang customer by id
export const selectSupplierById = (id: number) => (state: RootState) =>
  state.supplier.suppliers.find((c) => c.id === id);
