import type { Expense } from "./expense";

export interface ExpenseCategory {
  id: number;
  code: string;
  name: string;
  description?: string;
  isActive: boolean;
  createdDate: Date;
  modifiedDate?: Date;

  // Collection of Expenses
  expenses: Expense[];
}