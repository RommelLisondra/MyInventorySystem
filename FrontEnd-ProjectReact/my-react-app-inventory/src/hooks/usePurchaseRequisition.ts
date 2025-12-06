import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchpurchaseRequisitions,
  fetchpurchaseRequisitionById,
  searchpurchaseRequisitions,
  addpurchaseRequisition,
  updatepurchaseRequisitionById,
  deletepurchaseRequisitionById,
} from "../features/purchaseRequisitions/purchaseRequisitionsThunk";
import type { PurchaseRequisitionMaster } from "../types/purchaseRequisitionMaster";

interface UsePurchaseRequisitionReturn {
  purchaseRequisition: PurchaseRequisitionMaster[];
  selectedpurchaseRequisition: PurchaseRequisitionMaster | null;
  loading: boolean;
  error: string | null;
  addNewPurchaseRequisition: (PurchaseRequisition: PurchaseRequisitionMaster) => void;
  updatePurchaseRequisition: (PurchaseRequisition: PurchaseRequisitionMaster) => void;
  deletePurchaseRequisition: (id: number) => void;
  reloadPurchaseRequisitions: () => void;
  getPurchaseRequisitionById: (id: number) => void;
  searchPurchaseRequisition: (name: string) => void;
}

export const usePurchaseRequisition = (): UsePurchaseRequisitionReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { purchaseRequisition, selectedpurchaseRequisition, loading, error } = useSelector(
    (state: RootState) => state.purchaseRequisition
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchpurchaseRequisitions());
  }, [dispatch]);

  const addNewPurchaseRequisition = useCallback((purchaseRequisition: PurchaseRequisitionMaster) => {
      dispatch(addpurchaseRequisition(purchaseRequisition));
    },
    [dispatch]
  );

  const updatePurchaseRequisition = useCallback((purchaseRequisition: PurchaseRequisitionMaster) => {
      dispatch(updatepurchaseRequisitionById(purchaseRequisition));
    },
    [dispatch]
  );

  const deletePurchaseRequisition = useCallback((id: number) => {
      if (confirm("Are you sure you want to delete this PurchaseRequisition?")) {
        dispatch(deletepurchaseRequisitionById(id));
      }
    },
    [dispatch]
  );

  const reloadPurchaseRequisitions = useCallback(() => {
    dispatch(fetchpurchaseRequisitions());
  }, [dispatch]);

  const getPurchaseRequisitionById = useCallback((id: number) => {
      dispatch(fetchpurchaseRequisitionById(id));
    },
    [dispatch]
  );

  const searchPurchaseRequisition = useCallback((keyword: string) => {
      dispatch(searchpurchaseRequisitions(keyword));
    },
    [dispatch]
  );

  return {
    purchaseRequisition,
    selectedpurchaseRequisition,
    loading,
    error,
    addNewPurchaseRequisition,
    updatePurchaseRequisition,
    deletePurchaseRequisition,
    reloadPurchaseRequisitions,
    getPurchaseRequisitionById,
    searchPurchaseRequisition,
  };
};
