import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchAuditTrails,
  fetchAuditTrailById,
  searchAuditTrails,
  addAuditTrail,
  updateAuditTrailById,
  deleteAuditTrailById,
} from "../features/auditTrail/auditTrailThunk";
import type { AuditTrail } from "../types/auditTrail";

interface UseAuditTrailReturn {
  auditTrail: AuditTrail[];
  selectedAuditTrail: AuditTrail | null;
  loading: boolean;
  error: string | null;
  addNewAuditTrail: (auditTrail: AuditTrail) => void;
  updateAuditTrail: (auditTrail: AuditTrail) => void;
  deleteAuditTrail: (id: number) => void;
  reloadAuditTrails: () => void;
  getAuditTrailById: (id: number) => void;
  searchAuditTrail: (name: string) => void;
}

export const useAuditTrail = (): UseAuditTrailReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { auditTrail, selectedAuditTrail, loading, error } = useSelector(
    (state: RootState) => state.auditTrail
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchAuditTrails());
  }, [dispatch]);

  const addNewAuditTrail = useCallback(
    (AuditTrail: AuditTrail) => {
      dispatch(addAuditTrail(AuditTrail));
    },
    [dispatch]
  );

  const updateAuditTrail = useCallback(
    (AuditTrail: AuditTrail) => {
      dispatch(updateAuditTrailById(AuditTrail));
    },
    [dispatch]
  );

  const deleteAuditTrail = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this AuditTrail?")) {
        dispatch(deleteAuditTrailById(id));
      }
    },
    [dispatch]
  );

  const reloadAuditTrails = useCallback(() => {
    dispatch(fetchAuditTrails());
  }, [dispatch]);

  const getAuditTrailById = useCallback(
    (id: number) => {
      dispatch(fetchAuditTrailById(id));
    },
    [dispatch]
  );

  const searchAuditTrail = useCallback((keyword: string) => {
      dispatch(searchAuditTrails(keyword));
    },
    [dispatch]
  );

  return {
    auditTrail,
    selectedAuditTrail,
    loading,
    error,
    addNewAuditTrail,
    updateAuditTrail,
    deleteAuditTrail,
    reloadAuditTrails,
    getAuditTrailById,
    searchAuditTrail,
  };
};
