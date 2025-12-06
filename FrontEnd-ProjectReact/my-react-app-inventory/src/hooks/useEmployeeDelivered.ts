import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchEmployeeDelivereds,
  fetchEmployeeDeliveredById,
  searchEmployeeDelivereds,
  addEmployeeDelivered,
  updateEmployeeDeliveredById,
  deleteEmployeeDeliveredById,
} from "../features/employeeDeliverer/employeeDelivererThunk";
import type { EmployeeDelivered } from "../types/employeeDelivered";

interface UseEmployeeDeliveredReturn {
  employeeDelivered: EmployeeDelivered[];
  selectedEmployeeDelivered: EmployeeDelivered | null;
  loading: boolean;
  error: string | null;
  addNewEmployeeDelivered: (EmployeeDelivered: EmployeeDelivered) => void;
  updateEmployeeDelivered: (EmployeeDelivered: EmployeeDelivered) => void;
  deleteEmployeeDelivered: (id: number) => void;
  reloadEmployeeDelivereds: () => void;
  getEmployeeDeliveredById: (id: number) => void;
  searchEmployeeDelivered: (name: string) => void;
}

export const useEmployeeDelivered = (): UseEmployeeDeliveredReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { employeeDelivered, selectedEmployeeDelivered, loading, error } = useSelector(
    (state: RootState) => state.employeeDeliverer
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchEmployeeDelivereds());
  }, [dispatch]);

  const addNewEmployeeDelivered = useCallback(
    (employeeDelivered: EmployeeDelivered) => {
      dispatch(addEmployeeDelivered(employeeDelivered));
    },
    [dispatch]
  );

  const updateEmployeeDelivered = useCallback(
    (employeeDelivered: EmployeeDelivered) => {
      dispatch(updateEmployeeDeliveredById(employeeDelivered));
    },
    [dispatch]
  );

  const deleteEmployeeDelivered = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this EmployeeDelivered?")) {
        dispatch(deleteEmployeeDeliveredById(id));
      }
    },
    [dispatch]
  );

  const reloadEmployeeDelivereds = useCallback(() => {
    dispatch(fetchEmployeeDelivereds());
  }, [dispatch]);

  const getEmployeeDeliveredById = useCallback(
    (id: number) => {
      dispatch(fetchEmployeeDeliveredById(id));
    },
    [dispatch]
  );

  const searchEmployeeDelivered = useCallback((keyword: string) => {
      dispatch(searchEmployeeDelivereds(keyword));
    },
    [dispatch]
  );

  return {
    employeeDelivered,
    selectedEmployeeDelivered,
    loading,
    error,
    addNewEmployeeDelivered,
    updateEmployeeDelivered,
    deleteEmployeeDelivered,
    reloadEmployeeDelivereds,
    getEmployeeDeliveredById,
    searchEmployeeDelivered,
  };
};
