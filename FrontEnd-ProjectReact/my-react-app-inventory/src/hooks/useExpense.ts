import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchExpenses,
  fetchExpenseById,
  searchExpenses,
  addExpense,
  updateExpenseById,
  deleteExpenseById,
} from "../features/expense/expenseThunk";
import type { Expense } from "../types/expense";

interface UseExpenseReturn {
  expenses: Expense[];
  selectedExpense: Expense | null;
  loading: boolean;
  error: string | null;
  addNewExpense: (expense: Expense) => void;
  updateExpense: (expense: Expense) => void;
  deleteExpense: (id: number) => void;
  reloadExpenses: () => void;
  getExpenseById: (id: number) => void;
  searchExpense: (name: string) => void;
}

export const useExpense = (): UseExpenseReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { expenses, selectedExpense, loading, error } = useSelector(
    (state: RootState) => state.expense
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchExpenses());
  }, [dispatch]);

  const addNewExpense = useCallback(
    (expense: Expense) => {
      dispatch(addExpense(expense));
    },
    [dispatch]
  );

  const updateExpense = useCallback(
    (expense: Expense) => {
      dispatch(updateExpenseById(expense));
    },
    [dispatch]
  );

  const deleteExpense = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Expense?")) {
        dispatch(deleteExpenseById(id));
      }
    },
    [dispatch]
  );

  const reloadExpenses = useCallback(() => {
    dispatch(fetchExpenses());
  }, [dispatch]);

  const getExpenseById = useCallback(
    (id: number) => {
      dispatch(fetchExpenseById(id));
    },
    [dispatch]
  );

  const searchExpense = useCallback((keyword: string) => {
      dispatch(searchExpenses(keyword));
    },
    [dispatch]
  );

  return {
    expenses,
    selectedExpense,
    loading,
    error,
    addNewExpense,
    updateExpense,
    deleteExpense,
    reloadExpenses,
    getExpenseById,
    searchExpense,
  };
};
