import { EXPENSE_API } from "../constants/api";
import type { Expense } from "../types/expense";
import type { PagedResponse } from "../types/pagedResponse";
import { authFetch } from "./authService"; // centralized authFetch

// ===== Approval Flow API =====
export const getExpenses = async (): Promise<Expense[]> => {
  return authFetch(EXPENSE_API);
};

export const getExpensesPaged = async (pageNumber: number = 1,pageSize: number = 20): Promise<PagedResponse<Expense[]>> => {
  return authFetch(`${EXPENSE_API}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`);
};

export const getExpenseById = async (id: number): Promise<Expense> => {
  return authFetch(`${EXPENSE_API}/${id}`);
};

export const createExpense = async (Expense: Expense): Promise<Expense> => {
  return authFetch(EXPENSE_API, {
    method: "POST",
    body: JSON.stringify(Expense),
  });
};

export const updateExpense = async (Expense: Expense): Promise<void> => {
  await authFetch(`${EXPENSE_API}/${Expense.id}`, {
    method: "PUT",
    body: JSON.stringify(Expense),
  });
};

export const deleteExpense = async (id: number): Promise<void> => {
  await authFetch(`${EXPENSE_API}/${id}`, { method: "DELETE" });
};

export const searchExpenses = async (keyword: string): Promise<Expense[]> => {
  return authFetch(`${EXPENSE_API}/search?keyword=${encodeURIComponent(keyword)}`);
};
