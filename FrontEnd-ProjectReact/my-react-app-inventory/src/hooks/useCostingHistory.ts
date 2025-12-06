import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchCostingHistorys,
  fetchCostingHistoryById,
  searchCostingHistory,
  addCostingHistory,
  updateCostingHistoryById,
  deleteCostingHistoryById,
} from "../features/costingHistory/costingHistoryThunk";
import type { CostingHistory } from "../types/costingHistory";

interface UseCostingHistoryReturn {
    costingHistory: CostingHistory[];
    selectedCostingHistory: CostingHistory | null;
    loading: boolean;
    error: string | null;
    addNewCostingHistory: (CostingHistory: CostingHistory) => void;
    updateCostingHistory: (CostingHistory: CostingHistory) => void;
    deleteCostingHistory: (id: number) => void;
    reloadCostingHistorys: () => void;
    getCostingHistoryById: (id: number) => void;
    searchCostingHistorys: (name: string) => void;
}

export const useCostingHistory = (): UseCostingHistoryReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { costingHistory, selectedCostingHistory, loading, error } = useSelector(
      (state: RootState) => state.costingHistory
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchCostingHistorys());
    }, [dispatch]);

    const addNewCostingHistory = useCallback(
      (CostingHistory: CostingHistory) => {
        dispatch(addCostingHistory(CostingHistory));
      },
      [dispatch]
    );

    const updateCostingHistory = useCallback(
      (CostingHistory: CostingHistory) => {
        dispatch(updateCostingHistoryById(CostingHistory));
      },
      [dispatch]
    );

    const deleteCostingHistory = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this CostingHistory?")) {
          dispatch(deleteCostingHistoryById(id));
        }
      },
      [dispatch]
    );

    const reloadCostingHistorys = useCallback(() => {
      dispatch(fetchCostingHistorys());
    }, [dispatch]);

    const getCostingHistoryById = useCallback(
      (id: number) => {
        dispatch(fetchCostingHistoryById(id));
      },
      [dispatch]
    );

    const searchCostingHistorys = useCallback((keyword: string) => {
        dispatch(searchCostingHistory(keyword));
      },
      [dispatch]
    );

    return {
      costingHistory,
      selectedCostingHistory,
      loading,
      error,
      addNewCostingHistory,
      updateCostingHistory,
      deleteCostingHistory,
      reloadCostingHistorys,
      getCostingHistoryById,
      searchCostingHistorys,
    };
};
