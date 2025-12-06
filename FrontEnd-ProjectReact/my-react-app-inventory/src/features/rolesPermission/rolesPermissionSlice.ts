
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchRolePermissions,
  fetchRolePermissionById,
  addRolePermission,
  updateRolePermissionById,
  deleteRolePermissionById,
  searchRolePermissions,
  fetchRolePermissionsPaged
} from "./rolesPermissionThunk";
import type { RolePermission } from "../../types/rolePermission";
import type { PagedResponse } from "../../types/pagedResponse";

interface rolePermissionState {
  rolePermission: RolePermission[];
  selectedrolePermission: RolePermission | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: rolePermissionState = {
  rolePermission: [],
  selectedrolePermission: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const rolePermissionSlice = createSlice({
  name: "rolePermission",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedrolePermission = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchRolePermissions.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchRolePermissions.fulfilled, (state, action: PayloadAction<RolePermission[]>) => {
        state.loading = false;
        state.rolePermission = action.payload;
      })
      .addCase(fetchRolePermissions.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchRolePermissionsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchRolePermissionsPaged.fulfilled,(state, action: PayloadAction<PagedResponse<RolePermission[]>>) => {
        state.loading = false;
        state.rolePermission = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchRolePermissionsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchRolePermissionById.fulfilled, (state, action: PayloadAction<RolePermission>) => {
        state.selectedrolePermission = action.payload;
      })

      // Add
      .addCase(addRolePermission.fulfilled, (state, action: PayloadAction<RolePermission>) => {
        state.rolePermission.push(action.payload);
      })

      // Update
      .addCase(updateRolePermissionById.fulfilled, (state, action: PayloadAction<RolePermission>) => {
        const index = state.rolePermission.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.rolePermission[index] = action.payload;
      })

      // Delete
      .addCase(deleteRolePermissionById.fulfilled, (state, action: PayloadAction<number>) => {
        state.rolePermission = state.rolePermission.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchRolePermissions.fulfilled, (state, action: PayloadAction<RolePermission[]>) => {
        state.rolePermission = action.payload;
      });
  },
});

export const { clearSelectedItem } = rolePermissionSlice.actions;
export default rolePermissionSlice.reducer;
