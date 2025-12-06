
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchSystemLogs,
  fetchSystemLogById,
  addSystemLog,
  updateSystemLogById,
  deleteSystemLogById,
  searchSystemLogs,
  fetchSystemLogsPaged
} from "./systemLogsThunk";
import type { SystemLog } from "../../types/systemLog";
import type { PagedResponse } from "../../types/pagedResponse";

interface SystemLogState {
  systemLogs: SystemLog[];
  selectedSystemLog: SystemLog | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: SystemLogState = {
  systemLogs: [],
  selectedSystemLog: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const SystemLogSlice = createSlice({
  name: "SystemLog",
  initialState,
  reducers: {
    clearSelectedSystemLog: (state) => {
      state.selectedSystemLog = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchSystemLogs.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSystemLogs.fulfilled, (state, action: PayloadAction<SystemLog[]>) => {
        state.loading = false;
        state.systemLogs = action.payload;
      })
      .addCase(fetchSystemLogs.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch all by page
      .addCase(fetchSystemLogsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
       })
      .addCase(
        fetchSystemLogsPaged.fulfilled,
          (state, action: PayloadAction<PagedResponse<SystemLog[]>>) => {
            state.loading = false;
            state.systemLogs = action.payload.data;
            state.pageNumber = action.payload.pageNumber;
            state.totalPages = action.payload.totalPages;
            state.totalRecords = action.payload.totalRecords;
            state.pageSize = action.payload.pageSize;
      })
      .addCase(fetchSystemLogsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchSystemLogById.fulfilled, (state, action: PayloadAction<SystemLog>) => {
        state.selectedSystemLog = action.payload;
      })

      // Add
      .addCase(addSystemLog.fulfilled, (state, action: PayloadAction<SystemLog>) => {
        state.systemLogs.push(action.payload);
      })

      // Update
      .addCase(updateSystemLogById.fulfilled, (state, action: PayloadAction<SystemLog>) => {
        const index = state.systemLogs.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.systemLogs[index] = action.payload;
      })

      // Delete
      .addCase(deleteSystemLogById.fulfilled, (state, action: PayloadAction<number>) => {
        state.systemLogs = state.systemLogs.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchSystemLogs.fulfilled, (state, action: PayloadAction<SystemLog[]>) => {
        state.systemLogs = action.payload;
      });
  },
});

export const { clearSelectedSystemLog } = SystemLogSlice.actions;
export default SystemLogSlice.reducer;
