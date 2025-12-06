import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchDeliveryReceipts,
  fetchDeliveryReceiptById,
  searchDeliveryReceipts,
  addDeliveryReceipt,
  updateDeliveryReceiptById,
  deleteDeliveryReceiptById,
} from "../features/deliveryReceipt/deliveryReceiptThunk";
import type { DeliveryReceiptMaster } from "../types/deliveryReceiptMaster";

interface UseDeliveryReceiptReturn {
  deliveryReceipt: DeliveryReceiptMaster[];
  selectedDeliveryReceipt: DeliveryReceiptMaster | null;
  loading: boolean;
  error: string | null;
  addNewDeliveryReceipt: (DeliveryReceipt: DeliveryReceiptMaster) => void;
  updateDeliveryReceipt: (DeliveryReceipt: DeliveryReceiptMaster) => void;
  deleteDeliveryReceipt: (id: number) => void;
  reloadDeliveryReceipts: () => void;
  getDeliveryReceiptById: (id: number) => void;
  searchDeliveryReceipt: (name: string) => void;
}

export const useDeliveryReceipt = (): UseDeliveryReceiptReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { deliveryReceipt, selectedDeliveryReceipt, loading, error } = useSelector(
    (state: RootState) => state.deliveryReceipt
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchDeliveryReceipts());
  }, [dispatch]);

  const addNewDeliveryReceipt = useCallback(
    (deliveryReceipt: DeliveryReceiptMaster) => {
      dispatch(addDeliveryReceipt(deliveryReceipt));
    },
    [dispatch]
  );

  const updateDeliveryReceipt = useCallback(
    (deliveryReceipt: DeliveryReceiptMaster) => {
      dispatch(updateDeliveryReceiptById(deliveryReceipt));
    },
    [dispatch]
  );

  const deleteDeliveryReceipt = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this DeliveryReceipt?")) {
        dispatch(deleteDeliveryReceiptById(id));
      }
    },
    [dispatch]
  );

  const reloadDeliveryReceipts = useCallback(() => {
    dispatch(fetchDeliveryReceipts());
  }, [dispatch]);

  const getDeliveryReceiptById = useCallback(
    (id: number) => {
      dispatch(fetchDeliveryReceiptById(id));
    },
    [dispatch]
  );

  const searchDeliveryReceipt = useCallback((keyword: string) => {
      dispatch(searchDeliveryReceipts(keyword));
    },
    [dispatch]
  );

  return {
    deliveryReceipt,
    selectedDeliveryReceipt,
    loading,
    error,
    addNewDeliveryReceipt,
    updateDeliveryReceipt,
    deleteDeliveryReceipt,
    reloadDeliveryReceipts,
    getDeliveryReceiptById,
    searchDeliveryReceipt,
  };
};
