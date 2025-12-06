
import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import {
  fetchCompanys,
  fetchCompanyById,
  addCompany,
  updateCompanyById,
  deleteCompanyById,
  searchCompanys,
  fetchCompanysPaged,
} from "./companyThunk";
import type { Company } from "../../types/company";
import type { PagedResponse } from "../../types/pagedResponse";

interface CompanyState {
  company: Company[];
  selectedCompany: Company | null;
  loading: boolean;
  error: string | null;
  pageNumber: number;
  totalPages: number;
  totalRecords: number;
  pageSize: number;
}

const initialState: CompanyState = {
  company: [],
  selectedCompany: null,
  loading: false,
  error: null,
  pageNumber: 1,
  totalPages: 0,
  totalRecords: 0,
  pageSize: 20,
};

export const CompanySlice = createSlice({
  name: "Company",
  initialState,
  reducers: {
    clearSelectedCompany: (state) => {
      state.selectedCompany = null;
    },
  },
  extraReducers: (builder) => {
    builder
      // Fetch all
      .addCase(fetchCompanys.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCompanys.fulfilled, (state, action: PayloadAction<Company[]>) => {
        state.loading = false;
        state.company = action.payload;
      })
      .addCase(fetchCompanys.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })
    // Fetch all by page
      .addCase(fetchCompanysPaged.pending, (state) => {
        state.loading = true;
        state.error = null;
      })
      .addCase(fetchCompanysPaged.fulfilled,(state, action: PayloadAction<PagedResponse<Company[]>>) => {
          state.loading = false;
          state.company = action.payload.data;
          state.pageNumber = action.payload.pageNumber;
          state.totalPages = action.payload.totalPages;
          state.totalRecords = action.payload.totalRecords;
          state.pageSize = action.payload.pageSize;
        }
      )
      .addCase(fetchCompanysPaged.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
      })

      // Fetch by ID
      .addCase(fetchCompanyById.fulfilled, (state, action: PayloadAction<Company>) => {
        state.selectedCompany = action.payload;
      })

      // Add
      .addCase(addCompany.fulfilled, (state: { company: Company[]; }, action: PayloadAction<Company>) => {
        state.company.push(action.payload);
      })

      // Update
      .addCase(updateCompanyById.fulfilled, (state: { company: any[]; }, action: PayloadAction<Company>) => {
        const index = state.company.findIndex((c) => c.id === action.payload.id);
        if (index >= 0) state.company[index] = action.payload;
      })

      // Delete
      .addCase(deleteCompanyById.fulfilled, (state: { company: any[]; }, action: PayloadAction<number>) => {
        state.company = state.company.filter((c) => c.id !== action.payload);
      })

      // Search
      .addCase(searchCompanys.fulfilled, (state: { company: Company[]; }, action: PayloadAction<Company[]>) => {
        state.company = action.payload;
      });
  },
});

export const { clearSelectedCompany } = CompanySlice.actions;
export default CompanySlice.reducer;
