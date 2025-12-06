import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchEmployees,
  fetchEmployeeById,
  searchEmployees,
  addEmployee,
  updateEmployeeById,
  deleteEmployeeById,
} from "../features/employee/employeeThunk";
import type { Employee } from "../types/employee";

interface UseEmployeeReturn {
  employees: Employee[];
  selectedEmployee: Employee | null;
  loading: boolean;
  error: string | null;
  addNewEmployee: (employee: Employee) => void;
  updateEmployee: (employee: Employee) => void;
  deleteEmployee: (id: number) => void;
  reloadEmployees: () => void;
  getEmployeeById: (id: number) => void;
  searchEmployee: (name: string) => void;
}

export const useEmployee = (): UseEmployeeReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { employees,selectedEmployee, loading, error } = useSelector(
    (state: RootState) => state.employee
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchEmployees());
  }, [dispatch]);

  const addNewEmployee = useCallback(
    (employee: Employee) => {
      dispatch(addEmployee(employee));
    },
    [dispatch]
  );

  const updateEmployee = useCallback(
    (employee: Employee) => {
      dispatch(updateEmployeeById(employee));
    },
    [dispatch]
  );

  const deleteEmployee = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Employee?")) {
        dispatch(deleteEmployeeById(id));
      }
    },
    [dispatch]
  );

  const reloadEmployees = useCallback(() => {
    dispatch(fetchEmployees());
  }, [dispatch]);

  const getEmployeeById = useCallback(
    (id: number) => {
      dispatch(fetchEmployeeById(id));
    },
    [dispatch]
  );

  const searchEmployee = useCallback((keyword: string) => {
      dispatch(searchEmployees(keyword));
    },
    [dispatch]
  );

  return {
    employees,
    selectedEmployee,
    loading,
    error,
    addNewEmployee,
    updateEmployee,
    deleteEmployee,
    reloadEmployees,
    getEmployeeById,
    searchEmployee,
  };
};
