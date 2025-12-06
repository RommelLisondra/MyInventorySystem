import type { RootState } from "../../app/store";

export const selectexpenses = (state: RootState) => state.expense.expenses;
export const selectexpenseLoading = (state: RootState) => state.expense.loading;
export const selectexpenseError = (state: RootState) => state.expense.error;

// Optional: selector para sa isang customer by id
export const selectexpenseById = (id: number) => (state: RootState) =>
  state.expense.expenses.find((c) => c.id === id);
