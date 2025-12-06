import type { RootState } from "../../app/store";

export const selectcategory = (state: RootState) => state.category.category;
export const selectcategoryLoading = (state: RootState) => state.category.loading;
export const selectcategoryError = (state: RootState) => state.category.error;

// Optional: selector para sa isang customer by id
export const selectcategoryById = (id: number) => (state: RootState) =>
  state.category.category.find((c) => c.id === id);
