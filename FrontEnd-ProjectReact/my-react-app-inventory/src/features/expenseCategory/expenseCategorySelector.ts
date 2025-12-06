import type { RootState } from "../../app/store";

export const selectexpenseCategory = (state: RootState) => state.expenseCategory.expenseCategory;
export const selectexpenseCategoryLoading = (state: RootState) => state.expenseCategory.loading;
export const selectexpenseCategoryError = (state: RootState) => state.expenseCategory.error;

// Optional: selector para sa isang customer by id
export const selectexpenseCategoryById = (id: number) => (state: RootState) =>
  state.expenseCategory.expenseCategory.find((c) => c.id === id);
