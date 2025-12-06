import type { RootState } from "../../app/store";

export const selectWarehouses = (state: RootState) => state.warehouse.warehouses;
export const selectWarehouseLoading = (state: RootState) => state.warehouse.loading;
export const selectWarehouseError = (state: RootState) => state.warehouse.error;

// Optional: selector para sa isang customer by id
export const selectWarehouseById = (id: number) => (state: RootState) =>
  state.warehouse.warehouses.find((c) => c.id === id);
