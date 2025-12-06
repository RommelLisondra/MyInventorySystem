import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchApprovalHistorys,
  fetchApprovalHistoryById,
  searchApprovalHistorys,
  addApprovalHistory,
  updateApprovalHistoryById,
  deleteApprovalHistoryById,
} from "../features/approvalHistory/approvalHistoryThunk";
import type { ApprovalHistory } from "../types/approvalHistory";

interface UseApprovalHistoryReturn {
  approvalHistory: ApprovalHistory[];
  selectedApprovalHistory: ApprovalHistory | null;
  loading: boolean;
  error: string | null;
  addNewApprovalHistory: (approvalHistory: ApprovalHistory) => void;
  updateApprovalHistory: (approvalHistory: ApprovalHistory) => void;
  deleteApprovalHistory: (id: number) => void;
  reloadApprovalHistorys: () => void;
  getApprovalHistoryById: (id: number) => void;
  searchApprovalHistory: (name: string) => void;
}

export const useApprovalHistory = (): UseApprovalHistoryReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { approvalHistory, selectedApprovalHistory, loading, error } = useSelector(
      (state: RootState) => state.approvalHistory
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchApprovalHistorys());
    }, [dispatch]);

    const addNewApprovalHistory = useCallback(
      (ApprovalHistory: ApprovalHistory) => {
        dispatch(addApprovalHistory(ApprovalHistory));
      },
      [dispatch]
    );

    const updateApprovalHistory = useCallback(
      (ApprovalHistory: ApprovalHistory) => {
        dispatch(updateApprovalHistoryById(ApprovalHistory));
      },
      [dispatch]
    );

    const deleteApprovalHistory = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this ApprovalHistory?")) {
          dispatch(deleteApprovalHistoryById(id));
        }
      },
      [dispatch]
    );

    const reloadApprovalHistorys = useCallback(() => {
      dispatch(fetchApprovalHistorys());
    }, [dispatch]);

    const getApprovalHistoryById = useCallback(
      (id: number) => {
        dispatch(fetchApprovalHistoryById(id));
      },
      [dispatch]
    );

    const searchApprovalHistory = useCallback((keyword: string) => {
        dispatch(searchApprovalHistorys(keyword));
      },
      [dispatch]
    );

    return {
      approvalHistory,
      selectedApprovalHistory,
      loading,
      error,
      addNewApprovalHistory,
      updateApprovalHistory,
      deleteApprovalHistory,
      reloadApprovalHistorys,
      getApprovalHistoryById,
      searchApprovalHistory,
    };
};
