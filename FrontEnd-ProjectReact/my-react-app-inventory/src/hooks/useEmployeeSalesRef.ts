import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchEmployeeSalesRefs,
  fetchEmployeeSalesRefById,
  searchEmployeeSalesRefs,
  addEmployeeSalesRef,
  updateEmployeeSalesRefById,
  deleteEmployeeSalesRefById,
} from "../features/employeeSalesRef/employeeSalesRefThunk";
import type { EmployeeSalesRef } from "../types/employeeSalesRef";

interface UseEmployeeSalesRefReturn {
  employeeSalesRef: EmployeeSalesRef[];
  selectedEmployeeSalesRef: EmployeeSalesRef | null;
  loading: boolean;
  error: string | null;
  addNewEmployeeSalesRef: (EmployeeSalesRef: EmployeeSalesRef) => void;
  updateEmployeeSalesRef: (EmployeeSalesRef: EmployeeSalesRef) => void;
  deleteEmployeeSalesRef: (id: number) => void;
  reloadEmployeeSalesRefs: () => void;
  getEmployeeSalesRefById: (id: number) => void;
  searchEmployeeSalesRef: (name: string) => void;
}

export const useEmployeeSalesRef = (): UseEmployeeSalesRefReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { employeeSalesRef, selectedEmployeeSalesRef, loading, error } = useSelector(
    (state: RootState) => state.employeeSalesRef
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchEmployeeSalesRefs());
  }, [dispatch]);

  const addNewEmployeeSalesRef = useCallback(
    (employeeSalesRef: EmployeeSalesRef) => {
      dispatch(addEmployeeSalesRef(employeeSalesRef));
    },
    [dispatch]
  );

  const updateEmployeeSalesRef = useCallback(
    (employeeSalesRef: EmployeeSalesRef) => {
      dispatch(updateEmployeeSalesRefById(employeeSalesRef));
    },
    [dispatch]
  );

  const deleteEmployeeSalesRef = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this EmployeeSalesRef?")) {
        dispatch(deleteEmployeeSalesRefById(id));
      }
    },
    [dispatch]
  );

  const reloadEmployeeSalesRefs = useCallback(() => {
    dispatch(fetchEmployeeSalesRefs());
  }, [dispatch]);

  const getEmployeeSalesRefById = useCallback(
    (id: number) => {
      dispatch(fetchEmployeeSalesRefById(id));
    },
    [dispatch]
  );

  const searchEmployeeSalesRef = useCallback((keyword: string) => {
      dispatch(searchEmployeeSalesRefs(keyword));
    },
    [dispatch]
  );

  return {
    employeeSalesRef,
    selectedEmployeeSalesRef,
    loading,
    error,
    addNewEmployeeSalesRef,
    updateEmployeeSalesRef,
    deleteEmployeeSalesRef,
    reloadEmployeeSalesRefs,
    getEmployeeSalesRefById,
    searchEmployeeSalesRef,
  };
};
