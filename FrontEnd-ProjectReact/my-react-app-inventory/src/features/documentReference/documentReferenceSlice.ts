
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchDocumentReferences,
  fetchDocumentReferenceById,
  addDocumentReference,
  updateDocumentReferenceById,
  deleteDocumentReferenceById,
  searchDocumentReferences,
  fetchDocumentReferencesPaged,
} from "./documentReferenceThunk";
import type { DocumentReference } from "../../types/documentReference";
import type { PagedResponse } from "../../types/pagedResponse";

interface DocumentReferenceState {
  documentReference: DocumentReference[];
  selectedDocumentReference: DocumentReference | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: DocumentReferenceState = {
  documentReference: [],
  selectedDocumentReference: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const DocumentReferenceSlice = createSlice({
  name: "DocumentReference",
  initialState,
  reducers: {
    clearSelectedDocumentReference: (state) => {
      state.selectedDocumentReference = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchDocumentReferences.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchDocumentReferences.fulfilled, (state, action: PayloadAction<DocumentReference[]>) => {
        state.loading = false;
        state.documentReference = action.payload;
      })
      .addCase(fetchDocumentReferences.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchDocumentReferencesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchDocumentReferencesPaged.fulfilled,(state, action: PayloadAction<PagedResponse<DocumentReference[]>>) => {
          state.loading = false;
          state.documentReference = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchDocumentReferencesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchDocumentReferenceById.fulfilled, (state, action: PayloadAction<DocumentReference>) => {
        state.selectedDocumentReference = action.payload;
      })

      // Add
      .addCase(addDocumentReference.fulfilled, (state: { documentReference: DocumentReference[]; }, action: PayloadAction<DocumentReference>) => {
        state.documentReference.push(action.payload);
      })

      // Update
      .addCase(updateDocumentReferenceById.fulfilled, (state: { documentReference: any[]; }, action: PayloadAction<DocumentReference>) => {
        const index = state.documentReference.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.documentReference[index] = action.payload;
      })

      // Delete
      .addCase(deleteDocumentReferenceById.fulfilled, (state: { documentReference: any[]; }, action: PayloadAction<number>) => {
        state.documentReference = state.documentReference.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchDocumentReferences.fulfilled, (state: { documentReference: DocumentReference[]; }, action: PayloadAction<DocumentReference[]>) => {
        state.documentReference = action.payload;
      });
  },
});

export const { clearSelectedDocumentReference } = DocumentReferenceSlice.actions;
export default DocumentReferenceSlice.reducer;
