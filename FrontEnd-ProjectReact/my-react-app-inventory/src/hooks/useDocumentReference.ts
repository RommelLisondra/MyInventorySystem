import { useEffect, useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";
import type { AppDispatch, RootState } from "../app/store";
import {
  fetchDocumentReferences,
  fetchDocumentReferenceById,
  searchDocumentReferences,
  addDocumentReference,
  updateDocumentReferenceById,
  deleteDocumentReferenceById,
} from "../features/documentReference/documentReferenceThunk";
import type { DocumentReference } from "../types/documentReference";

interface UseDocumentReferenceReturn {
  documentReference: DocumentReference[];
  selectedDocumentReference: DocumentReference | null;
  loading: boolean;
  error: string | null;
  addNewDocumentReference: (DocumentReference: DocumentReference) => void;
  updateDocumentReference: (DocumentReference: DocumentReference) => void;
  deleteDocumentReference: (id: number) => void;
  reloadDocumentReferences: () => void;
  getDocumentReferenceById: (id: number) => void;
  searchDocumentReference: (name: string) => void;
}

export const useDocumentReference = (): UseDocumentReferenceReturn => {
  const dispatch = useDispatch<AppDispatch>();
  const { documentReference,selectedDocumentReference, loading, error } = useSelector(
    (state: RootState) => state.documentReference
  );

  // Load on mount
  useEffect(() => {
    dispatch(fetchDocumentReferences());
  }, [dispatch]);

  const addNewDocumentReference = useCallback(
    (documentReference: DocumentReference) => {
      dispatch(addDocumentReference(documentReference));
    },
    [dispatch]
  );

  const updateDocumentReference = useCallback(
    (documentReference: DocumentReference) => {
      dispatch(updateDocumentReferenceById(documentReference));
    },
    [dispatch]
  );

  const deleteDocumentReference = useCallback(
    (id: number) => {
      if (confirm("Are you sure you want to delete this DocumentReference?")) {
        dispatch(deleteDocumentReferenceById(id));
      }
    },
    [dispatch]
  );

  const reloadDocumentReferences = useCallback(() => {
    dispatch(fetchDocumentReferences());
  }, [dispatch]);

  const getDocumentReferenceById = useCallback(
    (id: number) => {
      dispatch(fetchDocumentReferenceById(id));
    },
    [dispatch]
  );

  const searchDocumentReference = useCallback((keyword: string) => {
      dispatch(searchDocumentReferences(keyword));
    },
    [dispatch]
  );

  return {
    documentReference,
    selectedDocumentReference,
    loading,
    error,
    addNewDocumentReference,
    updateDocumentReference,
    deleteDocumentReference,
    reloadDocumentReferences,
    getDocumentReferenceById,
    searchDocumentReference,
  };
};
