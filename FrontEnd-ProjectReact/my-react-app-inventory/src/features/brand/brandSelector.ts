import type { RootState } from "../../app/store";

export const selectbrands = (state: RootState) => state.brand.brands;
export const selectbrandLoading = (state: RootState) => state.brand.loading;
export const selectbrandError = (state: RootState) => state.brand.error;

// Optional: selector para sa isang customer by id
export const selectbrandById = (id: number) => (state: RootState) =>
  state.brand.brands.find((c) => c.id === id);
