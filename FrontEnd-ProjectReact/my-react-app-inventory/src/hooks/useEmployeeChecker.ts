import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchEmployeeCheckers,
  fetchEmployeeCheckerById,
  searchEmployeeCheckers,
  addEmployeeChecker,
  updateEmployeeCheckerById,
  deleteEmployeeCheckerById,
} from "../features/employeeChecker/employeeCheckerThunk";
import type { EmployeeChecker } from "../types/employeeChecker";

interface UseEmployeeCheckerReturn {
  employeeCheckers: EmployeeChecker[];
  selectedEmployeeChecker: EmployeeChecker | null;
  loading: boolean;
  error: string | null;
  addNewEmployeeChecker: (EmployeeChecker: EmployeeChecker) => void;
  updateEmployeeChecker: (EmployeeChecker: EmployeeChecker) => void;
  deleteEmployeeChecker: (id: number) => void;
  reloadEmployeeCheckers: () => void;
  getEmployeeCheckerById: (id: number) => void;
  searchEmployeeChecker: (name: string) => void;
}

export const useEmployeeChecker = (): UseEmployeeCheckerReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { employeeCheckers, selectedEmployeeChecker, loading, error } = useSelector(
    (state: RootState) => state.employeeChecker
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchEmployeeCheckers());
  }, [dispatch]);

  const addNewEmployeeChecker = useCallback(
    (employeeChecker: EmployeeChecker) => {
      dispatch(addEmployeeChecker(employeeChecker));
    },
    [dispatch]
  );

  const updateEmployeeChecker = useCallback(
    (employeeChecker: EmployeeChecker) => {
      dispatch(updateEmployeeCheckerById(employeeChecker));
    },
    [dispatch]
  );

  const deleteEmployeeChecker = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this EmployeeChecker?")) {
        dispatch(deleteEmployeeCheckerById(id));
      }
    },
    [dispatch]
  );

  const reloadEmployeeCheckers = useCallback(() => {
    dispatch(fetchEmployeeCheckers());
  }, [dispatch]);

  const getEmployeeCheckerById = useCallback(
    (id: number) => {
      dispatch(fetchEmployeeCheckerById(id));
    },
    [dispatch]
  );

  const searchEmployeeChecker = useCallback((keyword: string) => {
      dispatch(searchEmployeeCheckers(keyword));
    },
    [dispatch]
  );

  return {
    employeeCheckers,
    selectedEmployeeChecker,
    loading,
    error,
    addNewEmployeeChecker,
    updateEmployeeChecker,
    deleteEmployeeChecker,
    reloadEmployeeCheckers,
    getEmployeeCheckerById,
    searchEmployeeChecker,
  };
};
