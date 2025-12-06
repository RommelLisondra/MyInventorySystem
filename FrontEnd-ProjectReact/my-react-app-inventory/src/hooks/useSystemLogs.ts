import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchSystemLogs,
  fetchSystemLogById,
  searchSystemLogs,
  addSystemLog,
  updateSystemLogById,
  deleteSystemLogById,
} from "../features/systemLogs/systemLogsThunk";
import type { SystemLog } from "../types/systemLog";

interface UseSystemLogReturn {
  systemLogs: SystemLog[];
  selectedSystemLog: SystemLog | null;
  loading: boolean;
  error: string | null;
  addNewSystemLog: (systemLog: SystemLog) => void;
  updateSystemLog: (systemLog: SystemLog) => void;
  deleteSystemLog: (id: number) => void;
  reloadSystemLogs: () => void;
  getSystemLogById: (id: number) => void;
  searchSystemLog: (name: string) => void;
}

export const useSystemLog = (): UseSystemLogReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { systemLogs, selectedSystemLog, loading, error } = useSelector(
    (state: RootState) => state.systemLogs
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchSystemLogs());
  }, [dispatch]);

  const addNewSystemLog = useCallback(
    (systemLog: SystemLog) => {
      dispatch(addSystemLog(systemLog));
    },
    [dispatch]
  );

  const updateSystemLog = useCallback(
    (systemLog: SystemLog) => {
      dispatch(updateSystemLogById(systemLog));
    },
    [dispatch]
  );

  const deleteSystemLog = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this SystemLog?")) {
        dispatch(deleteSystemLogById(id));
      }
    },
    [dispatch]
  );

  const reloadSystemLogs = useCallback(() => {
    dispatch(fetchSystemLogs());
  }, [dispatch]);

  const getSystemLogById = useCallback(
    (id: number) => {
      dispatch(fetchSystemLogById(id));
    },
    [dispatch]
  );

  const searchSystemLog = useCallback(
    (keyword: string) => {
      dispatch(searchSystemLogs(keyword));
    },
    [dispatch]
  );

  return {
    systemLogs,
    selectedSystemLog,
    loading,
    error,
    addNewSystemLog,
    updateSystemLog,
    deleteSystemLog,
    reloadSystemLogs,
    getSystemLogById,
    searchSystemLog,
  };
};
