
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchAuditTrails,
  fetchAuditTrailById,
  addAuditTrail,
  updateAuditTrailById,
  deleteAuditTrailById,
  searchAuditTrails,
  fetchAuditTrailsPaged,
} from "./auditTrailThunk";
import type { AuditTrail } from "../../types/auditTrail";
import type { PagedResponse } from "../../types/pagedResponse";

interface AuditTrailState {
  auditTrail: AuditTrail[];
  selectedAuditTrail: AuditTrail | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: AuditTrailState = {
  auditTrail: [],
  selectedAuditTrail: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const AuditTrailSlice = createSlice({
  name: "AuditTrail",
  initialState,
  reducers: {
    clearSelectedAuditTrail: (state) => {
      state.selectedAuditTrail = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchAuditTrails.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAuditTrails.fulfilled, (state, action: PayloadAction<AuditTrail[]>) => {
        state.loading = false;
        state.auditTrail = action.payload;
      })
      .addCase(fetchAuditTrails.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchAuditTrailsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAuditTrailsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<AuditTrail[]>>) => {
          state.loading = false;
          state.auditTrail = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchAuditTrailsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchAuditTrailById.fulfilled, (state, action: PayloadAction<AuditTrail>) => {
        state.selectedAuditTrail = action.payload;
      })

      // Add
      .addCase(addAuditTrail.fulfilled, (state: { auditTrail: AuditTrail[]; }, action: PayloadAction<AuditTrail>) => {
        state.auditTrail.push(action.payload);
      })

      // Update
      .addCase(updateAuditTrailById.fulfilled, (state: { auditTrail: any[]; }, action: PayloadAction<AuditTrail>) => {
        const index = state.auditTrail.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.auditTrail[index] = action.payload;
      })

      // Delete
      .addCase(deleteAuditTrailById.fulfilled, (state: { auditTrail: any[]; }, action: PayloadAction<number>) => {
        state.auditTrail = state.auditTrail.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchAuditTrails.fulfilled, (state: { auditTrail: AuditTrail[]; }, action: PayloadAction<AuditTrail[]>) => {
        state.auditTrail = action.payload;
      });
  },
});

export const { clearSelectedAuditTrail } = AuditTrailSlice.actions;
export default AuditTrailSlice.reducer;
