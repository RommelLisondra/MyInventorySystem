import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchSystemSettings,
  fetchSystemSettingById,
  searchSystemSettings,
  addSystemSetting,
  updateSystemSettingById,
  deleteSystemSettingById,
} from "../features/systemSettings/systemSettingsThunk";
import type { SystemSetting } from "../types/systemSetting";

interface UseSystemSettingReturn {
  systemSettings: SystemSetting[];
  selectedSystemSetting: SystemSetting | null;
  loading: boolean;
  error: string | null;
  addNewSystemSetting: (SystemSetting: SystemSetting) => void;
  updateSystemSetting: (SystemSetting: SystemSetting) => void;
  deleteSystemSetting: (id: number) => void;
  reloadSystemSettings: () => void;
  getSystemSettingById: (id: number) => void;
  searchSystemSetting: (name: string) => void;
}

export const useSystemSetting = (): UseSystemSettingReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { systemSettings, selectedSystemSetting, loading, error } = useSelector(
    (state: RootState) => state.systemSettings
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchSystemSettings());
  }, [dispatch]);

  const addNewSystemSetting = useCallback(
    (systemSetting: SystemSetting) => {
      dispatch(addSystemSetting(systemSetting));
    },
    [dispatch]
  );

  const updateSystemSetting = useCallback(
    (systemSetting: SystemSetting) => {
      dispatch(updateSystemSettingById(systemSetting));
    },
    [dispatch]
  );

  const deleteSystemSetting = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this SystemSetting?")) {
        dispatch(deleteSystemSettingById(id));
      }
    },
    [dispatch]
  );

  const reloadSystemSettings = useCallback(() => {
    dispatch(fetchSystemSettings());
  }, [dispatch]);

  const getSystemSettingById = useCallback(
    (id: number) => {
      dispatch(fetchSystemSettingById(id));
    },
    [dispatch]
  );

  const searchSystemSetting = useCallback(
    (keyword: string) => {
      dispatch(searchSystemSettings(keyword));
    },
    [dispatch]
  );

  return {
    systemSettings,
    selectedSystemSetting,
    loading,
    error,
    addNewSystemSetting,
    updateSystemSetting,
    deleteSystemSetting,
    reloadSystemSettings,
    getSystemSettingById,
    searchSystemSetting,
  };
};
