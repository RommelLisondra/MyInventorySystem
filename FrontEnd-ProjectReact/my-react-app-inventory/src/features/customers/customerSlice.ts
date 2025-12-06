
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchCustomers,
  fetchCustomerById,
  addCustomer,
  updateCustomerById,
  deleteCustomerById,
  searchCustomers,
  fetchCustomersPaged,
} from "./customerThunk";
import type { Customer } from "../../types/customer";
import type { PagedResponse } from "../../types/pagedResponse";

interface CustomerState {
  customers: Customer[];
  selectedCustomer: Customer | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: CustomerState = {
  customers: [],
  selectedCustomer: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const customerSlice = createSlice({
  name: "customer",
  initialState,
  reducers: {
    clearSelectedCustomer: (state) => {
      state.selectedCustomer = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchCustomers.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCustomers.fulfilled, (state, action: PayloadAction<Customer[]>) => {
        state.loading = false;
        state.customers = action.payload;
      })
      .addCase(fetchCustomers.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchCustomersPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCustomersPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Customer[]>>) => {
          state.loading = false;
          state.customers = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchCustomersPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchCustomerById.fulfilled, (state, action: PayloadAction<Customer>) => {
        state.selectedCustomer = action.payload;
      })

      // Add
      .addCase(addCustomer.fulfilled, (state: { customers: Customer[]; }, action: PayloadAction<Customer>) => {
        state.customers.push(action.payload);
      })

      // Update
      .addCase(updateCustomerById.fulfilled, (state: { customers: any[]; }, action: PayloadAction<Customer>) => {
        const index = state.customers.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.customers[index] = action.payload;
      })

      // Delete
      .addCase(deleteCustomerById.fulfilled, (state: { customers: any[]; }, action: PayloadAction<number>) => {
        state.customers = state.customers.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchCustomers.fulfilled, (state: { customers: Customer[]; }, action: PayloadAction<Customer[]>) => {
        state.customers = action.payload;
      });
  },
});

export const { clearSelectedCustomer } = customerSlice.actions;
export default customerSlice.reducer;
