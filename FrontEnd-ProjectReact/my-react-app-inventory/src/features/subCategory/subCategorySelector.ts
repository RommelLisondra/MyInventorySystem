import type { RootState } from "../../app/store";

export const selectsubCategory = (state: RootState) => state.subCategory.subCategory;
export const selectsubCategoryLoading = (state: RootState) => state.subCategory.loading;
export const selectsubCategoryError = (state: RootState) => state.subCategory.error;

// Optional: selector para sa isang customer by id
export const selectsubCategoryById = (id: number) => (state: RootState) =>
  state.subCategory.subCategory.find((c) => c.id === id);
