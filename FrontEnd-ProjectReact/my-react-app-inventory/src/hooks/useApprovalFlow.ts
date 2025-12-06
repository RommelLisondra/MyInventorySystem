import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchApprovalFlows,
  fetchApprovalFlowById,
  searchApprovalFlows,
  addApprovalFlow,
  updateApprovalFlowById,
  deleteApprovalFlowById,
} from "../features/approvalFlow/approvalFlowThunk";
import type { ApprovalFlow } from "../types/approvalFlow";

interface UseApprovalFlowReturn {
    approvalFlows: ApprovalFlow[];
    selectedApprovalFlow: ApprovalFlow | null;
    loading: boolean;
    error: string | null;
    addNewApprovalFlow: (ApprovalFlow: ApprovalFlow) => void;
    updateApprovalFlow: (ApprovalFlow: ApprovalFlow) => void;
    deleteApprovalFlow: (id: number) => void;
    reloadApprovalFlows: () => void;
    getApprovalFlowById: (id: number) => void;
    searchApprovalFlow: (name: string) => void;
}

export const useApprovalFlow = (): UseApprovalFlowReturn => {
    const dispatch = useDispatch<AppDispatch>();
    const { approvalFlows, selectedApprovalFlow, loading, error } = useSelector(
      (state: RootState) => state.approvalFlow
    );

    // Load on mount
    useEffect(() => {
      dispatch(fetchApprovalFlows());
    }, [dispatch]);

    const addNewApprovalFlow = useCallback(
      (ApprovalFlow: ApprovalFlow) => {
        dispatch(addApprovalFlow(ApprovalFlow));
      },
      [dispatch]
    );

    const updateApprovalFlow = useCallback(
      (ApprovalFlow: ApprovalFlow) => {
        dispatch(updateApprovalFlowById(ApprovalFlow));
      },
      [dispatch]
    );

    const deleteApprovalFlow = useCallback(
      (id: number) => {
        if (confirm("Are you sure you want to delete this ApprovalFlow?")) {
          dispatch(deleteApprovalFlowById(id));
        }
      },
      [dispatch]
    );

    const reloadApprovalFlows = useCallback(() => {
      dispatch(fetchApprovalFlows());
    }, [dispatch]);

    const getApprovalFlowById = useCallback(
      (id: number) => {
        dispatch(fetchApprovalFlowById(id));
      },
      [dispatch]
    );

    const searchApprovalFlow = useCallback((keyword: string) => {
        dispatch(searchApprovalFlows(keyword));
      },
      [dispatch]
    );

    return {
      approvalFlows,
      selectedApprovalFlow,
      loading,
      error,
      addNewApprovalFlow,
      updateApprovalFlow,
      deleteApprovalFlow,
      reloadApprovalFlows,
      getApprovalFlowById,
      searchApprovalFlow,
    };
};
