
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as brandService from "../../services/brandService";
import type { Brand } from "../../types/brand";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Brands
export const fetchBrands = createAsyncThunk("brand/fetchAll", 
    async (_, thunkAPI) => {
        try {
            return await brandService.getBrands();
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch all Brands By page
export const fetchBrandsPaged = createAsyncThunk<PagedResponse<Brand[]>, { pageNumber: number; pageSize: number }, { rejectValue: string }> 
("brand/fetchPaged",
    async ({ pageNumber, pageSize }, thunkAPI) => {
        try {
            return await brandService.getBrandsPaged(pageNumber, pageSize);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch single Brand
export const fetchBrandById = createAsyncThunk("brand/fetchById",
    async (id: number, thunkAPI) => {
        try {
            return await brandService.getBrandById(id);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Add new Brand
export const addBrand = createAsyncThunk(
  "brand/add",
    async (brand: Brand, thunkAPI) => {
        try {
            return await brandService.createBrand(brand);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

export const updateBrandById = createAsyncThunk<Brand, Brand>(
  "brand/update",
    async (brand, thunkAPI) => {
        try {
            await brandService.updateBrand(brand);
        // Return updated Brand so reducer gets correct payload type
            return brand;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Delete Brand
export const deleteBrandById = createAsyncThunk<number, number>(
  "brand/delete",
    async (id, thunkAPI) => {
        try {
            await brandService.deleteBrand(id);
            return id;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Search Brands
export const searchBrands = createAsyncThunk(
  "brand/search",
    async (name: string, thunkAPI) => {
        try {
            return await brandService.searchBrands(name);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

