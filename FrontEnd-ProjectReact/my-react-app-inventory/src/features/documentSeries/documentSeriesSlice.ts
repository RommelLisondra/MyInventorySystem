
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchDocumentSeriess,
  fetchDocumentSeriesById,
  addDocumentSeries,
  updateDocumentSeriesById,
  deleteDocumentSeriesById,
  searchDocumentSeriess,
  fetchDocumentSeriessPaged,
} from "./documentSeriesThunk";
import type { DocumentSeries } from "../../types/documentSeries";
import type { PagedResponse } from "../../types/pagedResponse";

interface DocumentSeriesState {
  documentSeries: DocumentSeries[];
  selectedDocumentSeries: DocumentSeries | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: DocumentSeriesState = {
  documentSeries: [],
  selectedDocumentSeries: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const DocumentSeriesSlice = createSlice({
  name: "documentSeries",
  initialState,
  reducers: {
    clearSelectedDocumentSeries: (state) => {
      state.selectedDocumentSeries = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchDocumentSeriess.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchDocumentSeriess.fulfilled, (state, action: PayloadAction<DocumentSeries[]>) => {
        state.loading = false;
        state.documentSeries = action.payload;
      })
      .addCase(fetchDocumentSeriess.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchDocumentSeriessPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchDocumentSeriessPaged.fulfilled,(state, action: PayloadAction<PagedResponse<DocumentSeries[]>>) => {
          state.loading = false;
          state.documentSeries = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchDocumentSeriessPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchDocumentSeriesById.fulfilled, (state, action: PayloadAction<DocumentSeries>) => {
        state.selectedDocumentSeries = action.payload;
      })

      // Add
      .addCase(addDocumentSeries.fulfilled, (state: { documentSeries: DocumentSeries[]; }, action: PayloadAction<DocumentSeries>) => {
        state.documentSeries.push(action.payload);
      })

      // Update
      .addCase(updateDocumentSeriesById.fulfilled, (state: { documentSeries: any[]; }, action: PayloadAction<DocumentSeries>) => {
        const index = state.documentSeries.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.documentSeries[index] = action.payload;
      })

      // Delete
      .addCase(deleteDocumentSeriesById.fulfilled, (state: { documentSeries: any[]; }, action: PayloadAction<number>) => {
        state.documentSeries = state.documentSeries.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchDocumentSeriess.fulfilled, (state: { documentSeries: DocumentSeries[]; }, action: PayloadAction<DocumentSeries[]>) => {
        state.documentSeries = action.payload;
      });
  },
});

export const { clearSelectedDocumentSeries } = DocumentSeriesSlice.actions;
export default DocumentSeriesSlice.reducer;
