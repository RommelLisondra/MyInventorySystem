
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchBranchs,
  fetchBranchById,
  addBranch,
  updateBranchById,
  deleteBranchById,
  searchBranch,
  fetchBranchsPaged,
} from "./branchThunk";
import type { Branch } from "../../types/branch";
import type { PagedResponse } from "../../types/pagedResponse";

interface BranchState {
  branch: Branch[];
  selectedBranch: Branch | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: BranchState = {
  branch: [],
  selectedBranch: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const BranchSlice = createSlice({
  name: "Branch",
  initialState,
  reducers: {
    clearSelectedBranch: (state) => {
      state.selectedBranch = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchBranchs.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchBranchs.fulfilled, (state, action: PayloadAction<Branch[]>) => {
        state.loading = false;
        state.branch = action.payload;
      })
      .addCase(fetchBranchs.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchBranchsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchBranchsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Branch[]>>) => {
          state.loading = false;
          state.branch = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchBranchsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchBranchById.fulfilled, (state, action: PayloadAction<Branch>) => {
        state.selectedBranch = action.payload;
      })

      // Add
      .addCase(addBranch.fulfilled, (state: { branch: Branch[]; }, action: PayloadAction<Branch>) => {
        state.branch.push(action.payload);
      })

      // Update
      .addCase(updateBranchById.fulfilled, (state: { branch: any[]; }, action: PayloadAction<Branch>) => {
        const index = state.branch.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.branch[index] = action.payload;
      })

      // Delete
      .addCase(deleteBranchById.fulfilled, (state: { branch: any[]; }, action: PayloadAction<number>) => {
        state.branch = state.branch.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchBranch.fulfilled, (state: { branch: Branch[]; }, action: PayloadAction<Branch[]>) => {
        state.branch = action.payload;
      });
  },
});

export const { clearSelectedBranch } = BranchSlice.actions;
export default BranchSlice.reducer;
