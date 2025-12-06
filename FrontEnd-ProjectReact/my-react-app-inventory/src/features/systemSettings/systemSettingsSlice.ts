
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchSystemSettings,
  fetchSystemSettingById,
  addSystemSetting,
  updateSystemSettingById,
  deleteSystemSettingById,
  searchSystemSettings,
  fetchSystemSettingsPaged
} from "./systemSettingsThunk";
import type { SystemSetting } from "../../types/systemSetting";
import type { PagedResponse } from "../../types/pagedResponse";

interface SystemSettingState {
  systemSettings: SystemSetting[];
  selectedSystemSetting: SystemSetting | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: SystemSettingState = {
  systemSettings: [],
  selectedSystemSetting: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const SystemSettingSlice = createSlice({
  name: "SystemSetting",
  initialState,
  reducers: {
    clearSelectedSystemSetting: (state) => {
      state.selectedSystemSetting = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchSystemSettings.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchSystemSettings.fulfilled, (state, action: PayloadAction<SystemSetting[]>) => {
        state.loading = false;
        state.systemSettings = action.payload;
      })
      .addCase(fetchSystemSettings.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch all by page
      .addCase(fetchSystemSettingsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
       })
      .addCase(
        fetchSystemSettingsPaged.fulfilled,
          (state, action: PayloadAction<PagedResponse<SystemSetting[]>>) => {
            state.loading = false;
            state.systemSettings = action.payload.data;
            state.pageNumber = action.payload.pageNumber;
            state.totalPages = action.payload.totalPages;
            state.totalRecords = action.payload.totalRecords;
            state.pageSize = action.payload.pageSize;
      })
      .addCase(fetchSystemSettingsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchSystemSettingById.fulfilled, (state, action: PayloadAction<SystemSetting>) => {
        state.selectedSystemSetting = action.payload;
      })

      // Add
      .addCase(addSystemSetting.fulfilled, (state, action: PayloadAction<SystemSetting>) => {
        state.systemSettings.push(action.payload);
      })

      // Update
      .addCase(updateSystemSettingById.fulfilled, (state, action: PayloadAction<SystemSetting>) => {
        const index = state.systemSettings.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.systemSettings[index] = action.payload;
      })

      // Delete
      .addCase(deleteSystemSettingById.fulfilled, (state, action: PayloadAction<number>) => {
        state.systemSettings = state.systemSettings.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchSystemSettings.fulfilled, (state, action: PayloadAction<SystemSetting[]>) => {
        state.systemSettings = action.payload;
      });
  },
});

export const { clearSelectedSystemSetting } = SystemSettingSlice.actions;
export default SystemSettingSlice.reducer;
