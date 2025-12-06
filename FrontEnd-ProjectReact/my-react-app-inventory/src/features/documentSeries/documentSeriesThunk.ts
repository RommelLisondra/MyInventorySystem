
import { createAsyncThunk } from "@reduxjs/toolkit";
import * as documentSeriesService from "../../services/documentSeriesService";
import type { DocumentSeries } from "../../types/documentSeries";
import type { PagedResponse } from "../../types/pagedResponse";

// Fetch all DocumentSeriess
export const fetchDocumentSeriess = createAsyncThunk(
  "documentSeries/fetchAll",
  async (_, thunkAPI) => {
    try {
      return await documentSeriesService.getDocumentSeriess();
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch all DocumentSeriess By page
export const fetchDocumentSeriessPaged = createAsyncThunk<PagedResponse<DocumentSeries[]>, // return type
  { pageNumber: number; pageSize: number }, // parameters
  { rejectValue: string } // rejection type
>(
  "documentSeries/fetchPaged",
  async ({ pageNumber, pageSize }, thunkAPI) => {
    try {
      return await documentSeriesService.getDocumentSeriessPaged(pageNumber, pageSize);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Fetch single DocumentSeries
export const fetchDocumentSeriesById = createAsyncThunk(
  "documentSeries/fetchById",
  async (id: number, thunkAPI) => {
    try {
      return await documentSeriesService.getDocumentSeriesById(id);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Add new DocumentSeries
export const addDocumentSeries = createAsyncThunk(
  "documentSeries/add",
  async (DocumentSeries: DocumentSeries, thunkAPI) => {
    try {
      return await documentSeriesService.createDocumentSeries(DocumentSeries);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

export const updateDocumentSeriesById = createAsyncThunk<DocumentSeries, DocumentSeries>(
  "documentSeries/update",
  async (DocumentSeries, thunkAPI) => {
    try {
      await documentSeriesService.updateDocumentSeries(DocumentSeries);
      // Return updated DocumentSeries so reducer gets correct payload type
      return DocumentSeries;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Delete DocumentSeries
export const deleteDocumentSeriesById = createAsyncThunk<number, number>(
  "documentSeries/delete",
  async (id, thunkAPI) => {
    try {
      await documentSeriesService.deleteDocumentSeries(id);
      return id;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

// Search DocumentSeriess
export const searchDocumentSeriess = createAsyncThunk(
  "documentSeries/search",
  async (name: string, thunkAPI) => {
    try {
      return await documentSeriesService.searchDocumentSeriess(name);
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.message);
    }
  }
);

