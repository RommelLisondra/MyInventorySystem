import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchDocumentSeriess,
  fetchDocumentSeriesById,
  searchDocumentSeriess,
  addDocumentSeries,
  updateDocumentSeriesById,
  deleteDocumentSeriesById,
} from "../features/documentSeries/documentSeriesThunk";
import type { DocumentSeries } from "../types/documentSeries";

interface UseDocumentSeriesReturn {
  documentSeries: DocumentSeries[];
  selectedDocumentSeries: DocumentSeries | null;
  loading: boolean;
  error: string | null;
  addNewDocumentSeries: (DocumentSeries: DocumentSeries) => void;
  updateDocumentSeries: (DocumentSeries: DocumentSeries) => void;
  deleteDocumentSeries: (id: number) => void;
  reloadDocumentSeriess: () => void;
  getDocumentSeriesById: (id: number) => void;
  searchDocumentSeries: (name: string) => void;
}

export const useDocumentSeries = (): UseDocumentSeriesReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { documentSeries, selectedDocumentSeries, loading, error } = useSelector(
    (state: RootState) => state.documentSeries
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchDocumentSeriess());
  }, [dispatch]);

  const addNewDocumentSeries = useCallback(
    (documentSeries: DocumentSeries) => {
      dispatch(addDocumentSeries(documentSeries));
    },
    [dispatch]
  );

  const updateDocumentSeries = useCallback(
    (documentSeries: DocumentSeries) => {
      dispatch(updateDocumentSeriesById(documentSeries));
    },
    [dispatch]
  );

  const deleteDocumentSeries = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this DocumentSeries?")) {
        dispatch(deleteDocumentSeriesById(id));
      }
    },
    [dispatch]
  );

  const reloadDocumentSeriess = useCallback(() => {
    dispatch(fetchDocumentSeriess());
  }, [dispatch]);

  const getDocumentSeriesById = useCallback(
    (id: number) => {
      dispatch(fetchDocumentSeriesById(id));
    },
    [dispatch]
  );

  const searchDocumentSeries = useCallback((keyword: string) => {
      dispatch(searchDocumentSeriess(keyword));
    },
    [dispatch]
  );

  return {
    documentSeries,
    selectedDocumentSeries,
    loading,
    error,
    addNewDocumentSeries,
    updateDocumentSeries,
    deleteDocumentSeries,
    reloadDocumentSeriess,
    getDocumentSeriesById,
    searchDocumentSeries,
  };
};
