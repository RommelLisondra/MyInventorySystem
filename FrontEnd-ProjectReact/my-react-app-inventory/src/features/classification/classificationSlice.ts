
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchClassifications,
  fetchClassificationById,
  addClassification,
  updateClassificationById,
  deleteClassificationById,
  searchClassifications,
  fetchClassificationsPaged,
} from "./classificationThunk";
import type { Classification } from "../../types/classification";
import type { PagedResponse } from "../../types/pagedResponse";

interface ClassificationState {
  classification: Classification[];
  selectedClassification: Classification | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: ClassificationState = {
  classification: [],
  selectedClassification: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const ClassificationSlice = createSlice({
  name: "Classification",
  initialState,
  reducers: {
    clearSelectedClassification: (state) => {
      state.selectedClassification = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchClassifications.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchClassifications.fulfilled, (state, action: PayloadAction<Classification[]>) => {
        state.loading = false;
        state.classification = action.payload;
      })
      .addCase(fetchClassifications.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchClassificationsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchClassificationsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Classification[]>>) => {
          state.loading = false;
          state.classification = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchClassificationsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchClassificationById.fulfilled, (state, action: PayloadAction<Classification>) => {
        state.selectedClassification = action.payload;
      })

      // Add
      .addCase(addClassification.fulfilled, (state: { classification: Classification[]; }, action: PayloadAction<Classification>) => {
        state.classification.push(action.payload);
      })

      // Update
      .addCase(updateClassificationById.fulfilled, (state: { classification: any[]; }, action: PayloadAction<Classification>) => {
        const index = state.classification.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.classification[index] = action.payload;
      })

      // Delete
      .addCase(deleteClassificationById.fulfilled, (state: { classification: any[]; }, action: PayloadAction<number>) => {
        state.classification = state.classification.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchClassifications.fulfilled, (state: { classification: Classification[]; }, action: PayloadAction<Classification[]>) => {
        state.classification = action.payload;
      });
  },
});

export const { clearSelectedClassification } = ClassificationSlice.actions;
export default ClassificationSlice.reducer;
