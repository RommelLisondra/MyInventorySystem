
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as companyService from "../../services/companyService";
import type { Company } from "../../types/company";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Companys
export const fetchCompanys = createAsyncThunk(
  "company/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await companyService.getCompany();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all Companys By page
export const fetchCompanysPaged = createAsyncThunk<PagedResponse<Company[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "company/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await companyService.getCompanyPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single Company
export const fetchCompanyById = createAsyncThunk(
  "company/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await companyService.getCompanyById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new Company
export const addCompany = createAsyncThunk(
  "company/add",
  async (Company: Company, thunkAPI) => {
    try {
      return await companyService.createCompany(Company);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateCompanyById = createAsyncThunk<Company, Company>(
  "company/update",
  async (Company, thunkAPI) => {
    try {
      await companyService.updateCompany(Company);
      // Return updated Company so reducer gets correct payload type
      return Company;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete Company
export const deleteCompanyById = createAsyncThunk<number, number>(
  "company/delete",
  async (id, thunkAPI) => {
    try {
      await companyService.deleteCompany(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search Companys
export const searchCompany = createAsyncThunk(
  "company/search",
  async (name: string, thunkAPI) => {
    try {
      return await companyService.searchCompany(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

