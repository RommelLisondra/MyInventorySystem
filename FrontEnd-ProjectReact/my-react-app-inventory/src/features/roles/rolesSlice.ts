
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchRoles,
  fetchRoleById,
  addRole,
  updateRoleById,
  deleteRoleById,
  searchRoles,
  fetchRolesPaged
} from "./rolesThunk";
import type { Role } from "../../types/role";
import type { PagedResponse } from "../../types/pagedResponse";

interface roleState {
  role: Role[];
  selectedrole: Role | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: roleState = {
  role: [],
  selectedrole: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const roleSlice = createSlice({
  name: "role",
  initialState,
  reducers: {
    clearSelectedItem: (state) => {
      state.selectedrole = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchRoles.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchRoles.fulfilled, (state, action: PayloadAction<Role[]>) => {
        state.loading = false;
        state.role = action.payload;
      })
      .addCase(fetchRoles.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
      
      // Fetch all by page
      .addCase(fetchRolesPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchRolesPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Role[]>>) => {
        state.loading = false;
        state.role = action.payload.data;
        state.pageNumber = action.payload.pageNumber;
        state.totalPages = action.payload.totalPages;
        state.totalRecords = action.payload.totalRecords;
        state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchRolesPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchRoleById.fulfilled, (state, action: PayloadAction<Role>) => {
        state.selectedrole = action.payload;
      })

      // Add
      .addCase(addRole.fulfilled, (state, action: PayloadAction<Role>) => {
        state.role.push(action.payload);
      })

      // Update
      .addCase(updateRoleById.fulfilled, (state, action: PayloadAction<Role>) => {
        const index = state.role.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.role[index] = action.payload;
      })

      // Delete
      .addCase(deleteRoleById.fulfilled, (state, action: PayloadAction<number>) => {
        state.role = state.role.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchRoles.fulfilled, (state, action: PayloadAction<Role[]>) => {
        state.role = action.payload;
      });
  },
});

export const { clearSelectedItem } = roleSlice.actions;
export default roleSlice.reducer;
