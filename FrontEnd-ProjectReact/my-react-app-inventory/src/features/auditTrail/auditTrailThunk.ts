
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as auditTrailService from "../../services/auditTrailService";
import type { AuditTrail } from "../../types/auditTrail";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all AuditTrails
export const fetchAuditTrails = createAsyncThunk(
  "auditTrail/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await auditTrailService.getAuditTrails();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all AuditTrails By page
export const fetchAuditTrailsPaged = createAsyncThunk<PagedResponse<AuditTrail[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "auditTrail/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await auditTrailService.getAuditTrailsPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single AuditTrail
export const fetchAuditTrailById = createAsyncThunk(
  "auditTrail/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await auditTrailService.getAuditTrailById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new AuditTrail
export const addAuditTrail = createAsyncThunk(
  "auditTrail/add",
  async (AuditTrail: AuditTrail, thunkAPI) => {
    try {
      return await auditTrailService.createAuditTrail(AuditTrail);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateAuditTrailById = createAsyncThunk<AuditTrail, AuditTrail>(
  "auditTrail/update",
  async (AuditTrail, thunkAPI) => {
    try {
      await auditTrailService.updateAuditTrail(AuditTrail);
      // Return updated AuditTrail so reducer gets correct payload type
      return AuditTrail;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete AuditTrail
export const deleteAuditTrailById = createAsyncThunk<number, number>(
  "auditTrail/delete",
  async (id, thunkAPI) => {
    try {
      await auditTrailService.deleteAuditTrail(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search AuditTrails
export const searchAuditTrails = createAsyncThunk(
  "auditTrail/search",
  async (name: string, thunkAPI) => {
    try {
      return await auditTrailService.searchAuditTrails(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

