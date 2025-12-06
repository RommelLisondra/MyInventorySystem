
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchUserAccounts,
  fetchUserAccountById,
  addUserAccount,
  updateUserAccountById,
  deleteUserAccountById,
  searchUserAccounts,
  fetchUserAccountsPaged
} from "./userAccountsThunk";
import type { UserAccount } from "../../types/userAccount";
import type { PagedResponse } from "../../types/pagedResponse";

interface UserAccountState {
  userAccounts: UserAccount[];
  selectedUserAccount: UserAccount | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: UserAccountState = {
  userAccounts: [],
  selectedUserAccount: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const UserAccountSlice = createSlice({
  name: "userAccount",
  initialState,
  reducers: {
    clearSelectedUserAccount: (state) => {
      state.selectedUserAccount = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchUserAccounts.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchUserAccounts.fulfilled, (state, action: PayloadAction<UserAccount[]>) => {
        state.loading = false;
        state.userAccounts = action.payload;
      })
      .addCase(fetchUserAccounts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch all by page
      .addCase(fetchUserAccountsPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
       })
      .addCase(
        fetchUserAccountsPaged.fulfilled,
          (state, action: PayloadAction<PagedResponse<UserAccount[]>>) => {
            state.loading = false;
            state.userAccounts = action.payload.data;
            state.pageNumber = action.payload.pageNumber;
            state.totalPages = action.payload.totalPages;
            state.totalRecords = action.payload.totalRecords;
            state.pageSize = action.payload.pageSize;
      })
      .addCase(fetchUserAccountsPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchUserAccountById.fulfilled, (state, action: PayloadAction<UserAccount>) => {
        state.selectedUserAccount = action.payload;
      })

      // Add
      .addCase(addUserAccount.fulfilled, (state, action: PayloadAction<UserAccount>) => {
        state.userAccounts.push(action.payload);
      })

      // Update
      .addCase(updateUserAccountById.fulfilled, (state, action: PayloadAction<UserAccount>) => {
        const index = state.userAccounts.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.userAccounts[index] = action.payload;
      })

      // Delete
      .addCase(deleteUserAccountById.fulfilled, (state, action: PayloadAction<number>) => {
        state.userAccounts = state.userAccounts.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchUserAccounts.fulfilled, (state, action: PayloadAction<UserAccount[]>) => {
        state.userAccounts = action.payload;
      });
  },
});

export const { clearSelectedUserAccount } = UserAccountSlice.actions;
export default UserAccountSlice.reducer;
