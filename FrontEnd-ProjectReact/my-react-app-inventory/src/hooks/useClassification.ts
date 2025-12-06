import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchClassifications,
  fetchClassificationById,
  searchClassifications,
  addClassification,
  updateClassificationById,
  deleteClassificationById,
} from "../features/classification/classificationThunk";
import type { Classification } from "../types/classification";

interface UseClassificationReturn {
  classification: Classification[];
  selectedClassification: Classification | null;
  loading: boolean;
  error: string | null;
  addNewClassification: (classification: Classification) => void;
  updateClassification: (classification: Classification) => void;
  deleteClassification: (id: number) => void;
  reloadClassifications: () => void;
  getClassificationById: (id: number) => void;
  searchClassification: (name: string) => void;
}

export const useClassification = (): UseClassificationReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { classification, selectedClassification, loading, error } = useSelector(
    (state: RootState) => state.classification
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchClassifications());
  }, [dispatch]);

  const addNewClassification = useCallback(
    (classification: Classification) => {
      dispatch(addClassification(classification));
    },
    [dispatch]
  );

  const updateClassification = useCallback(
    (classification: Classification) => {
      dispatch(updateClassificationById(classification));
    },
    [dispatch]
  );

  const deleteClassification = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this Classification?")) {
        dispatch(deleteClassificationById(id));
      }
    },
    [dispatch]
  );

  const reloadClassifications = useCallback(() => {
    dispatch(fetchClassifications());
  }, [dispatch]);

  const getClassificationById = useCallback(
    (id: number) => {
      dispatch(fetchClassificationById(id));
    },
    [dispatch]
  );

  const searchClassification = useCallback((keyword: string) => {
      dispatch(searchClassifications(keyword));
    },
    [dispatch]
  );

  return {
    classification,
    selectedClassification,
    loading,
    error,
    addNewClassification,
    updateClassification,
    deleteClassification,
    reloadClassifications,
    getClassificationById,
    searchClassification,
  };
};
