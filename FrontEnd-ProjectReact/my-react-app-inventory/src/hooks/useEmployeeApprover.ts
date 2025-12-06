import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchEmployeeApprover,
  fetchEmployeeApproverById,
  searchEmployeeApprovers,
  addEmployeeApprover,
  updateEmployeeApproverById,
  deleteEmployeeApproverById,
} from "../features/employeeApprover/employeeApproverThunk";
import type { EmployeeApprover } from "../types/employeeApprover";

interface UseEmployeeApproverReturn {
  employeeApprover: EmployeeApprover[];
  selectedEmployeeApprover: EmployeeApprover | null;
  loading: boolean;
  error: string | null;
  addNewEmployeeApprover: (EmployeeApprover: EmployeeApprover) => void;
  updateEmployeeApprover: (EmployeeApprover: EmployeeApprover) => void;
  deleteEmployeeApprover: (id: number) => void;
  reloadEmployeeApprovers: () => void;
  getEmployeeApproverById: (id: number) => void;
  searchEmployeeApprover: (name: string) => void;
}

export const useEmployeeApprover = (): UseEmployeeApproverReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { employeeApprover, selectedEmployeeApprover, loading, error } = useSelector(
    (state: RootState) => state.employeeApprover
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchEmployeeApprover());
  }, [dispatch]);

  const addNewEmployeeApprover = useCallback((employeeApprover: EmployeeApprover) => {
      dispatch(addEmployeeApprover(employeeApprover));
    },
    [dispatch]
  );

  const updateEmployeeApprover = useCallback((employeeApprover: EmployeeApprover) => {
      dispatch(updateEmployeeApproverById(employeeApprover));
    },
    [dispatch]
  );

  const deleteEmployeeApprover = useCallback((id: number) => {
      if (confirm("Are you sure you want to delete this EmployeeApprover?")) {
        dispatch(deleteEmployeeApproverById(id));
      }
    },
    [dispatch]
  );

  const reloadEmployeeApprovers = useCallback(() => {
    dispatch(fetchEmployeeApprover());
  }, [dispatch]);

  const getEmployeeApproverById = useCallback((id: number) => {
      dispatch(fetchEmployeeApproverById(id));
    },
    [dispatch]
  );

  const searchEmployeeApprover = useCallback((keyword: string) => {
      dispatch(searchEmployeeApprovers(keyword));
    },
    [dispatch]
  );

  return {
    employeeApprover,
    selectedEmployeeApprover,
    loading,
    error,
    addNewEmployeeApprover,
    updateEmployeeApprover,
    deleteEmployeeApprover,
    reloadEmployeeApprovers,
    getEmployeeApproverById,
    searchEmployeeApprover,
  };
};
