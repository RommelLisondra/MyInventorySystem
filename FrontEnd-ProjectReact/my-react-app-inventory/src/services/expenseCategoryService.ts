import { EXPENSE_CATEGORY_API } from "../constants/api";
import type { ExpenseCategory } from "../types/expenseCategory";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getExpenseCategory = async (): Promise<ExpenseCategory[]> => {
  return authFetch(EXPENSE_CATEGORY_API);
};

export const getExpenseCategoryPaged = async (pageNumber: number = 1,pageSize: number = 20): Promise<PagedResponse<ExpenseCategory[]>> => {
  return authFetch(`${EXPENSE_CATEGORY_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getExpenseCategoryById = async (id: number): Promise<ExpenseCategory> => {
  return authFetch(`${EXPENSE_CATEGORY_API}/${id}`);
};

export const createExpenseCategory = async (ExpenseCategory: ExpenseCategory): Promise<ExpenseCategory> => {
  return authFetch(EXPENSE_CATEGORY_API, {
    method: "POST",
    body: JSON.stringify(ExpenseCategory),
  });
};

export const updateExpenseCategory = async (ExpenseCategory: ExpenseCategory): Promise<void> => {
  await authFetch(`${EXPENSE_CATEGORY_API}/${ExpenseCategory.id}`, {
    method: "PUT",
    body: JSON.stringify(ExpenseCategory),
  });
};

export const deleteExpenseCategory = async (id: number): Promise<void> => {
  await authFetch(`${EXPENSE_CATEGORY_API}/${id}`, { method: "DELETE" });
};

export const searchExpenseCategory = async (keyword: string): Promise<ExpenseCategory[]> => {
  return authFetch(`${EXPENSE_CATEGORY_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
