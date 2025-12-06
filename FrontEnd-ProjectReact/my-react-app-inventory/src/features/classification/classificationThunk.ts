
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as classificationService from "../../services/classificationService";
import type { Classification } from "../../types/classification";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all Classifications
export const fetchClassifications = createAsyncThunk(
  "classification/fetchAll",
    async (_, thunkAPI) => {
        try {
            return await classificationService.getClassifications();
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch all Classifications By page
export const fetchClassificationsPaged = createAsyncThunk<PagedResponse<Classification[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "classification/fetchPaged",
    async ({ pageNumber, pageSize }, thunkAPI) => {
        try {
            return await classificationService.getClassificationsPaged(pageNumber, pageSize);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Fetch single Classification
export const fetchClassificationById = createAsyncThunk(
  "classification/fetchById",
    async (id: number, thunkAPI) => {
        try {
            return await classificationService.getClassificationById(id);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Add new Classification
export const addClassification = createAsyncThunk(
  "classification/add",
    async (classification: Classification, thunkAPI) => {
        try {
            return await classificationService.createClassification(classification);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

export const updateClassificationById = createAsyncThunk<Classification, Classification>(
  "classification/update",
    async (classification, thunkAPI) => {
        try {
            await classificationService.updateClassification(classification);
        // Return updated Classification so reducer gets correct payload type
            return classification;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Delete Classification
export const deleteClassificationById = createAsyncThunk<number, number>(
  "classification/delete",
    async (id, thunkAPI) => {
        try {
            await classificationService.deleteClassification(id);
            return id;
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

// Search Classifications
export const searchClassifications = createAsyncThunk(
  "classification/search",
    async (name: string, thunkAPI) => {
        try {
            return await classificationService.searchClassifications(name);
        } catch (error: any) {
            return thunkAPI.rejectWithValue(error.message);
        }
    }
);

