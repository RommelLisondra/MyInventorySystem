import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchBranchs,
  fetchBranchById,
  searchBranch,
  addBranch,
  updateBranchById,
  deleteBranchById,
} from "../features/branch/branchThunk";
import type { Branch } from "../types/branch";

interface UseBranchReturn {
    branch: Branch[];
    selectedBranch: Branch | null;
    loading: boolean;
    error: string | null;
    addNewBranch: (Branch: Branch) => void;
    updateBranch: (Branch: Branch) => void;
    deleteBranch: (id: number) => void;
    reloadBranchs: () => void;
    getBranchById: (id: number) => void;
    searchBranches: (name: string) => void;
}

export const useBranch = (): UseBranchReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { branch, selectedBranch, loading, error } = useSelector(
      (state: RootState) => state.branch
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchBranchs());
    }, [dispatch]);

    const addNewBranch = useCallback(
      (Branch: Branch) => {
        dispatch(addBranch(Branch));
      },
      [dispatch]
    );

    const updateBranch = useCallback(
      (Branch: Branch) => {
        dispatch(updateBranchById(Branch));
      },
      [dispatch]
    );

    const deleteBranch = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this Branch?")) {
          dispatch(deleteBranchById(id));
        }
      },
      [dispatch]
    );

    const reloadBranchs = useCallback(() => {
      dispatch(fetchBranchs());
    }, [dispatch]);

    const getBranchById = useCallback(
      (id: number) => {
        dispatch(fetchBranchById(id));
      },
      [dispatch]
    );

    const searchBranches = useCallback((keyword: string) => {
        dispatch(searchBranch(keyword));
      },
      [dispatch]
    );

    return {
      branch,
      selectedBranch,
      loading,
      error,
      addNewBranch,
      updateBranch,
      deleteBranch,
      reloadBranchs,
      getBranchById,
      searchBranches,
    };
};
