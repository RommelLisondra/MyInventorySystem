import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchpurchaseReturns,
  fetchpurchaseReturnById,
  searchpurchaseReturns,
  addpurchaseReturn,
  updatepurchaseReturnById,
  deletepurchaseReturnById,
} from "../features/purchaseRetrun/purchaseRetrunThunk";
import type { PurchaseReturnMaster } from "../types/purchaseReturnMaster";

interface UsePurchaseReturnReturn {
  purchaseReturn: PurchaseReturnMaster[];
  selectedpurchaseReturn: PurchaseReturnMaster | null;
  loading: boolean;
  error: string | null;
  addNewPurchaseReturn: (purchaseReturn: PurchaseReturnMaster) => void;
  updatePurchaseReturn: (purchaseReturn: PurchaseReturnMaster) => void;
  deletePurchaseReturn: (id: number) => void;
  reloadPurchaseReturns: () => void;
  getPurchaseReturnById: (id: number) => void;
  searchPurchaseReturn: (name: string) => void;
}

export const usePurchaseReturn = (): UsePurchaseReturnReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { purchaseReturn, selectedpurchaseReturn, loading, error } = useSelector(
    (state: RootState) => state.purchaseRetrun
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchpurchaseReturns());
  }, [dispatch]);

  const addNewPurchaseReturn = useCallback(
    (purchaseReturn: PurchaseReturnMaster) => {
      dispatch(addpurchaseReturn(purchaseReturn));
    },
    [dispatch]
  );

  const updatePurchaseReturn = useCallback(
    (purchaseReturn: PurchaseReturnMaster) => {
      dispatch(updatepurchaseReturnById(purchaseReturn));
    },
    [dispatch]
  );

  const deletePurchaseReturn = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this PurchaseReturn?")) {
        dispatch(deletepurchaseReturnById(id));
      }
    },
    [dispatch]
  );

  const reloadPurchaseReturns = useCallback(() => {
    dispatch(fetchpurchaseReturns());
  }, [dispatch]);

  const getPurchaseReturnById = useCallback(
    (id: number) => {
      dispatch(fetchpurchaseReturnById(id));
    },
    [dispatch]
  );

  const searchPurchaseReturn = useCallback(
    (keyword: string) => {
      dispatch(searchpurchaseReturns(keyword));
    },
    [dispatch]
  );

  return {
    purchaseReturn,
    selectedpurchaseReturn,
    loading,
    error,
    addNewPurchaseReturn,
    updatePurchaseReturn,
    deletePurchaseReturn,
    reloadPurchaseReturns,
    getPurchaseReturnById,
    searchPurchaseReturn,
  };
};
