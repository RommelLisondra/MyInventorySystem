import type { ExpenseCategory } from "./expenseCategory";

export interface Expense {
  id: number;
  expenseNo: string;
  expenseDate: Date;
  expenseCategoryId: number;
  amount: number;
  vendor?: string;
  referenceNo?: string;
  notes?: string;
  attachmentPath?: string;
  createdDate: Date;
  isDeleted: boolean;

  // Navigation Property
  expenseCategory: ExpenseCategory;
}