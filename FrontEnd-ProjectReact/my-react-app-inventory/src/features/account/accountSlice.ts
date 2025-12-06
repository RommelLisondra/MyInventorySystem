
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchAccount,
  fetchAccountById,
  addAccount,
  updateAccountById,
  deleteAccountById,
  searchAccount,
  fetchAccountPaged,
} from "./accountThunk";
import type { Account } from "../../types/account";
import type { PagedResponse } from "../../types/pagedResponse";

interface AccountState {
  account: Account[];
  selectedAccount: Account | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: AccountState = {
  account: [],
  selectedAccount: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const AccountSlice = createSlice({
  name: "Account",
  initialState,
  reducers: {
    clearSelectedAccount: (state) => {
      state.selectedAccount = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchAccount.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAccount.fulfilled, (state, action: PayloadAction<Account[]>) => {
        state.loading = false;
        state.account = action.payload;
      })
      .addCase(fetchAccount.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchAccountPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchAccountPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Account[]>>) => {
          state.loading = false;
          state.account = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchAccountPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchAccountById.fulfilled, (state, action: PayloadAction<Account>) => {
        state.selectedAccount = action.payload;
      })

      // Add
      .addCase(addAccount.fulfilled, (state: { account: Account[]; }, action: PayloadAction<Account>) => {
        state.account.push(action.payload);
      })

      // Update
      .addCase(updateAccountById.fulfilled, (state: { account: any[]; }, action: PayloadAction<Account>) => {
        const index = state.account.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.account[index] = action.payload;
      })

      // Delete
      .addCase(deleteAccountById.fulfilled, (state: { account: any[]; }, action: PayloadAction<number>) => {
        state.account = state.account.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchAccount.fulfilled, (state: { account: Account[]; }, action: PayloadAction<Account[]>) => {
        state.account = action.payload;
      });
  },
});

export const { clearSelectedAccount } = AccountSlice.actions;
export default AccountSlice.reducer;
