import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchreceivingReports,
  fetchreceivingReportById,
  searchreceivingReports,
  addreceivingReport,
  updatereceivingReportById,
  deletereceivingReportById,
} from "../features/receivingReport/receivingReportThunk";
import type { ReceivingReportMaster } from "../types/receivingReportMaster";

interface UsereceivingReportReturn {
  receivingReport: ReceivingReportMaster[];
  selectedreceivingReport: ReceivingReportMaster | null;
  loading: boolean;
  error: string | null;
  addNewreceivingReport: (receivingReport: ReceivingReportMaster) => void;
  updatereceivingReport: (receivingReport: ReceivingReportMaster) => void;
  deletereceivingReport: (id: number) => void;
  reloadreceivingReports: () => void;
  getreceivingReportById: (id: number) => void;
  searchreceivingReport: (name: string) => void;
}

export const usereceivingReport = (): UsereceivingReportReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { receivingReport, selectedreceivingReport, loading, error } = useSelector(
    (state: RootState) => state.receivingReport
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchreceivingReports());
  }, [dispatch]);

  const addNewreceivingReport = useCallback(
    (receivingReport: ReceivingReportMaster) => {
      dispatch(addreceivingReport(receivingReport));
    },
    [dispatch]
  );

  const updatereceivingReport = useCallback(
    (receivingReport: ReceivingReportMaster) => {
      dispatch(updatereceivingReportById(receivingReport));
    },
    [dispatch]
  );

  const deletereceivingReport = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this receivingReport?")) {
        dispatch(deletereceivingReportById(id));
      }
    },
    [dispatch]
  );

  const reloadreceivingReports = useCallback(() => {
    dispatch(fetchreceivingReports());
  }, [dispatch]);

  const getreceivingReportById = useCallback(
    (id: number) => {
      dispatch(fetchreceivingReportById(id));
    },
    [dispatch]
  );

  const searchreceivingReport = useCallback(
    (keyword: string) => {
      dispatch(searchreceivingReports(keyword));
    },
    [dispatch]
  );

  return {
    receivingReport,
    selectedreceivingReport,
    loading,
    error,
    addNewreceivingReport,
    updatereceivingReport,
    deletereceivingReport,
    reloadreceivingReports,
    getreceivingReportById,
    searchreceivingReport,
  };
};
