import type { RootState } from "../../app/store";

export const selectitemWarehouseMapping = (state: RootState) => state.itemWarehouseMapping.itemWarehouseMapping;
export const selectitemWarehouseMappingLoading = (state: RootState) => state.itemWarehouseMapping.loading;
export const selectitemWarehouseMappingError = (state: RootState) => state.itemWarehouseMapping.error;

// Optional: selector para sa isang customer by id
export const selectitemWarehouseMappingById = (id: number) => (state: RootState) =>
  state.itemWarehouseMapping.itemWarehouseMapping.find((c) => c.id === id);
