import type { RootState } from "../../app/store";

export const selectDocumentReferences = (state: RootState) => state.documentReference.documentReference;
export const selectDocumentReferenceLoading = (state: RootState) => state.documentReference.loading;
export const selectDocumentReferenceError = (state: RootState) => state.documentReference.error;

// Optional: selector para sa isang DocumentReference by id
export const selectDocumentReferenceById = (id: number) => (state: RootState) =>
  state.documentReference.documentReference.find((c) => c.id === id);
