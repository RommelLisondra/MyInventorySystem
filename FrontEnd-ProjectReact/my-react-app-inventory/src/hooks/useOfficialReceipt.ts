import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchofficialReceipts,
  fetchofficialReceiptById,
  searchofficialReceipts,
  addofficialReceipt,
  updateofficialReceiptById,
  deleteofficialReceiptById,
} from "../features/officialReceipt/officialReceiptThunk";
import type { OfficialReceiptMaster } from "../types/officialReceiptMaster";

interface UseOfficialReceiptReturn {
  OfficialReceipt: OfficialReceiptMaster[];
  selectedOfficialReceipt: OfficialReceiptMaster | null;
  loading: boolean;
  error: string | null;
  addNewOfficialReceipt: (OfficialReceipt: OfficialReceiptMaster) => void;
  updateOfficialReceipt: (OfficialReceipt: OfficialReceiptMaster) => void;
  deleteOfficialReceipt: (id: number) => void;
  reloadOfficialReceipts: () => void;
  getOfficialReceiptById: (id: number) => void;
  searchOfficialReceipt: (name: string) => void;
}

export const useOfficialReceipt = (): UseOfficialReceiptReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { OfficialReceipt, selectedOfficialReceipt, loading, error } = useSelector(
    (state: RootState) => state.officialReceipt
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchofficialReceipts());
  }, [dispatch]);

  const addNewOfficialReceipt = useCallback(
    (OfficialReceipt: OfficialReceiptMaster) => {
      dispatch(addofficialReceipt(OfficialReceipt));
    },
    [dispatch]
  );

  const updateOfficialReceipt = useCallback(
    (OfficialReceipt: OfficialReceiptMaster) => {
      dispatch(updateofficialReceiptById(OfficialReceipt));
    },
    [dispatch]
  );

  const deleteOfficialReceipt = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this OfficialReceipt?")) {
        dispatch(deleteofficialReceiptById(id));
      }
    },
    [dispatch]
  );

  const reloadOfficialReceipts = useCallback(() => {
    dispatch(fetchofficialReceipts());
  }, [dispatch]);

  const getOfficialReceiptById = useCallback(
    (id: number) => {
      dispatch(fetchofficialReceiptById(id));
    },
    [dispatch]
  );

  const searchOfficialReceipt = useCallback((keyword: string) => {
      dispatch(searchofficialReceipts(keyword));
    },
    [dispatch]
  );

  return {
    OfficialReceipt,
    selectedOfficialReceipt,
    loading,
    error,
    addNewOfficialReceipt,
    updateOfficialReceipt,
    deleteOfficialReceipt,
    reloadOfficialReceipts,
    getOfficialReceiptById,
    searchOfficialReceipt,
  };
};
