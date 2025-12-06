import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchpurchaseOrders,
  fetchpurchaseOrderById,
  searchpurchaseOrders,
  addpurchaseOrder,
  updatepurchaseOrderById,
  deletepurchaseOrderById,
} from "../features/purchaseOrder/purchaseOrderThunk";
import type { PurchaseOrderMaster } from "../types/purchaseOrderMaster";

interface UsePurchaseOrderReturn {
  purchaseOrder: PurchaseOrderMaster[];
  selectedPurchaseOrder: PurchaseOrderMaster | null;
  loading: boolean;
  error: string | null;
  addNewPurchaseOrder: (PurchaseOrder: PurchaseOrderMaster) => void;
  updatePurchaseOrder: (PurchaseOrder: PurchaseOrderMaster) => void;
  deletePurchaseOrder: (id: number) => void;
  reloadPurchaseOrders: () => void;
  getPurchaseOrderById: (id: number) => void;
  searchPurchaseOrder: (name: string) => void;
}

export const usePurchaseOrder = (): UsePurchaseOrderReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { purchaseOrder, selectedPurchaseOrder, loading, error } = useSelector(
    (state: RootState) => state.purchaseOrder
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchpurchaseOrders());
  }, [dispatch]);

  const addNewPurchaseOrder = useCallback((PurchaseOrder: PurchaseOrderMaster) => {
      dispatch(addpurchaseOrder(PurchaseOrder));
    },
    [dispatch]
  );

  const updatePurchaseOrder = useCallback((PurchaseOrder: PurchaseOrderMaster) => {
      dispatch(updatepurchaseOrderById(PurchaseOrder));
    },
    [dispatch]
  );

  const deletePurchaseOrder = useCallback((id: number) => {
      if (confirm("Are you sure you want to delete this PurchaseOrder?")) {
        dispatch(deletepurchaseOrderById(id));
      }
    },
    [dispatch]
  );

  const reloadPurchaseOrders = useCallback(() => {
    dispatch(fetchpurchaseOrders());
  }, [dispatch]);

  const getPurchaseOrderById = useCallback((id: number) => {
      dispatch(fetchpurchaseOrderById(id));
    },
    [dispatch]
  );

  const searchPurchaseOrder = useCallback((keyword: string) => {
      dispatch(searchpurchaseOrders(keyword));
    },
    [dispatch]
  );

  return {
    purchaseOrder,
    selectedPurchaseOrder,
    loading,
    error,
    addNewPurchaseOrder,
    updatePurchaseOrder,
    deletePurchaseOrder,
    reloadPurchaseOrders,
    getPurchaseOrderById,
    searchPurchaseOrder,
  };
};
